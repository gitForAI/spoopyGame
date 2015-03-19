using UnityEngine;
using System.Collections;

public class MoveToExit : Action {

	public aStar path;
	public bool shouldMove = true;
	public Human human;

	public override void Act(){
		path.move (human.exitNode.x, human.exitNode.y);
	}

	// Use this for initialization
	void Start () {
		path = gameObject.GetComponent<aStar>();
		human = gameObject.GetComponent<Human> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
