using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

//nodes to put on open list that contain the square and the heuristic +
//cost so far
public class openListNode {
	//current square value
	public Square currSquare;
	//cost so far
	public int costSoFar;
	//heuristic
	public int heuristic;
	//heuristic + cost
	public int heuristicCost;
	//back path
	public List<closedListNode> cameFrom = new List<closedListNode>();

	public openListNode(Square curr, int cost, int h){
		currSquare = curr;
		costSoFar = cost;
		heuristic = h;
		heuristicCost = costSoFar + heuristic;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
