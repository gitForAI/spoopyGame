using UnityEngine;
using System.Collections;

public class PickUpKey : Action {

	public KeyCoord key;
	public Human human;

	public override void Act(){
		Destroy (key.gameObject);
		human.keyFound = true;
	}

	// Use this for initialization
	void Start () {
		key = gameObject.GetComponent<KeyCoord> ();
		human = gameObject.GetComponent<Human> ();
		x = key.x;
		y = key.y;
		shouldMove = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
