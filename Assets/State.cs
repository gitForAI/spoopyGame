using UnityEngine;
using System.Collections.Generic;

public class State {

	public Action entryAction;
	public Action exitAction;
	public List<Action> actions;
	public Transition entryTransition;
	public Transition exitTransition;
	public List<Transition> transitions;
	public Action currAction;
	public int actNum = 0;
	public StateMachine parent;
	public bool exists = true;

	public Action update(){
		foreach(var transition in transitions){
			if(transition.toTrigger.test ()){
				parent.triggered = transition;
				parent.isTriggered = true;
				return currAction;
			}
		}
		if(actions.Count > 0 && actNum < actions.Count){
			currAction = actions[actNum];
			actNum ++;
		}
		else if(actions.Count > 0 && actNum == actions.Count){
			actNum = 0;
			currAction = actions[actNum];
		}
		else if(actions.Count == 0){
			currAction = actions[0];
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
