using UnityEngine;
using System.Collections.Generic;

public class HStateMachine : MonoBehaviour {

	public List<StateMachine> machines;
	public StateMachine currMachine;
	public StateMachine initMachine;
	public List<Action> actions;

	public HStateMachine(List<StateMachine> mach, StateMachine init){
		machines = mach;
		initMachine = init;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
