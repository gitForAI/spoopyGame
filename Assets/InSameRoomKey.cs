using UnityEngine;
using System.Collections;

public class InSameRoomKey : Condition {

	public GameObject person;
	public Human humanScript;
	public GameObject key;
	public KeyCoord keyScript;

	public InSameRoomKey(){
		person = GameObject.FindWithTag ("human");
		humanScript = person.GetComponent<Human> ();
		key = GameObject.FindWithTag ("key");
		keyScript = key.GetComponent<KeyCoord> ();
	}


	public override bool test(){
		int hX = humanScript.x;
		int hY = humanScript.y;
		int kX = keyScript.x;
		int kY = keyScript.y;
		return InSameRoom (hX, hY, kX, kY);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
