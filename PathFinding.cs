using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* A* Pathfinding:
 * Score board with 2 ways:
 * First score how far each cell is from the Enemy using manhattan distances
 * Then do the same with the player
 * Add it together
 * Make enemy go in the direction of the lowest score out of the 4 ways they can go around them
 * Must have player have the "player" tag
 * Attach this script to the enemy
 */

public class PathFinding : MonoBehaviour {

	int[,] scoreBoard(int player){
		/*
		 * Parameters: 
		 *     Player (int) will be the player that it is taking it with respect to. 0 for enemy and 1 for player
		 * 
		 * Returns:
		 * 	   A (rows x cols) length 2D array with the scores of each cell in each of the cells
		 */

		//Initialize the scoredMaze and get the current cell the enemy is in
		int[,] scoredMaze = new int[GlobalVariables.row, GlobalVariables.col];
		Vector3 currentPos = new Vector3 ();
		if (player == 0)
			currentPos = this.gameObject.transform.position;
		else
			currentPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		int[] element = new int[2];
		element [0] = Mathf.RoundToInt (currentPos.x / 2);
		element [1] = Mathf.RoundToInt (currentPos.z / 2);

		if (player == 0) {
			int[,,] maze = GlobalVariables.maze;
			if (maze [element [0], element [1], 0] == 0) {
				scoredMaze [element [0], element [1] - 1] = 1;
			} else {
				scoredMaze [element [0], element [1] - 1] = 1000;
			}
			if (maze [element [0], element [1], 1] == 0) {
				scoredMaze [element [0] + 1, element [1]] = 1;
			} else {
				scoredMaze [element [0] + 1, element [1]] = 1000;
			}
			if (maze [element [0], element [1], 2] == 0) {
				scoredMaze [element [0], element [1] + 1] = 1;
			} else {
				scoredMaze [element [0], element [1] + 1] = 1000;
			}
			if (maze [element [0], element [1], 3] == 0) {
				scoredMaze [element [0] - 1, element [1]] = 1;
			} else {
				scoredMaze [element [0] - 1, element [1]] = 1000;
			}
			return scoredMaze;
		} else {

			//Assuming no walls
			for (int i = 0; i < GlobalVariables.row; i++) {
				for (int j = 0; j < GlobalVariables.col; j++) {
					scoredMaze [i, j] = Mathf.Abs (i - element [0]) + Mathf.Abs (j - element [1]);
				}
			}

			return scoredMaze;
		}
	}

	int[,] addBoards(int[,] playerBoard, int[,] enemyBoard){
		/*
		 * Parameters:
		 *     playerBoard (int[,]) will be the score of the player's board
		 * 	   enemyBoard (int[,]) will be the score of the enemy's board
		 * 
		 * Returns:
		 *     A board with the parameters added together like matrices
		 */

		int[,] added = new int[GlobalVariables.row, GlobalVariables.col];

		for (int i = 0; i < GlobalVariables.row; i++) {
			for (int j = 0; j < GlobalVariables.col; j++) {
				added [i, j] = playerBoard [i, j] + enemyBoard [i, j];
			}
		}

		return added;
	}

	int[] direction(int[,] board){
		/* 
		 * Parameters:
		 *     board (int[,]) will be the scored board that was the added one
		 * 
		 * Returns:
		 *     An array of 2 ints. [0,1], [0,-1], [1, 0] or [-1,0] that represent the direction of the enemy
		 */

		Vector3 currentPos = this.gameObject.transform.position; 
		int x = Mathf.RoundToInt (currentPos.x / 2);
		int z = Mathf.RoundToInt (currentPos.z / 2);

		if(board[x-1,z] <= board[x+1,z] && board[x-1,z] <= board[x,z+1] && board[x-1,z] <= board[x,z-1]){
			int[] a = new int[2];
			a[0] = -1;
			a[1] = 0;
			return a;
		} else if(board[x+1,z] <= board[x-1,z] && board[x+1,z] <= board[x,z+1] && board[x+1,z] <= board[x,z-1]){
			int[] a = new int[2];
			a[0] = 1;
			a[1] = 0;
			return a;
		} else if(board[x,z + 1] <= board[x+1,z] && board[x,z+1] <= board[x-1,z] && board[x,z+1] <= board[x,z-1]){
			int[] a = new int[2];
			a[0] = 0;
			a[1] = 1;
			return a;
		} else {
			int[] a = new int[2];
			a[0] = 0;
			a[1] = -1;
			return a;
		}

	}

	int[] getDirection(){
		return direction (addBoards (scoreBoard (0), scoreBoard (1)));
	}
}
