using UnityEngine;
using System.Collections;

//nodes to put in closed list that contain cost so far and current square
public class closedListNode {
	//current square
	Square currSquare;
	//cost so far
	int cost;

	public closedListNode(Square curr, int soFar){
		currSquare = curr;
		cost = soFar;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
