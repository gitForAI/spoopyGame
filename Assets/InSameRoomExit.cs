using UnityEngine;
using System.Collections;

// Condition that human is in the same room as the exit
public class InSameRoomExit : Condition {

	// Will hold human GameObject
	public GameObject person;
	// Will hold Human script attached to human GameObject
	public Human humanScript;

	// Constructor
	public InSameRoomExit(){
		// Find GameObject with tag human
		person = GameObject.FindWithTag ("human");
		// Assign Human script attached to human GameObject
		humanScript = person.GetComponent<Human>();
	}

	// Test if condition has been fulfilled
	public override bool test(){
		// If human is in room 4, return true
		return (humanScript.roomNum == 4);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
