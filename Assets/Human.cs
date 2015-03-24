using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class Human : MonoBehaviour {

	public Human(){

	}

	// Human's x and y position
	public int x;
	public int y;

	// Room number
	public int roomNum = 1;

	// Stores exit node
	Square exitNode;

	// Tells if key has been picked up or not
	public bool keyFound = false;
	// Tells if exit has been found
	public bool exitFound = false;
	// Tells if human is afraid
	public bool isAfraid = false;

	// Object reference to allow call to pathfinding algorithm
	aStar path;
	// Object reference to monster
	public GameObject monster;
	// Reference to monster script
	Monster monsterScript;
	// Reference to key
	public GameObject key;
	// Object reference to key script
	KeyCoord keyScript;

	// Hierarchical finite state machine to allow human to make 
	// decisions about its environment
	HStateMachine machine;

	// State machine for exploring
	StateMachine exploring;
	// Action list for exploring
	List<Action> expAct;	
	// State machine for running
	StateMachine running;
	// Action list for running
	List<Action> runAct;
	// State machine for exiting
	StateMachine exiting;
	// Action list for exiting
	List<Action> exitAct;

	// Action list for run scared
	List<Action> runAwayAct;
	// Action list for get key
	List<Action> getKeyAct;
	// Action list for store exit
	List<Action> storeAct;

	// State for walking
	State walking;
	// State for running
	State runAway;
	// State for afraid running
	State runScared;
	// State for running to exit
	State goToExit;
	// State for picking up key
	State getKey;
	// State for storing exit
	State storeExit;

	// Transition from explore to run
	Transition walkToRun;
	// Transition from explore to run scared
	Transition walkToScared;
	// Transition from run to run scared
	Transition runToScared;
	// Transition from run to explore
	Transition runToWalk;
	// Transition from run scared to explore
	Transition scaredToWalk;
	// Transition from run to exit
	Transition runToExit;
	// Transition from exit to run
	Transition exitToRun;
	//Transition from exit to run scared
	Transition exitToScared;
	// Transition from walk to pick up key
	Transition walkToKey;
	// Transition from walk to store exit
	Transition walkToStore;
	// Transition from scared to pick up key
	Transition scaredToKey;
	// Transition from scared to store exit
	Transition scaredToStore;
	// Transition from store exit to scared
	Transition storeToScared;
	// Transition from store exit to go to exit
	Transition storeToExit;
	// Transition from store exit to walk
	Transition storeToWalk;
	// Transition from get key to run scared
	Transition keyToScared;
	// Transition from get key to walk
	Transition keyToWalk;
	// Transition from get key to run
	Transition keyToRun;

	// Action list
	List<Action> humanActions;
	// State machine list
	List<StateMachine> humanMachines;

	// Exploring state list
	List<State> exploreStates;
	// Running state list
	List<State> runStates;
	// Exiting state list
	List<State> exitStates;

	// Exploring transition list
	List<Transition> expTransition;
	// Running transition list
	List<Transition> runTransition;
	// Exiting transition list
	List<Transition> exitTransition;
	// Run scared transition list
	List<Transition> runAwayTransition;
	// Get key transition list
	List<Transition> getKeyTransition;
	// Store exit transition list
	List<Transition> storeTransition;

	// Condition: in same room as monster
	InSameRoomMonster seeMonster;
	// Condition: in same room as exit
	InSameRoomExit seeExit;
	// Condition: in same room as key
	InSameRoomKey seeKey;
	// Condition: in same room as monster and afraid
	SameMonsterAfraid afraidMonster;
	// Condition: key and exit found
	KeyAndExit bothFound;
	// Condition: haven't seen monster for 5 seconds
	Escaped escaped;
	// Condition: found the key
	FoundKey isFound;

	public float timeSinceMonster = 0;
	public float timeToRun = 0;

	public void room1Move(){
		int newX = (int)Mathf.Round (UnityEngine.Random.Range(0,2));
		int newY = (int)Mathf.Round (UnityEngine.Random.Range (0,2));
		path.move (newX, newY);
		roomNum = 1;
	}

	public void room2Move(){
		int newX = (int)Mathf.Round (UnityEngine.Random.Range (4,6));
		int newY = (int)Mathf.Round (UnityEngine.Random.Range (0,2));
		path.move (newX, newY);
		roomNum = 2;
	}

	public void room3Move(){
		int newX = (int)Mathf.Round (UnityEngine.Random.Range (4, 6));
		int newY = (int)Mathf.Round (UnityEngine.Random.Range (4, 6));
		path.move (newX, newY);
		roomNum = 3;
	}

	public void room4Move(){
		int newX = (int)Mathf.Round (UnityEngine.Random.Range (0,2));
		int newY = (int)Mathf.Round (UnityEngine.Random.Range (4,6));
		path.move (newX, newY);
		roomNum = 4;
	}

	// Use this for initialization
	void Start () {
		try{
			path = gameObject.GetComponent<aStar> ();
			key = GameObject.FindWithTag ("key");
			keyScript = key.GetComponent<KeyCoord> ();
			monster = GameObject.FindWithTag ("monster");
			monsterScript = monster.GetComponent<Monster> ();

			humanActions = new List<Action> ();

			seeMonster = new InSameRoomMonster ();
			seeExit = new InSameRoomExit ();
			seeKey = new InSameRoomKey ();
			afraidMonster = new SameMonsterAfraid ();
			bothFound = new KeyAndExit ();
			escaped = new Escaped ();
			isFound = new FoundKey ();

			exploreStates = new List<State> ();
			runStates = new List<State> ();
			exitStates = new List<State> ();

			expTransition = new List<Transition> ();
			runTransition = new List<Transition> ();
			exitTransition = new List<Transition> ();
			runAwayTransition = new List<Transition> ();
			getKeyTransition = new List<Transition>();
			storeTransition = new List<Transition> ();

			humanMachines = new List<StateMachine> ();

			humanActions = new List<Action> ();

			expAct = new List<Action> ();
			exitAct = new List<Action> ();
			runAwayAct = new List<Action> ();
			getKeyAct = new List<Action> ();
			storeAct = new List<Action> ();
			runAct = new List<Action> ();

			expAct.Add (new MoveToRoom1 ());
			expAct.Add (new MoveToRoom2 ());
			expAct.Add (new MoveToRoom3 ());
			expAct.Add (new MoveToRoom4 ());
			
			runAct.Add (new RunAway ());
			runAwayAct.Add (new RunScared ());
			
			exitAct.Add (new MoveToExit ());
			
			getKeyAct.Add (new PickUpKey ());
			
			storeAct.Add (new StoreExit ());

			// States in state machines
			walking = new State (expAct);
			runAway = new State (runAct);
			runScared = new State (runAwayAct);
			goToExit = new State (exitAct);
			getKey = new State (getKeyAct);
			storeExit = new State (storeAct);

			exploreStates.Add (walking);
			exploreStates.Add (getKey);
			exploreStates.Add (storeExit);
			
			runStates.Add (runAway);
			runStates.Add (runScared);
			
			exitStates.Add (goToExit);
			
			// State machines in HFSM
			exploring = new StateMachine (walking, exploreStates, 0);
			running = new StateMachine (runAway, runStates, 2);
			exiting = new StateMachine (goToExit, exitStates, 1);

			// Transitions
			walkToRun = new Transition (walking, runAway, exploring, running);
			walkToScared = new Transition (walking, runScared, exploring, running);
			runToExit = new Transition (runAway, goToExit, running, exiting);
			runToScared = new Transition (runAway, runScared, running, running);
			runToWalk = new Transition (runAway, walking, running, exploring);
			scaredToWalk = new Transition (runScared, walking, running, exploring);
			exitToRun = new Transition (goToExit, runAway, exiting, running);
			exitToScared = new Transition (goToExit, runScared, exiting, running);
			walkToKey = new Transition (walking, getKey, exploring, exploring);
			walkToStore = new Transition (walking, storeExit, exploring, exploring);
			scaredToKey = new Transition (runScared, getKey, running, exploring);
			scaredToStore = new Transition (runScared, storeExit, running, exploring);
			storeToScared = new Transition (storeExit, runScared, exploring, running);
			storeToExit = new Transition (storeExit, goToExit, exploring, exiting);
			storeToWalk = new Transition (storeExit, walking, exploring, exploring);
			keyToScared = new Transition (getKey, runScared, exploring, running);
			keyToWalk = new Transition (getKey, walking, exploring, exploring);
			keyToRun = new Transition (getKey, runAway, exploring, running);
			
			walkToRun.toTrigger = seeMonster;
			walkToScared.toTrigger = seeMonster;
			runToExit.toTrigger = seeExit;
			runToScared.toTrigger = afraidMonster;
			runToWalk.toTrigger = escaped;
			scaredToWalk.toTrigger = escaped;
			exitToRun.toTrigger = seeMonster;
			exitToScared.toTrigger = afraidMonster;
			walkToKey.toTrigger = seeKey;
			walkToStore.toTrigger = seeExit;
			scaredToKey.toTrigger = seeKey;
			scaredToStore.toTrigger = seeExit;
			storeToScared.toTrigger = seeMonster;
			storeToExit.toTrigger = bothFound;
			keyToRun.toTrigger = seeMonster;
			keyToWalk.toTrigger = isFound;
			keyToScared.toTrigger = afraidMonster;
			
			expTransition.Add (walkToRun);
			expTransition.Add (walkToScared);
			expTransition.Add (walkToKey);
			expTransition.Add (walkToStore);
			
			runTransition.Add (runToExit);
			runTransition.Add (runToScared);
			runTransition.Add (runToWalk);
			
			runAwayTransition.Add (scaredToWalk);
			runAwayTransition.Add (scaredToKey);
			runAwayTransition.Add (scaredToStore);
			
			exitTransition.Add (exitToRun);
			exitTransition.Add (exitToScared);
			
			getKeyTransition.Add (keyToRun);
			getKeyTransition.Add (keyToScared);
			getKeyTransition.Add (keyToWalk);
			
			storeTransition.Add (storeToExit);
			storeTransition.Add (storeToScared);
			storeTransition.Add (storeToWalk);

			exploring.transitions = expTransition;
			running.transitions = runTransition;
			exiting.transitions = exitTransition;

			walking.transitions = expTransition;
			runAway.transitions = runTransition;
			runScared.transitions = runAwayTransition;
			goToExit.transitions = exitTransition;
			getKey.transitions = getKeyTransition;
			storeExit.transitions = storeTransition;

			walking.parent = exploring;
			getKey.parent = exploring;
			storeExit.parent = exploring;

			runAway.parent = running;
			runScared.parent = running;

			humanMachines.Add (exploring);
			humanMachines.Add (running);
			humanMachines.Add (exiting);

			// Human HFSM
			this.machine = new HStateMachine (humanMachines, exploring);

			exploring.parent = machine;
			running.parent = machine;
			exiting.parent = machine;

			goToExit.parent = exiting;
		}
		catch(Exception e){
			Debug.Log ("exception " + e);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Set x and y postion to value of square human is on;
		// Make the units more readable and base them on the scale of 
		//the floor
		if (transform.position.x >= 10) {
			x = (int)(Math.Floor ((double)(transform.position.x / 10)));
		}
		else{
			x = 0;
		}
		if(transform.position.z >= 10){
			y = (int)(Math.Floor ((double)(transform.position.z / 10)));
		}
		else{
			y = 0;
		}
		if(timeToRun >= 3){
			timeToRun = 0;
			if(machine.currMachine != null){
				if(machine.currMachine.triggered != null){
					if(machine.currMachine.triggered.toTrigger is InSameRoomMonster){
						timeSinceMonster = 0;
					}
				}
			}

			if(machine.currMachine != null && machine.currMachine.triggered != null && machine.currMachine.triggered.toTrigger is Escaped){
				for(int i=0; i<generateSquares.xScale-1; i++){
					for(int j=0; j<generateSquares.yScale-1; j++){
						if(generateSquares.grid[i,j].cost == 999){
							generateSquares.grid[i,j].cost = 1;
						}
					}
				}
			}
			timeSinceMonster += Time.deltaTime;


			humanActions.Add (machine.update ());
			Action newAction = humanActions [0];
			Debug.Log ("action " + newAction);
			humanActions.Remove (humanActions [0]);

			if(newAction is PickUpKey){
				path.move (keyScript.x, keyScript.y);
				Destroy(key);
				keyFound = true;
				walking.transitions.Remove(walkToKey);
				expTransition.Remove (walkToKey);
				runAwayTransition.Remove (scaredToKey);
			}
			else if(newAction is MoveToRoom1){
				Debug.Log ("moving to room 1");
				room1Move ();
			}
			else if(newAction is MoveToRoom2){
				Debug.Log ("moving to room 2");
				room2Move ();
			}
			else if(newAction is MoveToRoom3){
				Debug.Log ("moving to room 3");
				room3Move();
			}
			else if(newAction is MoveToRoom4){
				Debug.Log ("moving to room 4");
				room4Move();
			}
			else if(newAction is MoveToExit){
				path.move (1,7);
			}
			else if(newAction is RunAway){
				int monsterX = monsterScript.x;
				int monsterY = monsterScript.y;
				Debug.Log ("running away");
				timeSinceMonster = 0;
				if(monsterX - 1 >= 0){
					generateSquares.grid[monsterX - 1, monsterY].cost = 999;
				}
				if(monsterX + 1 <= generateSquares.xScale){
					generateSquares.grid[monsterX + 1, monsterY].cost = 999;
				}
				if(monsterY - 1 >= 0){
					generateSquares.grid[monsterX, monsterY - 1].cost = 999;
				}
				if(monsterY + 1 <= generateSquares.yScale){
					generateSquares.grid[monsterX, monsterY + 1].cost = 999;
				}

				if(roomNum == 1){
					room2Move();
				}
				else if(roomNum == 2){
					room3Move();
				}
				else if(roomNum == 3){
					room4Move();
				}
				else{
					room1Move();
				}
			}
			else if(newAction is RunScared){
				timeSinceMonster = 0;
				if(roomNum == 1){
					humanActions.Add (new MoveToRoom2());
				}
				else if(roomNum == 2){
					humanActions.Add (new MoveToRoom3());
				}
				else if(roomNum == 3){
					humanActions.Add (new MoveToRoom4());
				}
				else{
					humanActions.Add (new MoveToRoom1());
				}
			}
			else if(newAction is StoreExit){
				exitNode = generateSquares.grid[7,1];
				exitFound = true;
			}
		}
		else{
			timeToRun += Time.deltaTime;
			timeSinceMonster += Time.deltaTime;
		}
	}
}
