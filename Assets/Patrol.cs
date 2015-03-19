using UnityEngine;
using System.Collections.Generic;

public class Patrol : State {

	public Action entryAction;
	public Action exitAction;
	public Action duringAction;
	public List<Action> actions;
	public Transition entryTransition;
	public Transition exitTransition;
	public List<Transition> transitions;
	public int actNum = 0;

	public Patrol(List<Action> moves){
		actions = moves;
	}

	public override void performState(){
		if(actions.Count > 0 && actNum < actions.Count){
			duringAction = actions[actNum];
			actNum ++;
		}
		else if(actions.Count > 0 && actNum == actions.Count){
			actNum = 0;
			duringAction = actions[actNum];
		}
		else if(actions.Count == 0){
			duringAction = actions[0];
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
