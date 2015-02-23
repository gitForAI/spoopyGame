using UnityEngine;
using System.Collections;
using System.Linq;

public class aStar : MonoBehaviour {
	public static Square [] openList;
	public static Square [,] closedList;
	public int heuristic;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//given an x and y coordinate, returns the square with that x and y coordinate
	Square findSquare(int x, int y){
		for (int i=0; i<generateSquares.xScale; i++) {
			for(int j=0; i<generateSquares.yScale; j++){
				if(generateSquares.grid[i,j].x == x && generateSquares.grid[i,j].y == y){
					return generateSquares.grid[i,j];
				}
			}
		}
	}

	//main pathfinding algorithm
	void pathFind(Square goalNode){
		//find node where human is
		Square startNode = findSquare (Human.x, Human.y);
		openListNode start = new openListNode (startNode, startNode.getHeuristic (goalNode));
		//add start node to open list
		openList [0] = start;
		//add start node to closed list
		closedList [0] = new closedListNode (start, 0);
		//while the closed list does not contain the goal node, do...
		while (!closedList.Contains(goalNode)) {

		}
	}
}
