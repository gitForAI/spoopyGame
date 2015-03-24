using UnityEngine;
using System.Collections;

public class Escaped : Condition {

	public GameObject person;
	public Human humanScript;

	public Escaped(){
		person = GameObject.FindWithTag ("human");
		humanScript = person.GetComponent<Human> ();
	}

	public override bool test(){
		if(humanScript.timeSinceMonster >= 5){
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
