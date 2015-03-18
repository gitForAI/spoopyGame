using UnityEngine;
using System.Collections.Generic;

public class MoveToHuman : Action {

	public GameObject human;
	public Human script;

	public override void Act(){
		//find human coordinates
		//send move command based on coordinates
		//communicate with whatever gives command to move
		int humanX = script.x;
		int humanY = script.y;
	}

	// Use this for initialization
	void Start () {
		script = gameObject.GetComponent<Human>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
