using UnityEngine;
using System.Collections;

public class KeyAndExit : Condition {

	public GameObject person;
	public Human humanScript;

	public KeyAndExit(){
		person = GameObject.FindWithTag ("human");
		humanScript = person.GetComponent<Human> ();
	}

	public override bool test(){
		return (humanScript.keyFound && humanScript.exitFound);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
