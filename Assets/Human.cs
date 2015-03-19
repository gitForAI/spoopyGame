﻿using UnityEngine;
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
	// Action that holds where human should move to
	public Action moveTo;
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
	public Patrol walking;
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
	public List<Action> humanActions;
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
		humanMachines.Add (exploring);
		humanMachines.Add (running);
		humanMachines.Add (exiting);

		exploreStates.Add (walking);
		exploreStates.Add (getKey);
		exploreStates.Add (storeExit);

		runStates.Add (runAway);
		runStates.Add (runScared);

		exitStates.Add (goToExit);

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

		// Human HFSM
		machine = new HStateMachine (humanMachines, exploring, humanActions);

		// State machines in HFSM
		exploring = new StateMachine (walking, expTransition, exploreStates, 0);
		running = new StateMachine (null, runTransition, runStates, 2);
		exiting = new StateMachine (goToExit, exitTransition, exitStates, 1);

		// States in state machines
		walking = new Patrol (expAct);
		runAway = new State ();
		runScared = new State ();
		goToExit = new State ();
		getKey = new State ();
		storeExit = new State ();

		goToExit.actions = exitAct;
		runAway.actions.Add (new RunAway ());
		runScared.actions.Add (new RunScared ());
		getKey.actions.Add (new PickUpKey ());
		storeExit.actions.Add (new StoreExit ());

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
		// If boolean flag is set, move to target held by action
		while(true){
			// If no current machine, start at initial machine
			if(machine.currMachine == null){
				machine.currMachine = machine.initMachine;
				// If no saved state at initial machine, go to initial state
				if(machine.currMachine.savedState == null){
					machine.currMachine.currState = machine.currMachine.initState;
				}
				// Otherwise go to saved state
				else{
					machine.currMachine.currState = machine.currMachine.savedState;
				}
			}
			// If there is a triggered transition
			if(machine.currMachine.triggered != null){
				// Save the current state and set the current state to null
				machine.currMachine.savedState = machine.currMachine.currState;
				machine.currMachine.currState = null;
				//StateMachine oldMachine = machine.currMachine;
				// Set current machine to target machine in triggered transition
				machine.currMachine = machine.currMachine.triggered.targetMachine;
				// If no saved state in current machine, go to initial state
				if(machine.currMachine.savedState == null){
					machine.currMachine.currState = machine.currMachine.initState;
				}
				// Otherwise go to the saved state
				else{
					machine.currMachine.currState = machine.currMachine.savedState;
				}
				// Add the entry action and during action of the new state to the action list
				humanActions.Add (machine.currMachine.currState.entryAction);
				humanActions.Add (machine.currMachine.currState.duringAction);
			}
			// Otherwise, just do the during action of the current state
			else{
				humanActions.Add(machine.currMachine.currState.duringAction);
			}

			// If flag is set to move, move. 
			if(shouldMove){

			}
		}
	}
}
