using UnityEngine;
using System.Collections;

// Condition that human has found key
public class FoundKey : Condition {

	// Will hold human GameObject
	public GameObject person;
	// Will hold Human script attached to human GameObject
	public Human humanScript;

	// Constructor
	public FoundKey(){
		// Find GameObject with tag human
		person = GameObject.FindWithTag ("human");
		// Assign Human script attached to human GameObject
		humanScript = person.GetComponent<Human> ();
	}

	// Test if the condition has been fulfilled
	public override bool test(){
		// If the key has been found, return true
		if(humanScript.keyFound){
			return true;
		}
		return false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
