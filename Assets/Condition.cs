using UnityEngine;
using System.Collections;

// Parent class for conditions
public class Condition {

	// Empty constructor
	public Condition(){

	}

	// Empty test function
	// Will test whether subclass conditions have been fulfilled
	public virtual bool test(){
		return true;
	}

	// Test if thing at (x1, y1) is in the same room as thing at (x2, y2)
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
