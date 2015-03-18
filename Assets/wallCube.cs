using UnityEngine;
using System.Collections;
using System;

public class wallCube : MonoBehaviour {
	public int x;
	public int y;
	// Use this for initialization
	void Start () {
		x = (int)transform.position.x;
		y = (int)transform.position.z;
		// Set x and y postion to value of square cube is on;
		// Make the units more readable and base them on the scale 
		//of the floor
		if (x >= 10) {
			x = (int)(Math.Floor ((double)(x / 10)));
		}
		else{
			x = 0;
		}
		if(y >= 10){
			y = (int)(Math.Floor ((double)(y / 10)));
		}
		else{
			y = 0;
		}

		for (int i=0; i<(int)generateSquares.xScale; i++) {
			for(int j=0; j<(int)generateSquares.yScale; j++){
				if(x == generateSquares.grid[i,j].x && y == 
				   generateSquares.grid[i,j].y){
					generateSquares.grid[i,j].isWall = true;
					generateSquares.grid[i,j].cost = 99999;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
