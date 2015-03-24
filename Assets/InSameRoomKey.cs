using UnityEngine;
using System.Collections;

// Condition that human is in same room as key
public class InSameRoomKey : Condition {

	// Will hold human GameObject
	public GameObject person;
	// Will hold Human script attached to human GameObject
	public Human humanScript;
	// Will hold key GameObject
	public GameObject key;
	// Will hold KeyCoord script attached to key GameObject
	public KeyCoord keyScript;

	// Constructor
	public InSameRoomKey(){

	}

	// Test if condition has been fulfilled
	public override bool test(){
		// Find GameObject tagged with human
		person = GameObject.FindWithTag ("human");
		// Assign Human script attached to human GameObject
		humanScript = person.GetComponent<Human> ();
		// Find GameObject with tag key
		key = GameObject.FindWithTag ("key");
		// Assign KeyCoord script attached to key GameObject
		keyScript = key.GetComponent<KeyCoord> ();

		// Get key and human coordinates
		int hX = humanScript.x;
		int hY = humanScript.y;
		int kX = keyScript.x;
		int kY = keyScript.y;
		// Return true if human and key are in same room
		return InSameRoom (hX, hY, kX, kY);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
