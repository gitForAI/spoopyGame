using UnityEngine;
using System.Collections;

public class InSameRoomMonster : Condition {

	public GameObject person;
	public Human humanScript;
	public GameObject monster;
	public Monster monsterScript;

	public InSameRoomMonster(){
		person = GameObject.FindWithTag("human");
		humanScript = person.GetComponent<Human> ();
		monster = GameObject.FindWithTag ("monster");
		monsterScript = monster.GetComponent <Monster> ();
	}

	public override bool test(){
		int hX = humanScript.x;
		int hY = humanScript.y;
		int mX = monster.GetComponent<Monster> ().x;
		int mY = monster.GetComponent <Monster> ().y;
		return InSameRoom (hX, hY, mX, mY);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
