using UnityEngine;
using System.Collections;

// Nodes to put in closed list that contain all information needed 
//about node

public class closedListNode {
	// Square represented by this node
	public Square currSquare;
	// Cost to get to this square/node so far
	public int soFar;

	//Constructor
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
