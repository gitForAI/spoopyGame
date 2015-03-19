using UnityEngine;
using System.Collections;

public class InSameRoomExit : Condition {

	public GameObject person;
	public Human humanScript;

	public override bool test(){
		int hX = humanScript.x;
		int hY = humanScript.y;
		return InSameRoom (hX, hY, 1, 7);
	}

	// Use this for initialization
	void Start () {
		person = GameObject.Find ("human");
		humanScript = person.GetComponent<Human>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
