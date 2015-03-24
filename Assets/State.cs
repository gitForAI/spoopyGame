using UnityEngine;
using System.Collections.Generic;

public class State {

	// Action to enter state
	public Action entryAction;
	// Action to exit state
	public Action exitAction;
	// List of actions state can execute
	public List<Action> actions = new List<Action>();
	// Transition to enter state
	public Transition entryTransition;
	// Transition to exit the state
	public Transition exitTransition;
	// List of transitions in the state
	public List<Transition> transitions = new List<Transition>();
	// Current action being executed in the state
	public Action currAction;
	// Action number
	public int actNum = 0;
	// State machine this state is associated with
	public StateMachine parent;

	// Constructor
	public State(List<Action> moves){//, List<Transition> tran){
		actions = moves;
		//transitions = tran;
	}

	// Returns the current action that the state machine should execute
	public Action update(){
		// Check if each transition is triggered
		foreach(var transition in transitions){
			if(transition.toTrigger.test ()){
				Debug.Log (transition.toTrigger);
				// Set parent machine's triggered transition to whichever transition's test returned true
				parent.triggered = transition;
				parent.isTriggered = true;
				// Sets current machine to transition's target machine
				parent.parent.currMachine = transition.targetMachine;
				// Sets current state to transition's target state
				parent.currState = transition.targetState;
				// Return target state's current action
				return transition.targetState.actions[transition.targetState.actNum];
			}
		}
		if(actions != null){
			// If there's more than 1 action, do the next action and add to the action number count
			if(actions.Count > 1 && actNum < actions.Count){
				currAction = actions[actNum];
				actNum ++;
			}
			// If there's one action, do that action
			else if(actions.Count == 1 && actions[0] != null){
				Debug.Log ("only one action");
				currAction = actions[0];
			}
			// Reset action number count to 0
			if(actNum == actions.Count){
				actNum = 0;
			}
		}
		return currAction;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
