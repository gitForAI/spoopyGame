using UnityEngine;
using System.Collections;

public class MoveToExit : Action {

	public override void Act(){

	}

	// Use this for initialization
	void Start () {
		human = gameObject.GetComponent<Human> ();
		x = human.exitNode.x;
		y = human.exitNode.y;
		shouldMove = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
