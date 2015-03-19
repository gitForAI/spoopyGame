using UnityEngine;
using System.Collections;

public class StoreExit : Action {

	public Human human;

	public override void Act(){
		human.exitNode = human.exit;
	}

	// Use this for initialization
	void Start () {
		human = gameObject.GetComponent<Human> ();
		shouldMove = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
