using UnityEngine;
using System.Collections;

public class InSameRoomMonster : Condition {

	public GameObject person;
	public Human humanScript;
	public Monster monster;

	public override bool test(){
		int hX = humanScript.x;
		int hY = humanScript.y;
		int mX = monster.x;
		int mY = monster.y;
		return InSameRoom (hX, hY, mX, mY);
	}

	// Use this for initialization
	void Start () {
		person = GameObject.Find("human");
		humanScript = person.GetComponent<Human> ();
		monster = gameObject.GetComponent <Monster> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
