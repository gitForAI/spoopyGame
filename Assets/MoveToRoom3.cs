﻿using UnityEngine;
using System.Collections;

public class MoveToRoom3 : Action {

	public override void Act(){

	}

	// Use this for initialization
	void Start () {
		x = (int)Mathf.Round (Random.Range (4, 6));
		y = (int)Mathf.Round (Random.Range (4, 6));
		shouldMove = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
