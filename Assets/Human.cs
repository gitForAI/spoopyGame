using UnityEngine;
using System.Collections;
using System;

public class Human : MonoBehaviour {
	public static int x;
	public static int y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//set x and y postion to value of square human is on
		x = (int)Math.Floor (transform.position.x);
		y = (int)Math.Floor (transform.position.z);

		if (transform.position.x >= 10) {
			x = (int)(Math.Floor ((double)(Human.x / 10)));
		}
		else{
			x = 0;
		}
		if(transform.position.z >= 10){
			y = (int)(Math.Floor ((double)(Human.y / 10)));
		}
		else{
			y = 0;
		}
	}
}
