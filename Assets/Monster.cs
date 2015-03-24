using UnityEngine;
using System.Collections;
using System;

public class Monster : MonoBehaviour {
	// Monster x and y position
	public int x;
	public int y;
	// Object reference to allow call to pathfinding algorithm
	public aStar path;

	// Use this for initialization
	void Start () {
		path = gameObject.GetComponent<aStar> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Set x and y postion to value of square monster is on;
		// Make the units more readable and base them on the scale of 
		//the floor
		if (transform.position.x >= 10) {
			x = (int)(Math.Floor ((double)(transform.position.x / 10)));

		}
		else{
			x = 0;
		}
		if(transform.position.z >= 10){
			y = (int)(Math.Floor ((double)(transform.position.z / 10)));
		}
		else{
			y = 0;
		}
	}
}
