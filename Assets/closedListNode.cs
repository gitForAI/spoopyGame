using UnityEngine;
using System.Collections;

//nodes to put in closed list that contain cost so far and current square
public class closedListNode {
	//current square
	public Square currSquare;
	//cost so far
	public int soFar;

	public closedListNode(Square curr, int cost){
		currSquare = curr;
		soFar = cost;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
