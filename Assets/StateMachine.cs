using UnityEngine;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour {

	public State initState;
	public State savedState;
	public State currState;
	public List<Transition> transitions;
	//public Transition entryTransition;
	//public Transition exitTransition;
	public List<State> states;
	public Transition triggered;
	public bool isTriggered = false;
	public int level;
	public HStateMachine parent;
	public bool exists = true;

	public StateMachine(State init, List<Transition> tran, //Transition enter, Transition exit,
	    List<State> States, int lev){
		initState = init;
		//entryTransition = enter;
		//exitTransition = exit;
		states = States;
		level = lev;
		transitions = tran;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
