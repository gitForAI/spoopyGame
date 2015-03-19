using UnityEngine;
using System.Collections;

public class RunAway : Action {

	public bool shouldMove = true;
	public aStar path;
	public Monster monster;
	public Human human;

	public override void Act(){
		Square monsterSquare = generateSquares.grid [monster.x, monster.y];
		if(monsterSquare.x - 1 >= 0){
			generateSquares.grid[monsterSquare.x - 1, monsterSquare.y].cost = 999;
		}
		if(monsterSquare.x + 1 <= generateSquares.xScale){
			generateSquares.grid[monsterSquare.x + 1, monsterSquare.y].cost = 999;
		}
		if(monsterSquare.y - 1 >= 0){
			generateSquares.grid[monsterSquare.x, monsterSquare.y - 1].cost = 999;
		}
		if(monsterSquare.y + 1 <= generateSquares.yScale){
			generateSquares.grid[monsterSquare.x, monsterSquare.y + 1].cost = 999;
		}

		if(human.roomNum == 1){
			human.humanActions.Add (new MoveToRoom2());
		}
		else if(human.roomNum == 2){
			human.humanActions.Add (new MoveToRoom3());
		}
		else if(human.roomNum == 3){
			human.humanActions.Add (new MoveToRoom4());
		}
		else{
			human.humanActions.Add (new MoveToRoom1());
		}
	}

	// Use this for initialization
	void Start () {
		path = gameObject.GetComponent<aStar> ();
		monster = gameObject.GetComponent<Monster> ();
		human = gameObject.GetComponent<Human> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
