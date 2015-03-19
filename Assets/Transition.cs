using UnityEngine;
using System.Collections.Generic;

public class Transition : MonoBehaviour {

	public State initState;
	public State targetState;
	public bool triggered;
	public StateMachine initMachine;
	public StateMachine targetMachine;
	public Condition toTrigger;
	public bool exists = true;

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
