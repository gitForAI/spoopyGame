using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class aStar : MonoBehaviour {
	public List<openListNode> openList = new List<openListNode>();
	public List<List<closedListNode>> closedList = new 
		List<List<closedListNode>>();
	//public GameObject character;
	public int startX;
	public int startY;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x >= 10) {
			startX = (int)(Math.Floor ((double)(gameObject.transform.position.x / 10)));
		}
		else{
			startX = 0;
		}
		if(gameObject.transform.position.z >= 10){
			startY = (int)(Math.Floor ((double)(gameObject.transform.position.z / 10)));
		}
		else{
			startY = 0;
		}
	}

	public void move(int x, int y){
		List<closedListNode> path = new List<closedListNode>();
		if (generateSquares.grid != null) {
			// Holds the path returned by A* and calls the main algorithm.
			path = pathFind (generateSquares.grid [x, y]);
			// Moves human's x and y positions to the x and y positions 
			//of each node in the path list.
			foreach (var node in path) {
				// Changes gameobject's position to position of node.
				gameObject.transform.position = new Vector3(
					(float)node.currSquare.x * 10, (float)transform.position.y, 
					(float)node.currSquare.y * 10);
			}
		}
	}

	// Returns a list of successors to node and uses goalNode to find 
	//the heuristic + cost of each successor.
	public List<openListNode> getSuccessors(openListNode node, Square goalNode){
		List<openListNode> successors = new List<openListNode>();
		// Ensure that index is not out of range, then add node to successor list
		if (node.currSquare.x - 1 >= 0) {
			successors.Add (new openListNode(
				generateSquares.grid[node.currSquare.x - 1, 
			    node.currSquare.y], node.costSoFar + 
				generateSquares.grid[node.currSquare.x - 1, 
			    node.currSquare.y].cost, 
				generateSquares.grid[node.currSquare.x - 1, 
			    node.currSquare.y].getHeuristic(goalNode)));
		}
		if (node.currSquare.x + 1 <= generateSquares.xScale - 1) {
			successors.Add (new openListNode(
				generateSquares.grid[node.currSquare.x + 1, 
			    node.currSquare.y], node.costSoFar + 
				generateSquares.grid[node.currSquare.x + 1, 
			    node.currSquare.y].cost, 
				generateSquares.grid[node.currSquare.x + 1, 
			    node.currSquare.y].getHeuristic(goalNode)));
		}
		if (node.currSquare.y - 1 >= 0) {
			successors.Add (new openListNode(
				generateSquares.grid[node.currSquare.x, 
			    node.currSquare.y - 1], node.costSoFar + 
				generateSquares.grid[node.currSquare.x, 
			    node.currSquare.y - 1].cost, 
				generateSquares.grid[node.currSquare.x, 
			    node.currSquare.y - 1].getHeuristic(goalNode)));
		}
		if (node.currSquare.y + 1 <= generateSquares.yScale - 1) {
			successors.Add (new openListNode(
				generateSquares.grid[node.currSquare.x, 
			    node.currSquare.y + 1], node.costSoFar + 
				generateSquares.grid[node.currSquare.x, 
			    node.currSquare.y + 1].cost, 
				generateSquares.grid[node.currSquare.x, 
			    node.currSquare.y + 1].getHeuristic(goalNode)));
		}
		// Create path that each successor node came from in order to 
		//be able to create correct new paths later on
		foreach(var thing in successors){
			thing.cameFrom.AddRange(node.cameFrom);
			closedListNode thingClosed = new closedListNode(
				thing.currSquare, thing.costSoFar);
			thing.cameFrom.Add (thingClosed);
		}
		return successors;
	}

	// Custom function to check if list contains node.
	// Created because Unity's System.Contains function checks pointers, 
	//and new pointers were created with nodes.
	public bool contain(List<openListNode> cln, openListNode oln) 
	{
		foreach (var n in cln)
		{
			if( (n.currSquare.x == oln.currSquare.x) && 
			   (n.currSquare.y == oln.currSquare.y)){
				return true;
			}
		}
		return false;
	}

	// Main pathfinding algorithm
	// Takes in a goal node
	// Finds where object script is attached to is on the grid
	// Performs A* algorithm until a path to the goalNode is found
	public List<closedListNode> pathFind(Square goalNode){
		// Find start node
		Square startNode = generateSquares.grid[startX, startY];
		// Create open and closed list nodes from the start node
		openListNode start = new openListNode (startNode, 0, 
		      startNode.getHeuristic (goalNode));
		closedListNode startClosed = new closedListNode (startNode, 0);
		// Initialize start node's back path
		start.cameFrom.Add(startClosed);
		// Add start node to open list
		openList.Add (start);
		// Create new path, add start node to new path, add path to 
		//closed list
		List<closedListNode> path1 = new List<closedListNode>();
		path1.Add (startClosed);
		openList.Remove (start);
		closedList.Add (path1);

		// Get successors of start node
		List<openListNode> startSuccessors = getSuccessors (start, goalNode);
		// Add successors of start node to open list
		openList.AddRange (startSuccessors);
	
		// Boolean to check if goal is in closed list
		bool goalReached = false;

		//while the closed list does not contain the goal node, do...
		while (!goalReached) {
			// Check if goal is in closed list
			foreach(var sublist in closedList){
				foreach(var value in sublist){
					if(value.currSquare.x == goalNode.x && 
					   value.currSquare.y == goalNode.y){
						goalReached = true;
						return sublist;
					}
				}
			}
			// Temp list of paths to remove from closed list due to 
			//having other paths to end node that are shorter
			List<List<closedListNode>> toRemove = new 
				List<List<closedListNode>>();
			foreach(var sublist in closedList){
				foreach(var path in closedList){
					if(sublist == path){
						continue;
					}
					// Add path to remove list if there is another path 
					//that gets to the final node in a shorter way 
					if((sublist[sublist.Count - 1].currSquare == 
					    path[path.Count - 1].currSquare) && 
					   (sublist[sublist.Count - 1].soFar < 
					 path[path.Count - 1].soFar)){
						toRemove.Add(path);
					}
					else if((sublist[sublist.Count - 1].currSquare == 
					         path[path.Count - 1].currSquare) && 
					        (path[path.Count - 1].soFar < 
					 		sublist[sublist.Count - 1].soFar)){
						toRemove.Add (sublist);
					}
				}
			}

			// Remove less optimal paths from closed list
			foreach(var sublist in toRemove){
				closedList.Remove(sublist);
			}

			// Find node with least heuristic + cost in open list
			openListNode leastCost = openList[0];
			foreach(var node in openList){
				if(node.heuristicCost < leastCost.heuristicCost){
					leastCost = node;
				}
			}
			// Remove node with least heuristic + cost from open list
			openList.Remove (leastCost);
			bool addedToClosed = false;
			// Add node to its path
			foreach(var sublist in closedList){
				// Last node of sublist
				openListNode lastNode = new openListNode(
					sublist[sublist.Count - 1].currSquare, 
					sublist[sublist.Count - 1].soFar, -1);
				// Sucessor list of last node
				List<openListNode> successor = 
					getSuccessors(lastNode, goalNode);

				// Add least cost node to sublist if it's on the list of
				//successors for the last node in the sublist
				if(contain (successor, leastCost)){
					sublist.Add(new 
					    closedListNode(leastCost.currSquare, 
					    sublist[sublist.Count - 1].soFar + 
					    leastCost.currSquare.cost));
					addedToClosed = true;
				} 
			}
			// If we didn't find the path that the least cost node was in,
			//create a new path
			if(!addedToClosed){
				// Use back path to create new path in closed list
				List<closedListNode> newPath = leastCost.cameFrom;
				closedList.Add(newPath);
			}
			// Add successors of least cost node to open list
			openList.AddRange(getSuccessors(leastCost, goalNode));
		}
		// Catch in case the algorithm didn't work/is buggy
		Debug.Log ("Didn't work");
		return null;
	}
}
