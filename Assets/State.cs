using UnityEngine;
using System.Collections.Generic;

public class State : MonoBehaviour {

	public Action entryAction;
	public Action exitAction;
	public List<Action> actions;
	public Transition entryTransition;
	public Transition exitTransition;
	public List<Transition> transitions;
	public Action duringAction;
	public int actNum = 0;

	public virtual void performState(){

	}

	public virtual Action update(){

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
