using UnityEngine;
using System.Collections;
using System;

public class Square{
	// X and y positions
	public int x;
	public int y;
	// True means the square is a wall, false means the square is not a wall
	public bool isWall;
	// Cost to get to this square from an adjacent square
	public int cost;

	public Square(bool wall){
		isWall = wall;
		// Set cost high if square is a wall
		if(isWall){
			cost = 99999;
		}
		else{
			cost = 1;
		}
	}

	// Manhattan distance prediction
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
