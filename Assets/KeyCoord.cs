using UnityEngine;
using System.Collections;
using System;

// Allows outside objects to access key's coordinates
public class KeyCoord : MonoBehaviour {

	// Key's x and y
	public int x;
	public int y;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Find x and y of key, make readable based on floor gridding
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
