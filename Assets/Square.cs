using UnityEngine;
using System.Collections;
using System;

public class Square{
	//x and y positions
	public int x;
	public int y;
	//true mean it is a wall, false is not a wall
	public bool isWall;
	//cost to get to this square from an adjacent square
	public int cost;
	//predicted cost to get to goal node from this square
	public int heuristic;

	public Square(bool wall){
		isWall = wall;
		//set cost high if it's a wall
		if(isWall){
			cost = 14;
		}
		else{
			cost = 1;
		}

		heuristic = -1;
	}

	//manhattan distance prediction (should be really accurate right now 
	//since we have a 5 x 5 plane with no walls...)
	public int getHeuristic(Square goal){
		return (int)(Math.Abs ((x - goal.x) + (y - goal.y)));
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
