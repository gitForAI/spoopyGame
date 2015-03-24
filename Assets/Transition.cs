using UnityEngine;
using System.Collections.Generic;

// Transitions between states and state machines
public class Transition {

	// State that transition starts in
	public State initState;
	// State transition ends in
	public State targetState;
	// Whether transition has been triggered
	public bool triggered;
	// State machine transition starts in
	public StateMachine initMachine;
	// State machine transition ends in
	public StateMachine targetMachine;
	// Condition that must be fulfilled to trigger this transition
	public Condition toTrigger;
	

	// Constructor
	public Transition(State init, State target, StateMachine initial, StateMachine targ){
		initState = init;
		targetState = target;
		triggered = false;
		initMachine = initial;
		targetMachine = targ;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
