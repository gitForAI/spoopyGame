using UnityEngine;
using System.Collections.Generic;

public class MoveToHuman : Action {
	
	public Human script;
	public aStar path;
	public bool shouldMove = true;

	public override void Act(){
		//find human coordinates
		//send move command based on coordinates
		//communicate with whatever gives command to move
		int humanX = script.x;
		int humanY = script.y;
		path.move (humanX, humanY);
	}

	// Use this for initialization
	void Start () {
		script = gameObject.GetComponent<Human>();
		path = gameObject.GetComponent<aStar> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
