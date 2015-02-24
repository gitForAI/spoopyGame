using UnityEngine;
using System.Collections;
using System;

public class Monster : MonoBehaviour {
	// X and y position of the monster
	public static int x;
	public static int y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Set monster's position to value of square monster is on;
		// Make units more readable and base them on the scale of the floor
		if (transform.position.x >= 10) {
			x = (int)(Math.Floor ((double)(x / 10)));
		}
		else{
			x = 0;
		}
		if(transform.position.z >= 10){
			y = (int)(Math.Floor ((double)(y / 10)));
		}
		else{
			y = 0;
		}
	}
}
