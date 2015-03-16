using UnityEngine;
using System.Collections;

public class generateSquares : MonoBehaviour {
	// Grid of squares
	public static Square [,] grid;
	// X and y size to be set dynamically by size of plane (floor) 
	//on game start
	public static float xScale;
	public static float yScale;

	// Use this for initialization
	void Start () {
		// Set x and y size
		xScale = transform.lossyScale[0];
		yScale = transform.lossyScale[2];
		grid = new Square[(int)xScale, (int)yScale];
		// Make all new squares and put into grid
		for (int i=0; i<(int)xScale; i++){
			for(int j=0; j<(int)yScale; j++){
				grid[i,j] = new Square(false);
				grid[i,j].x = i;
				grid[i,j].y = j;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
