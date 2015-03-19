using UnityEngine;
using System.Collections;

public class StoreExit : Action {

	public Human human;
	public bool shouldMove = false;

	public override void Act(){
		human.exitNode = human.exit;
	}

	// Use this for initialization
	void Start () {
		human = gameObject.GetComponent<Human> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
