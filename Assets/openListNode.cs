using UnityEngine;
using System.Collections;

//nodes to put on open list that contain the square and the heuristic +
//cost so far
public class openListNode {
	//current square value
	Square currSquare;
	//heuristic + cost
	int heuristicCost;

	public openListNode(Square curr, int cost){
		currSquare = curr;
		heuristicCost = cost;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
