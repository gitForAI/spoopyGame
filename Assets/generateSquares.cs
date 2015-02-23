using UnityEngine;
using System.Collections;

public class generateSquares : MonoBehaviour {
	//grid of squares
	public static Square [,] grid;
	//x and y size to be set dynamically on game start
	public static float xScale;
	public static float yScale;
	// Use this for initialization
	void Start () {
		//set x and y size
		xScale = transform.lossyScale[0];
		yScale = transform.lossyScale[2];
		grid = new Square[(int)xScale, (int)yScale];
		//make all new squares
		for (int i=0; i<(int)xScale; i++){
			for(int j=0; j<(int)yScale; j++){
				grid[i,j] = new Square(false);
				grid[i,j].x = i;
				grid[i,j].y = j;
				//Debug.Log("x = " + grid[i,j].x + ", y = " + grid[i,j].y);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
