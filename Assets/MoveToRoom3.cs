using UnityEngine;
using System.Collections;

public class MoveToRoom3 : Action {

	public bool shouldMove = true;
	public aStar path;

	public override void Act(){
		path.move (5, 5);
	}

	// Use this for initialization
	void Start () {
		path = gameObject.GetComponent<aStar> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
