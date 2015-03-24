using UnityEngine;
using System.Collections.Generic;

public class StateMachine {

	// Initial state of machine
	public State initState;
	// Saved state of machine
	public State savedState;
	// Current state of machine
	public State currState;
	// List of machine's transitions
	public List<Transition> transitions;
	//public Transition entryTransition;
	//public Transition exitTransition;
	// List of states in machine
	public List<State> states;
	// Transition that's currently triggered
	public Transition triggered;
	// Tells if there is a triggered transition or not
	public bool isTriggered = false;
	// Level/priority of the machine
	public int level;
	// HSM that machine is a part of
	public HStateMachine parent;

	// Constructor
	public StateMachine(State init, //List<Transition> tran, //Transition enter, Transition exit,
	    List<State> States, int lev){
		initState = init;
		//entryTransition = enter;
		//exitTransition = exit;
		states = States;
		level = lev;
		//transitions = tran;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
