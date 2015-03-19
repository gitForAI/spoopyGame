using UnityEngine;
using System.Collections;

public class RunScared : Action {

	public Human human;

	public override void Act(){
		if(human.roomNum == 1){
			human.humanActions.Add (new MoveToRoom2());
		}
		else if(human.roomNum == 2){
			human.humanActions.Add (new MoveToRoom3());
		}
		else if(human.roomNum == 3){
			human.humanActions.Add (new MoveToRoom4());
		}
		else{
			human.humanActions.Add (new MoveToRoom1());
		}
	}

	// Use this for initialization
	void Start () {
		shouldMove = false;
		human = gameObject.GetComponent<Human> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
