using UnityEngine;
using System.Collections;

public class InSameRoomKey : Condition {

	public GameObject person;
	public Human humanScript;
	public GameObject key;
	public KeyCoord keyScript;

	public InSameRoomKey(){

	}


	public override bool test(){
		person = GameObject.FindWithTag ("human");
		humanScript = person.GetComponent<Human> ();
		key = GameObject.FindWithTag ("key");
		keyScript = key.GetComponent<KeyCoord> ();

		int hX = humanScript.x;
		int hY = humanScript.y;
		int kX = keyScript.x;
		int kY = keyScript.y;
		Debug.Log ("human: x = " + hX + ", y = " + hY);
		Debug.Log ("key: x = " + kX + ", y = " + kY);
		return InSameRoom (hX, hY, kX, kY);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
