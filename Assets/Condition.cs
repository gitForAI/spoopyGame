using UnityEngine;
using System.Collections;

public class Condition {

	public Condition(){

	}

	public virtual bool test(){
		return true;
	}

	public bool InSameRoom(int x1, int y1, int x2, int y2){
		if(Mathf.Abs(x2 - x1) <= 2 && Mathf.Abs (y2 - y1) <= 2){
			return true;
		}
		else{
			return false;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
