using UnityEngine;
using System.Collections;

// Condition that human is in the same room as the monster
public class InSameRoomHuman : Condition {

	// Will hold human GameObject
	public GameObject person;
	// Will hold Human script attached to human GameObject
	public Human humanScript;
	// Will hold monster GameObject
	public GameObject monster;
	// Will hold Monster script attached to monster GameObject
	public Monster monsterScript;

	// Constructor
	public InSameRoomHuman(){
		// Find GameObject with tag human
		person = GameObject.FindWithTag ("human");
		// Assign Human script attached to human GameObject
		humanScript = person.GetComponent<Human> ();
		// Find GameObject with tag monster
		monster = GameObject.FindWithTag ("monster");
		// Assign Monster script attached to monster GameObject
		monsterScript = monster.GetComponent<Monster> ();
	}

	// Test if condition has been fulfilled
	public override bool test(){
		// Get x, y coordinates of human and monster
		int hX = humanScript.x;
		int hY = humanScript.y;
		int mX = monsterScript.x;
		int mY = monsterScript.y;
		// Return true if human and monster are in the same room
		return InSameRoom (hX, hY, mX, mY);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
