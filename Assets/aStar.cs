using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public class aStar : MonoBehaviour {
	public static List<openListNode> openList = new List<openListNode>();
	public static List<List<closedListNode>> closedList = new List<List<closedListNode>>();
	int heuristic;
	int openListCount = 0;

	// Use this for initialization
	void Start () {
		if (generateSquares.grid != null) {
			List<closedListNode> path = pathFind (generateSquares.grid [3, 4]);
			//Debug.Log ("path count = " + path.Count);
			foreach (var node in path) {
				Debug.Log ("PATH x = " + node.currSquare.x + ", y = " + node.currSquare.y);
				Human.x = node.currSquare.x;
				Human.y = node.currSquare.y;
				gameObject.transform.position = (node.currSquare.x, transform.position.y, node.currSquare.y);

			}
		}
		//Debug.Log ("transform x = " + gameObject.transform.position.x + ", y = " + gameObject.transform.position.y + ", z = " + gameObject.transform.position.z);
		Debug.Log ("new human x = " + Human.x + ", y = " + Human.y);
	}
	
	// Update is called once per frame
	void Update () {

	}

	List<openListNode> getSuccessors(openListNode node, Square goalNode){
		List<openListNode> successors = new List<openListNode>();
		//Debug.Log ("successor x = " + node.x + ", successor y = " + node.y);
		if (node.currSquare.x - 1 >= 0) {
			successors.Add (new openListNode(generateSquares.grid[node.currSquare.x - 1, node.currSquare.y], node.costSoFar + generateSquares.grid[node.currSquare.x - 1, node.currSquare.y].cost, generateSquares.grid[node.currSquare.x - 1, node.currSquare.y].getHeuristic(goalNode)));
		}
		if (node.currSquare.x + 1 <= generateSquares.xScale - 1) {
			successors.Add (new openListNode(generateSquares.grid[node.currSquare.x + 1, node.currSquare.y], node.costSoFar + generateSquares.grid[node.currSquare.x + 1, node.currSquare.y].cost, generateSquares.grid[node.currSquare.x + 1, node.currSquare.y].getHeuristic(goalNode)));
		}
		if (node.currSquare.y - 1 >= 0) {
			successors.Add (new openListNode(generateSquares.grid[node.currSquare.x, node.currSquare.y - 1], node.costSoFar + generateSquares.grid[node.currSquare.x, node.currSquare.y - 1].cost, generateSquares.grid[node.currSquare.x, node.currSquare.y - 1].getHeuristic(goalNode)));
		}
		if (node.currSquare.y + 1 <= generateSquares.yScale - 1) {
			successors.Add (new openListNode(generateSquares.grid[node.currSquare.x, node.currSquare.y + 1], node.costSoFar + generateSquares.grid[node.currSquare.x, node.currSquare.y + 1].cost, generateSquares.grid[node.currSquare.x, node.currSquare.y + 1].getHeuristic(goalNode)));
		}
		foreach(var thing in successors){
			thing.cameFrom.AddRange(node.cameFrom);
			closedListNode thingClosed = new closedListNode(thing.currSquare, thing.costSoFar);
			thing.cameFrom.Add (thingClosed);
		}
		return successors;
	}
	
	Boolean contain(List<openListNode> cln, openListNode oln) 
	{
		foreach (var n in cln)
		{
			if( (n.currSquare.x == oln.currSquare.x) && (n.currSquare.y == oln.currSquare.y)){
				return true;
			}
		}
		return false;
	}

	//main pathfinding algorithm
	List<closedListNode> pathFind(Square goalNode){
		//Debug.Log ("x = " + Human.x + ", y = " + Human.y);
		//find node where human is
		Square startNode = generateSquares.grid[Human.x, Human.y];
		openListNode start = new openListNode (startNode, 0, startNode.getHeuristic (goalNode));
		closedListNode startClosed = new closedListNode (startNode, 0);
		start.cameFrom.Add(startClosed);
		//add start node to open list
		openList.Add (start);
		//add start node to closed list
		List<closedListNode> path1 = new List<closedListNode>();

		path1.Add (startClosed);
		openList.Remove (start);
		closedList.Add (path1);
		//get successors of start node
		List<openListNode> startSuccessors = getSuccessors (start, goalNode);
		//add successors of start node to open list
		openList.AddRange (startSuccessors);

		//GENERIC GOAL NODE COST - NEED TO CHANGE COST WHEN ADDING TO CLOSED LIST
		//closedListNode goal = new closedListNode (goalNode, 0);
		bool goalReached = false;
		int count = 0;

		//while the closed list does not contain the goal node, do...
		while (count < 1000) {
			count++;
			//Debug.Log ("closed count = " + closedList.Count + ", open count = " + openList.Count);
			//Debug.Log ("sublist count = " +closedList[0].Count);
			//check if goal is in closed list
			foreach(var sublist in closedList){
				foreach(var value in sublist){
					if(value.currSquare.x == goalNode.x && value.currSquare.y == goalNode.y){
						goalReached = true;
						return sublist;
					}
				}
			}
			List<List<closedListNode>> toRemove = new List<List<closedListNode>>();
			foreach(var sublist in closedList){
				foreach(var path in closedList){
					if(sublist == path){
						continue;
					}
					if((sublist[sublist.Count - 1].currSquare == path[path.Count - 1].currSquare) && (sublist[sublist.Count - 1].soFar < path[path.Count - 1].soFar)){
						toRemove.Add(path);
					}
					else if((sublist[sublist.Count - 1].currSquare == path[path.Count - 1].currSquare) && (path[path.Count - 1].soFar < sublist[sublist.Count - 1].soFar)){
						toRemove.Add (sublist);
					}
				}
			}
			foreach(var sublist in toRemove){
				closedList.Remove(sublist);
			}
			//find node with least heuristic + cost in open list
			openListNode leastCost = openList[0];
			foreach(var node in openList){
				if(node.heuristicCost < leastCost.heuristicCost){
					leastCost = node;
				}
			}
			openList.Remove (leastCost);
			Boolean addedToClosed = false;
			foreach(var sublist in closedList){
				openListNode lastNode = new openListNode(sublist[sublist.Count - 1].currSquare, sublist[sublist.Count - 1].soFar, -1);
				//Debug.Log ("successor node size" + getSuccessors(lastNode, goalNode).Count);

				Debug.Log("least cost coord" + leastCost.currSquare.x + " " + leastCost.currSquare.y);

				List<openListNode> successor = getSuccessors(lastNode, goalNode);

				//Debug.Log("successor count " + successor.Count);
				/*foreach(var s in successor) {
					Debug.Log("successor coord" + s.currSquare.x + " " + s.currSquare.y);
				}*/

				if(contain (successor, leastCost)){//getSuccessors(lastNode, goalNode).Contains(leastCost)){
					Debug.Log ("adding to closed list");
					Debug.Log ("last node x = " + sublist[sublist.Count - 1].currSquare.x + ", y = " + sublist[sublist.Count - 1].currSquare.y);
					Debug.Log ("least cost x = " + leastCost.currSquare.x + ", y = " + leastCost.currSquare.y);
					sublist.Add(new closedListNode(leastCost.currSquare, sublist[sublist.Count - 1].soFar + leastCost.currSquare.cost));
					addedToClosed = true;
				} 
			}
			if(!addedToClosed){
				Debug.Log ("adding new path");

				List<closedListNode> newPath = leastCost.cameFrom;
				/*newPath.Add(startClosed);
				newPath.Add(new closedListNode(leastCost.currSquare, leastCost.currSquare.cost));*/
				closedList.Add(newPath);
			}
			openList.AddRange(getSuccessors(leastCost, goalNode));
		}
		Debug.Log ("didn't work");
		return null;
	}
}
