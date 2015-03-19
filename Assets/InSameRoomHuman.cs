using UnityEngine;
using System.Collections;

public class InSameRoomHuman : Condition {

	public Human human;
	public Monster monster;

	public override bool test(){
		int hX = human.x;
		int hY = human.y;
		int mX = monster.x;
		int mY = monster.y;
		return InSameRoom (hX, hY, mX, mY);
	}

	// Use this for initialization
	void Start () {
		human = gameObject.GetComponent<Human> ();
		monster = gameObject.GetComponent<Monster> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
