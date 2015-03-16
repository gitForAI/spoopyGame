using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// Nodes to put on open list that contain all information needed about 
//the node

public class openListNode {
	// Square represented by node
	public Square currSquare;
	// Cost to get to this square/node so far
	public int costSoFar;
	// Prediction of how much it will cost to get to the goal from here
	public int heuristic;
	// Heuristic + cost
	public int heuristicCost;
	// Back path
	public List<closedListNode> cameFrom = new List<closedListNode>();

	// Constructor
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
