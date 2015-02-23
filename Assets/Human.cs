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
	}
}
