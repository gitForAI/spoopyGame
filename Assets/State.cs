using UnityEngine;
using System.Collections.Generic;

public class State {

	public Action entryAction;
	public Action exitAction;
	public List<Action> actions = new List<Action>();
	public Transition entryTransition;
	public Transition exitTransition;
	public List<Transition> transitions = new List<Transition>();
	public Action currAction;
	public int actNum = 0;
	public StateMachine parent;
	public bool exists = true;

	public State(List<Action> moves){//, List<Transition> tran){
		actions = moves;
		//transitions = tran;
	}

	public Action update(){
		foreach(var transition in transitions){
			if(transition.toTrigger.test ()){
				Debug.Log ("triggered");
				parent.triggered = transition;
				parent.isTriggered = true;
				return currAction;
			}
		}
		if(actions.Count > 0 && actNum < actions.Count){
			Debug.Log("first if");
			currAction = actions[actNum];
			actNum ++;
		}

		else if(actions.Count == 0 && actions[0] != null){
			Debug.Log ("third if");
			currAction = actions[0];
		}
		if(actNum == actions.Count){
			actNum = 0;
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
