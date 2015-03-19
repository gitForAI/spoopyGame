using UnityEngine;
using System.Collections.Generic;
using System;

public class Human : MonoBehaviour {
	// Human's x and y position
	public int x;
	public int y;
	// Room number
	public int roomNum;
	// Stores exit node
	public Square exitNode;
	// Stores real exit for easier access later
	public Square exit = generateSquares.grid[7,1];
	// Tells if key has been picked up or not
	public bool keyFound = false;
	// Boolean flag to tell human if it should be moving
	public bool shouldMove = false;
	// Target where human should move to
	public int targetX;
	public int targetY;
	// Object reference to allow call to pathfinding algorithm
	public aStar path;
	// Hierarchical finite state machine to allow human to make 
	// decisions about its environment
	public HStateMachine machine;
	// State machine for exploring
	public StateMachine exploring;
	// Action list for exploring
	public List<Action> expAct;	
	// State machine for running
	public StateMachine running;
	// Action list for running
	public List<Action> runAct;
	// State machine for exiting
	public StateMachine exiting;
	// Action list for exiting
	public List<Action> exitAct;
	// State for walking
	public State walking;
	// State for running
	public State runAway;
	// State for afraid running
	public State runScared;
	// State for running to exit
	public State goToExit;
	// State for picking up key
	public State getKey;
	// State for storing exit
	public State storeExit;
	// Transition from explore to run
	public Transition walkToRun;
	// Transition from explore to run scared
	public Transition walkToScared;
	// Transition from explore to exit
	public Transition walkToExit;
	// Transition from run to run scared
	public Transition runToScared;
	// Transition from run to explore
	public Transition runToWalk;
	// Transition from run scared to explore
	public Transition scaredToWalk;
	// Transition from run to exit
	public Transition runToExit;
	// Transition from exit to run
	public Transition exitToRun;
	//Transition from exit to run scared
	public Transition exitToScared;
	// Transition from walk to pick up key
	public Transition walkToKey;
	// Transition from walk to store exit
	public Transition walkToStore;
	// Action list
	public List<Action> humanActions = new List<Action>();
	// State machine list
	public List<StateMachine> humanMachines;
	// Exploring state list
	public List<State> exploreStates;
	// Running state list
	public List<State> runStates;
	// Exiting state list
	public List<State> exitStates;
	// Exploring transition list
	public List<Transition> expTransition;
	// Running transition list
	public List<Transition> runTransition;
	// Exiting transition list
	public List<Transition> exitTransition;

	// Use this for initialization
	void Start () {
		path = gameObject.GetComponent<aStar> ();

		// States in state machines
		/*walking = new State ();
		runAway = new State ();
		runScared = new State ();
		goToExit = new State ();
		getKey = new State ();
		storeExit = new State ();*/

		goToExit.actions = exitAct;
		runAway.actions.Add (new RunAway ());
		runScared.actions.Add (new RunScared ());
		getKey.actions.Add (new PickUpKey ());
		storeExit.actions.Add (new StoreExit ());
		
		// State machines in HFSM
		exploring = new StateMachine (walking, expTransition, exploreStates, 0);
		running = new StateMachine (null, runTransition, runStates, 2);
		exiting = new StateMachine (goToExit, exitTransition, exitStates, 1);
		
		// Transitions
		walkToRun = new Transition (walking, runAway, exploring, running);
		walkToExit = new Transition (walking, goToExit, exploring, exiting);
		walkToScared = new Transition (walking, runScared, exploring, running);
		runToExit = new Transition (runAway, goToExit, running, exiting);
		runToScared = new Transition (runAway, runScared, running, running);
		runToWalk = new Transition (runAway, walking, running, exploring);
		scaredToWalk = new Transition (runScared, walking, running, exploring);
		exitToRun = new Transition (goToExit, runAway, exiting, running);
		exitToScared = new Transition (goToExit, runScared, exiting, running);
		walkToKey = new Transition (walking, getKey, exploring, exploring);
		walkToStore = new Transition (walking, storeExit, exploring, exploring);
		
		// Human HFSM
		machine = new HStateMachine (humanMachines, exploring, gameObject);

		exploring.parent = machine;
		running.parent = machine;
		exiting.parent = machine;

		humanMachines.Add (exploring);
		humanMachines.Add (running);
		humanMachines.Add (exiting);

		exploreStates.Add (walking);
		exploreStates.Add (getKey);
		exploreStates.Add (storeExit);
		walking.parent = exploring;
		getKey.parent = exploring;
		storeExit.parent = exploring;

		runStates.Add (runAway);
		runStates.Add (runScared);
		runAway.parent = running;
		runScared.parent = running;

		exitStates.Add (goToExit);
		goToExit.parent = exiting;
		expTransition.Add (walkToRun);
		expTransition.Add (walkToExit);
		expTransition.Add (walkToScared);

		runTransition.Add (runToExit);
		runTransition.Add (runToScared);
		runTransition.Add (runToWalk);
		runTransition.Add (scaredToWalk);

		exitTransition.Add (exitToRun);
		exitTransition.Add (exitToScared);

		expAct.Add (new MoveToRoom1 ());
		expAct.Add (new MoveToRoom2 ());
		expAct.Add (new MoveToRoom3 ());
		expAct.Add (new MoveToRoom4 ());

		runAct.Add (new RunAway ());
		runAct.Add (new RunScared ());

		exitAct.Add (new MoveToExit ());
	}
	
	// Update is called once per frame
	void Update () {
		// Set x and y postion to value of square human is on;
		// Make the units more readable and base them on the scale of 
		//the floor
		if (transform.position.x >= 10) {
			x = (int)(Math.Floor ((double)(x / 10)));
		}
		else{
			x = 0;
		}
		if(transform.position.z >= 10){
			y = (int)(Math.Floor ((double)(y / 10)));
		}
		else{
			y = 0;
		}
		humanActions.Add (machine.update ());
		Action newAction = humanActions [0];
		humanActions.Remove (humanActions [0]);
		// If flag is set to move, move. 
		if(newAction.shouldMove){
			path.move (newAction.x, newAction.y);
			newAction.Act();
		}
		else{
			newAction.Act();
		}
	}
}
