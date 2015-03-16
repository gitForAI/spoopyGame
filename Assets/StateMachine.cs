using UnityEngine;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour {

	State initState;
	State savedState;
	State currState;
	Transition entryTransition;
	Transition exitTransition;
	List<State> states;
	Transition triggered;
	int level;

	public StateMachine(State init, Transition enter, Transition exit,
	    List<State> States, int lev){
		initState = init;
		entryTransition = enter;
		exitTransition = exit;
		states = States;
		level = lev;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
