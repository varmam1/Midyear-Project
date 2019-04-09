using System.Collections;
using UnityEngine;

public static class GlobalVariables{

	//first is row of mze, second is column, third describes walls around and if traps on the current cell 
	public static int[,,] maze;

	public static int row;

	public static int col;

	//higher int = higher difficulty should NOT be zero, as it wil be a variable affecting the enemy parameters
	public static int difficulty;

	//
	public static int level = 1;

	//
	public static float time = 0f;

	//
	public static float score;

	//
	public static int monstersPoints;

	//
	public static float timeMultiplier = 1f;

	//
	public static float levelMultiplier = 1f;

	public static float scaleOfEachCell = 2f;

	public static int[] startingPosition = new int[2];

	public static int[] endingPosition = new int[2];

	public static bool inGame = false;

	static int[] positionToElement(Vector3 position){
		int[] element = new int[2];
		element [0] = Mathf.RoundToInt (position.x / 2);
		element [1] = Mathf.RoundToInt (position.z / 2);
		return element;
	}



}
