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

	// Time since the human has seen the monster
	public float timeSinceMonster = 0;
	// Counter for when the fsm should run
	public float timeToRun = 0;

	// Function to move the human to room 1
	public void room1Move(){
		// Make random x and y
		int newX = (int)Mathf.Round (UnityEngine.Random.Range(0,2));
		int newY = (int)Mathf.Round (UnityEngine.Random.Range (0,2));
		// Ensure x and y are not the same as the monster's x and y
		while(newX == monsterScript.x){
			newX = (int)Mathf.Round (UnityEngine.Random.Range(0,2));
		}
		while(newY == monsterScript.y){
			newY = (int)Mathf.Round (UnityEngine.Random.Range(0,2));
		}
		// Move to x and y
		path.move (newX, newY);
		// Change the room number
		roomNum = 1;
	}

	// Function to move the human to room 2
	public void room2Move(){
		// Create random x and y to move to in room
		int newX = (int)Mathf.Round (UnityEngine.Random.Range (4,6));
		int newY = (int)Mathf.Round (UnityEngine.Random.Range (0,2));
		// Make sure the x and y aren't the same as the monster's x and y
		while(newX == monsterScript.x){
			newX = (int)Mathf.Round (UnityEngine.Random.Range (4,6));
		}
		while(newY == monsterScript.y){
			newY = (int)Mathf.Round (UnityEngine.Random.Range (0,2));
		}
		// Move to the new x and y
		path.move (newX, newY);
		// Change the room number the human is in
		roomNum = 2;
	}

	// Function to move the human to room 3
	public void room3Move(){
		// Find random x and y
		int newX = (int)Mathf.Round (UnityEngine.Random.Range (4, 6));
		int newY = (int)Mathf.Round (UnityEngine.Random.Range (4, 6));
		// Make sure the x and y aren't the same as the monster's x and y
		while(newX == monsterScript.x){
			newX = (int)Mathf.Round (UnityEngine.Random.Range (4, 6));
		}
		while(newY == monsterScript.y){
			newY = (int)Mathf.Round (UnityEngine.Random.Range (4, 6));
		}
		// Move to x, y
		path.move (newX, newY);
		// Change room number
		roomNum = 3;
	}

	// Function to move the human to room 4
	public void room4Move(){
		// Make random x and y
		int newX = (int)Mathf.Round (UnityEngine.Random.Range (0,2));
		int newY = (int)Mathf.Round (UnityEngine.Random.Range (4,6));
		// Make sure x and y aren't the same as the monster's x and y
		while(newX == monsterScript.x){
			newX = (int)Mathf.Round (UnityEngine.Random.Range (0,2));
		}
		while(newY == monsterScript.y){
			newY = (int)Mathf.Round (UnityEngine.Random.Range (4,6));
		}
		// Move to x and y
		path.move (newX, newY);
		// Change room number
		roomNum = 4;
	}

	// Use this for initialization
	void Start () {
		try{
			// Get path, key, monster GameObjects and scripts for reference
			path = gameObject.GetComponent<aStar> ();
			key = GameObject.FindWithTag ("key");
			keyScript = key.GetComponent<KeyCoord> ();
			monster = GameObject.FindWithTag ("monster");
			monsterScript = monster.GetComponent<Monster> ();

			// Initialize list of actions for human
			humanActions = new List<Action> ();

			// Initialize transition conditions
			seeMonster = new InSameRoomMonster ();
			seeExit = new InSameRoomExit ();
			seeKey = new InSameRoomKey ();
			afraidMonster = new SameMonsterAfraid ();
			bothFound = new KeyAndExit ();
			escaped = new Escaped ();
			isFound = new FoundKey ();

			// Initialize lists of states
			exploreStates = new List<State> ();
			runStates = new List<State> ();
			exitStates = new List<State> ();

			// Initialize transition lists
			expTransition = new List<Transition> ();
			runTransition = new List<Transition> ();
			exitTransition = new List<Transition> ();
			runAwayTransition = new List<Transition> ();
			getKeyTransition = new List<Transition>();
			storeTransition = new List<Transition> ();

			// Initialize list of state machines for HFSM
			humanMachines = new List<StateMachine> ();

			// Initialize lists of actions
			expAct = new List<Action> ();
			exitAct = new List<Action> ();
			runAwayAct = new List<Action> ();
			getKeyAct = new List<Action> ();
			storeAct = new List<Action> ();
			runAct = new List<Action> ();

			// Add room moves to exploring state machine action list
			expAct.Add (new MoveToRoom1 ());
			expAct.Add (new MoveToRoom2 ());
			expAct.Add (new MoveToRoom3 ());
			expAct.Add (new MoveToRoom4 ());

			// Add run away and run away scared action to running action list
			runAct.Add (new RunAway ());
			runAwayAct.Add (new RunScared ());

			// Add exit move action to exit action list
			exitAct.Add (new MoveToExit ());

			// Add pick up key action to get key state
			getKeyAct.Add (new PickUpKey ());

			// Add store exit action to store exit state
			storeAct.Add (new StoreExit ());

			// Initialize states in state machines
			walking = new State (expAct);
			runAway = new State (runAct);
			runScared = new State (runAwayAct);
			goToExit = new State (exitAct);
			getKey = new State (getKeyAct);
			storeExit = new State (storeAct);

			// Add states to exploring state machine state list
			exploreStates.Add (walking);
			exploreStates.Add (getKey);
			exploreStates.Add (storeExit);

			// Add states to running state machine state list
			runStates.Add (runAway);
			runStates.Add (runScared);

			// Add states to exiting state machine state list
			exitStates.Add (goToExit);
			
			// Initialize state machines in HFSM
			exploring = new StateMachine (walking, exploreStates, 0);
			running = new StateMachine (runAway, runStates, 2);
			exiting = new StateMachine (goToExit, exitStates, 1);

			// Initialize transitions
			// Transition from walk to run
			walkToRun = new Transition (walking, runAway, exploring, running);
			// Transition from walk to run away scared
			walkToScared = new Transition (walking, runScared, exploring, running);
			// Transition from run to move to exit
			runToExit = new Transition (runAway, goToExit, running, exiting);
			// Transition from run to run away scared
			runToScared = new Transition (runAway, runScared, running, running);
			// Transition from run to walking
			runToWalk = new Transition (runAway, walking, running, exploring);
			// Transition from run scared to walking
			scaredToWalk = new Transition (runScared, walking, running, exploring);
			// Transition from move to exit to run
			exitToRun = new Transition (goToExit, runAway, exiting, running);
			// Transition from move to exit to run away scared
			exitToScared = new Transition (goToExit, runScared, exiting, running);
			// Transition from walking to picking up key
			walkToKey = new Transition (walking, getKey, exploring, exploring);
			// Transition from walking to storing the exit
			walkToStore = new Transition (walking, storeExit, exploring, exploring);
			// Transition from run scared to getting the key
			scaredToKey = new Transition (runScared, getKey, running, exploring);
			// Transition from run scared to store exit
			scaredToStore = new Transition (runScared, storeExit, running, exploring);
			// Transition from store exit to run scared
			storeToScared = new Transition (storeExit, runScared, exploring, running);
			// Transition from store exit to move to exit
			storeToExit = new Transition (storeExit, goToExit, exploring, exiting);
			// Transition from store exit to walking
			storeToWalk = new Transition (storeExit, walking, exploring, exploring);
			// Transition from pick up key to run scared
			keyToScared = new Transition (getKey, runScared, exploring, running);
			// Transition from pick up key to walking
			keyToWalk = new Transition (getKey, walking, exploring, exploring);
			// Transition from pick up key to run
			keyToRun = new Transition (getKey, runAway, exploring, running);

			// Set conditions to trigger each transition
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

			// Add transitions to exploring state machine
			expTransition.Add (walkToRun);
			expTransition.Add (walkToScared);
			expTransition.Add (walkToKey);
			expTransition.Add (walkToStore);

			// Add transitions to running state
			runTransition.Add (runToExit);
			runTransition.Add (runToScared);
			runTransition.Add (runToWalk);

			// Add transitions to run scared state
			runAwayTransition.Add (scaredToWalk);
			runAwayTransition.Add (scaredToKey);
			runAwayTransition.Add (scaredToStore);

			// Add transitions to move to exit state
			exitTransition.Add (exitToRun);
			exitTransition.Add (exitToScared);

			// Add transitions to get key state
			getKeyTransition.Add (keyToRun);
			getKeyTransition.Add (keyToScared);
			getKeyTransition.Add (keyToWalk);

			// Add transitions to store exit state
			storeTransition.Add (storeToExit);
			storeTransition.Add (storeToScared);
			storeTransition.Add (storeToWalk);

			// Set transition lists for state machines
			exploring.transitions = expTransition;
			running.transitions = runTransition;
			exiting.transitions = exitTransition;

			// Set transition lists for states
			walking.transitions = expTransition;
			runAway.transitions = runTransition;
			runScared.transitions = runAwayTransition;
			goToExit.transitions = exitTransition;
			getKey.transitions = getKeyTransition;
			storeExit.transitions = storeTransition;

			// Set state parents
			walking.parent = exploring;
			getKey.parent = exploring;
			storeExit.parent = exploring;

			runAway.parent = running;
			runScared.parent = running;

			// Add state machines to state machine list
			humanMachines.Add (exploring);
			humanMachines.Add (running);
			humanMachines.Add (exiting);

			// Human HFSM
			this.machine = new HStateMachine (humanMachines, exploring);

			// Set state machine parents to HFSM
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
		// Run the machine every 3 seconds
		if(timeToRun >= 3){
			timeToRun = 0;

			// If the human has escaped
			if(machine.currMachine != null && machine.currMachine.triggered != null && machine.currMachine.triggered.toTrigger is Escaped){
				// Set the monster and its surrounding blocks to normal cost again
				for(int i=0; i<generateSquares.xScale-1; i++){
					for(int j=0; j<generateSquares.yScale-1; j++){
						if(generateSquares.grid[i,j].cost == 999){
							generateSquares.grid[i,j].cost = 1;
							Debug.Log ("reset");
							break;
						}
					}
				}
			}
			// Add to time since human has seen monster
			timeSinceMonster += Time.deltaTime;

			// Add result of HSM's update to human's actions to execute
			humanActions.Add (machine.update ());
			// Set action to execute to start of human action list
			Action newAction = humanActions [0];
			Debug.Log ("action " + newAction);
			// Remove action from list
			humanActions.Remove (humanActions [0]);

			// Action is to pick up the key
			if(newAction is PickUpKey){
				// Move to key
				path.move (keyScript.x, keyScript.y);
				// Destroy key GameObject
				Destroy(key);
				keyFound = true;
				// Remove transitions involving key to avoid null pointer exceptions
				walking.transitions.Remove(walkToKey);
				expTransition.Remove (walkToKey);
				runAwayTransition.Remove (scaredToKey);
			}

			// Action is to move to first room
			else if(newAction is MoveToRoom1){
				Debug.Log ("moving to room 1");
				// Move to random location in room 1
				room1Move ();
			}
			// Action is to move to second room
			else if(newAction is MoveToRoom2){
				Debug.Log ("moving to room 2");
				// Move to random location in room 2
				room2Move ();
			}
			// Action is to move to third room
			else if(newAction is MoveToRoom3){
				Debug.Log ("moving to room 3");
				// Move to random location in room 3
				room3Move();
			}
			// Action is to move to fourth room
			else if(newAction is MoveToRoom4){
				Debug.Log ("moving to room 4");
				// Move to random location in room 4
				room4Move();
			}

			// Action is to move to exit
			else if(newAction is MoveToExit){
				// Move to exit node
				path.move (1,7);
			}

			// Action is to run away from the monster; not scared
			else if(newAction is RunAway){
				int monsterX = monsterScript.x;
				int monsterY = monsterScript.y;
				Debug.Log ("running away");
				// Reset time since seen monster
				timeSinceMonster = 0;
				// Set cost of monster square high so human won't travel to it
				generateSquares.grid[monsterX, monsterY].cost = 999;

				// If they're in room 1, move to room 2
				if(roomNum == 1){
					room2Move();
				}
				// If they're in room 2, move to room 3
				else if(roomNum == 2){
					room3Move();
				}
				// If they're in room 3, move to room 4
				else if(roomNum == 3){
					room4Move();
				}
				// Otherwise, move to room 1
				else{
					room1Move();
				}
				Debug.Log ("current machine: " + machine.currMachine.currState);
			}

			// Action is to run away from monster; is afraid
			else if(newAction is RunScared){
				// Reset time since seen monster to 0
				timeSinceMonster = 0;
				// Don't set monster square cost to high. Might run into monster.
				// If they're in room 1, move to room 2
				if(roomNum == 1){
					room2Move();
				}
				// If they're in room 2, move to room 3
				else if(roomNum == 2){
					room3Move();
				}
				// If they're in room 3, move to room 4
				else if(roomNum == 3){
					room4Move();
				}
				// Otherwise move to room 1
				else{
					room1Move();
				}
			}

			// Action is to store the exit
			else if(newAction is StoreExit){
				// Set exit node to exit square
				exitNode = generateSquares.grid[7,1];
				exitFound = true;
			}
		}
		// Add to time to run and time since seen monster
		else{
			timeToRun += Time.deltaTime;
			timeSinceMonster += Time.deltaTime;
		}
	}
}
