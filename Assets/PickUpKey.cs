using UnityEngine;
using System.Collections;

public class PickUpKey : Action {

	public KeyCoord key;
	public aStar path;
	public Human human;
	public bool shouldMove = true;

	public override void Act(){
		int keyX = key.x;
		int keyY = key.y;
		path.move (keyX, keyY);
		Destroy (key.gameObject);
		human.keyFound = true;
	}

	// Use this for initialization
	void Start () {
		key = gameObject.GetComponent<KeyCoord> ();
		path = gameObject.GetComponent<aStar> ();
		human = gameObject.GetComponent<Human> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
