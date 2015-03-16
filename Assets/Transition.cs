﻿using UnityEngine;
using System.Collections.Generic;

public class Transition : MonoBehaviour {

	State initState;
	State targetState;
	bool triggered;
	StateMachine initMachine;
	StateMachine targetMachine;

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