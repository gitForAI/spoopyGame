using UnityEngine;
using System.Collections;

// Condition that human has found key and exit
public class KeyAndExit : Condition {

	// Will hold human GameObject
	public GameObject person;
	// Will hold Human script attached to human GameObject
	public Human humanScript;

	// Constructor
	public KeyAndExit(){
		// Find GameObject with tag human
		person = GameObject.FindWithTag ("human");
		// Assign Human script attached to human GameObject
		humanScript = person.GetComponent<Human> ();
	}

	// Test if condition is fulfilled
	public override bool test(){
		// Return true if key is found and exit is found
		return (humanScript.keyFound && humanScript.exitFound);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
