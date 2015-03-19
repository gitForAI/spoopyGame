using UnityEngine;
using System.Collections.Generic;

public class HStateMachine : MonoBehaviour {

	public List<StateMachine> machines;
	public StateMachine currMachine;
	public StateMachine initMachine;
	public Action currAction;
	public GameObject attachedTo;
	public StateMachine savedMachine;

	public HStateMachine(List<StateMachine> mach, StateMachine init, 
	    GameObject attached){
		machines = mach;
		initMachine = init;
		attachedTo = attached;
	}

	public Action update(){
		// If no current machine, start at initial machine
		if(!currMachine.exists){
			currMachine = initMachine;
			// If no saved state at initial machine, go to initial state
			if(!currMachine.savedState.exists){
				currMachine.currState = currMachine.initState;
			}
			// Otherwise go to saved state
			else{
				currMachine.currState = currMachine.savedState;
			}
		}
		// If there is a triggered transition
		if(currMachine.isTriggered){
			savedMachine = currMachine;
			currMachine.savedState = currMachine.currState;
			currMachine = savedMachine.triggered.targetMachine;
			currMachine.currState = savedMachine.triggered.targetState;

			savedMachine.triggered.exists = false;
			savedMachine.currState.exists = false;
			savedMachine.exists = true;
			currAction = currMachine.currState.update ();
		}
		// Otherwise, just do the during action of the current state
		else{
			currAction = currMachine.currState.update ();
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
