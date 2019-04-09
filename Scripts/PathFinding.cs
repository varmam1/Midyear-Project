using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinding : MonoBehaviour {

	public string playerName;
	public int[,] scoreMaze;
	private int levelTracker;

	public Stack<Vector2> path;
	public Vector2 currentMove;


	void ScoreBoardRecursively(int[] currentPos, int[] startingPos){



		int[] upPos = new int[2];
		int[] rightPos = new int[2];
		int[] downPos = new int[2];
		int[] leftPos = new int[2];

		upPos [0] = currentPos [0] - 1; 
		upPos [1] = currentPos [1];

		rightPos [0] = currentPos [0]; 
		rightPos [1] = currentPos [1] + 1;

		downPos [0] = currentPos [0] + 1; 
		downPos [1] = currentPos [1];

		leftPos [0] = currentPos [0]; 
		leftPos [1] = currentPos [1] - 1;

		bool canMoveUp = upPos[0] >= 0 && GlobalVariables.maze[currentPos[0], currentPos[1], 0] == 0;
		bool canMoveRight = rightPos[1] < GlobalVariables.col && GlobalVariables.maze[currentPos[0], currentPos[1], 1] == 0;
		bool canMoveDown = downPos[0] < GlobalVariables.row && GlobalVariables.maze [currentPos [0], currentPos [1], 2] == 0;
		bool canMoveLeft = leftPos[1] >= 0 && GlobalVariables.maze[currentPos[0], currentPos[1], 3] == 0;

		bool upCase = false;
		bool rightCase = false;
		bool downCase = false;
		bool leftCase = false;

		if (currentPos[0] == startingPos[0] && currentPos[1] == startingPos[1]) {
			scoreMaze [currentPos [0], currentPos [1]] = 1;
		}

		if (scoreMaze[currentPos[0], currentPos[1]] != 0){
			if (canMoveUp) {
				if (scoreMaze [upPos [0], upPos [1]] != 0) {
					upCase = true;
				} else {
					upCase = false;
				}
			} else {
				upCase = true;
			}

			if (canMoveRight) {
				if (scoreMaze [rightPos [0], rightPos [1]] != 0) {
					rightCase = true;
				} else {
					rightCase = false;
				}
			} else {
				rightCase = true;
			}

			if (canMoveDown) {
				if (scoreMaze [downPos [0], downPos [1]] != 0) {
					downCase = true;
				} else {
					downCase = false;
				}
			} else {
				downCase = true;
			}

			if (canMoveLeft) {
				if (scoreMaze [leftPos [0], leftPos [1]] != 0) {
					leftCase = true;
				} else {
					leftCase = false;
				}
			} else {
				leftCase = true;
			}
		}

		if (upCase && rightCase && downCase && leftCase) {
			return;
		}
			

		if (canMoveUp && (scoreMaze [upPos[0], upPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [upPos[0], upPos[1]] == 0)){
			scoreMaze [upPos[0], upPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
			ScoreBoardRecursively (upPos, startingPos);
		}
		if (canMoveRight && (scoreMaze [rightPos[0], rightPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [rightPos[0], rightPos[1]] == 0)){
			scoreMaze [rightPos[0], rightPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
			ScoreBoardRecursively (rightPos, startingPos);
		}
		if (canMoveDown && (scoreMaze [downPos[0], downPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [downPos[0], downPos[1]] == 0)){
			scoreMaze [downPos[0], downPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
			ScoreBoardRecursively (downPos, startingPos);
		}
		if (canMoveUp && (scoreMaze [leftPos[0], leftPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [leftPos[0], leftPos[1]] == 0)){
			scoreMaze [leftPos[0], leftPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
			ScoreBoardRecursively (leftPos, startingPos);
		}
			
		/*
		if (canMoveUp) {
			ScoreBoardRecursively (upPos, startingPos);
		}
		if (canMoveRight) {
			ScoreBoardRecursively (rightPos, startingPos);
		}
		if (canMoveDown) {
			ScoreBoardRecursively (downPos, startingPos);
		}
		if (canMoveLeft) {
			ScoreBoardRecursively (leftPos, startingPos);
		}
		*/




	}

	bool boardIsScored(){
		for (int i = 0; i < scoreMaze.GetLength (0); i++) {
			for (int j = 0; j < scoreMaze.GetLength (1); j++) {
				if (scoreMaze [i, j] != 0) {
					return false;
				}
			}
		}
		return true;
	}

	Stack<Vector2> getPathRecursively(int[] startingPos, int[] endingPos, Stack<Vector2> p){
		Stack<Vector2> path = p;
		if (!boardIsScored ()) {
			return null;
		}
		if (startingPos [0] == endingPos [0] && startingPos [1] == endingPos [1]) {
			return path;
		} else {
			int[] upPos = new int[2];
			int[] rightPos = new int[2];
			int[] downPos = new int[2];
			int[] leftPos = new int[2];

			upPos [0] = endingPos [0] + 1; 
			upPos [1] = endingPos [1];
			rightPos [0] = endingPos [0]; 
			rightPos [1] = endingPos [1] + 1;
			downPos [0] = endingPos [0] - 1; 
			downPos [1] = endingPos [1];
			leftPos [0] = endingPos [0]; 
			leftPos [1] = endingPos [1] - 1;
			if (upPos[0] >= 0 && GlobalVariables.maze[endingPos[0], endingPos[1], 0] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[upPos[0], upPos[1]]){
				path.Push (Vector2.down);
				getPathRecursively (startingPos, upPos, path);
			}
			if (rightPos[0] < GlobalVariables.col && GlobalVariables.maze[endingPos[0], endingPos[1], 1] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[rightPos[0], rightPos[1]]){
				path.Push (Vector2.left);
				getPathRecursively (startingPos, rightPos, path);
			}
			if (downPos[0] < GlobalVariables.row && GlobalVariables.maze[endingPos[0], endingPos[1], 2] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[downPos[0], downPos[1]])
			{
				path.Push(Vector2.up);
				getPathRecursively(startingPos, downPos, path);
			}
			if (leftPos[0] >= 0 && GlobalVariables.maze[endingPos[0], endingPos[1], 3] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[leftPos[0], leftPos[1]])
			{
				path.Push(Vector2.right);
				getPathRecursively(startingPos, leftPos, path);
			}
		}
		return path;
	}

	String ScoretoString(){
		String s = "";
		for (int i = 0; i < GlobalVariables.row; i++) {
			for (int j = 0; j < GlobalVariables.col; j++) {
				s = s + scoreMaze [i, j];
				s = s + " ";
			}
			s += "\n";
		}
		return s;
	}

	String PathToString(){
		String s = "";;
		Vector2[] p = path.ToArray ();
		for (int i = 0; i < path.Count; i++) {
			if (p [i].Equals (Vector2.up)) {
				s += "up";
			}
			if (p [i].Equals (Vector2.right)) {
				s += "right";
			}
			if (p [i].Equals (Vector2.up)) {
				s += "down";
			}
			if (p [i].Equals (Vector2.up)) {
				s += "left";
			}
			s += "\n";
		}
		return s;
	}

	void Start(){
		path = new Stack<Vector2> ();
		currentMove = Vector2.zero;
		levelTracker = 0;
	}

	// Update is called once per frame
	void Update () {
		if (!GlobalVariables.inGame) {
			return;
		}
		if (levelTracker != GlobalVariables.level) {
			levelTracker = GlobalVariables.level;
			scoreMaze = new int[GlobalVariables.maze.GetLength(0), GlobalVariables.maze.GetLength(1)];
		}

		if (path.Count == 0 || path.Equals(null)) {
			Vector3 position = this.gameObject.transform.position;
			int[] element = new int[2];
			element [0] = Mathf.RoundToInt (position.x / 2);
			element [1] = Mathf.RoundToInt (position.z / 2);

			Vector3 playerPos = GameObject.Find (playerName).transform.position;
			int[] playerPosition = new int[2];
			playerPosition [0] = Mathf.RoundToInt (playerPos.x / 2);
			playerPosition [1] = Mathf.RoundToInt (playerPos.z / 2);

			ScoreBoardRecursively (element, element);
			path = getPathRecursively (element, playerPosition, path);

			print (ScoretoString ());
			print (PathToString ());
		} 

		if (currentMove.Equals (Vector2.zero)) {
			currentMove = path.Pop ();
		} else {
			Vector3 bestPath3 = new Vector3 (currentMove.x, 0, currentMove.y);
			this.gameObject.transform.Translate (bestPath3 * 1f / 60);
			currentMove -= (currentMove *  1f / 60);
		}

	}
}
