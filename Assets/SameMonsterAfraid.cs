using UnityEngine;
using System.Collections;

public class SameMonsterAfraid : Condition {

	public GameObject person;
	public Human humanScript;
	public GameObject monster;
	public Monster monsterScript;

	public SameMonsterAfraid(){
		person = GameObject.FindWithTag ("human");
		humanScript = person.GetComponent<Human> ();
		monster = GameObject.FindWithTag ("monster");
		monsterScript = monster.GetComponent<Monster> ();
	}

	public override bool test(){
		int hX = humanScript.x;
		int hY = humanScript.y;
		int mX = monsterScript.x;
		int mY = monsterScript.y;
		return (InSameRoom (hX, hY, mX, mY) && humanScript.isAfraid);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
