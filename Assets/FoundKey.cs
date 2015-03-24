using UnityEngine;
using System.Collections;

public class FoundKey : Condition {

	public GameObject person;
	public Human humanScript;

	public FoundKey(){
		person = GameObject.FindWithTag ("human");
		humanScript = person.GetComponent<Human> ();
	}

	public override bool test(){
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
