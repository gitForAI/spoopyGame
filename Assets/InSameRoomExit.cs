using UnityEngine;
using System.Collections;

public class InSameRoomExit : Condition {

	public GameObject person;
	public Human humanScript;

	public InSameRoomExit(){
		person = GameObject.FindWithTag ("human");
		humanScript = person.GetComponent<Human>();
	}

	public override bool test(){
		return humanScript.roomNum == 4;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
