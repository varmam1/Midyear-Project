using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorPhoton : Photon.MonoBehaviour {
	//Generates a maze as a 2D array

	public int rows;
	public int cols;
	public float scaleOfEachCell;
	public float heightOfWall;
	public int offset = 2;

	public bool mazeMade = false;

	public List<GameObject> planes = new List<GameObject>();

	private int[,,] maze;
	//Maze is (rows x cols x 5) where the 5 element array for each cell represents
	//The 5 element array = [WallUp, WallRight, WallDown, WallLeft, trapType] where 1 represents true and 0 represents false

	//	public GameObject floor;
	//	public GameObject wall;
	//	public GameObject enemy;

	public void setSize(int r, int c){
		rows = r;
		cols = c;
	}

	public void removeMaze(){
		for (int i = 0; i < planes.Count; i++) {
			PhotonNetwork.Destroy(planes [i]);
		}
		planes.Clear ();
		mazeMade = false;
	}

	void putMazeInUnity(){
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < cols; c++) {
				GameObject newFloor = PhotonNetwork.Instantiate ("Floor", new Vector3 (r*scaleOfEachCell, 0, c*scaleOfEachCell), Quaternion.identity, 0) as GameObject; 
				if (r == GlobalVariables.endingPosition [0] && c == GlobalVariables.endingPosition [1]) {
					newFloor.GetComponent<Renderer> ().material.color = Color.red;
				}
				planes.Add (newFloor);

				if (maze [r,c,0] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 180, 90);
					GameObject gameWall = PhotonNetwork.Instantiate ("Wall", new Vector3 (r*scaleOfEachCell - scaleOfEachCell / 2, heightOfWall/2, c*scaleOfEachCell), q, 0) as GameObject;
					planes.Add (gameWall);
				}
				if (maze [r,c,1] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 270, 90);
					GameObject gameWall = PhotonNetwork.Instantiate ("Wall", new Vector3 (r*scaleOfEachCell, heightOfWall/2, c*scaleOfEachCell + scaleOfEachCell / 2), q, 0) as GameObject;
					planes.Add (gameWall);
				}
				if (maze [r,c,2] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 0, 90);
					GameObject gameWall = PhotonNetwork.Instantiate ("Wall", new Vector3 (r*scaleOfEachCell + scaleOfEachCell / 2, heightOfWall/2, c*scaleOfEachCell), q, 0) as GameObject;
					planes.Add (gameWall);
				}
				if (maze [r,c,3] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 90, 90);
					GameObject gameWall = PhotonNetwork.Instantiate ("Wall", new Vector3 (r*scaleOfEachCell, heightOfWall/2, c*scaleOfEachCell - scaleOfEachCell / 2), q, 0) as GameObject;
					planes.Add (gameWall);
				}				
			}
		}
	}

	void initializeMaze(){
		maze = new int[rows, cols, 5];
		for (int c = 0; c < cols; c++) {
			for (int r = 0; r < rows; r++) {
				if (c == 0)
					maze [r, 0, 3] = 1;
				else if (c == cols - 1)
					maze [r, c, 1] = 1;
				if (r == 0)
					maze [r, c, 0] = 1;
				else if (r == rows - 1)
					maze [r, c, 2] = 1;
			}
		}
	}

	int[,,] generateMazeWithRecursiveDivision(int[,,] sectionedMaze, int startRowIndex, int endRowIndex, int startColIndex, int endColIndex){
		//Recursive division
		//Random row and column for a wall
		//Put a hole in the wall for each and call function again on the smaller chambers
		if (startRowIndex + 1 >= endRowIndex || startColIndex + 1 >= endColIndex) {
			return sectionedMaze;
		} 
		if (startRowIndex + offset >= endRowIndex || startColIndex + offset >= endColIndex) {
			int randColWall1 = Random.Range (startColIndex, endColIndex - 1);
			int randRowWall1 = Random.Range (startRowIndex, endRowIndex - 1);

			for (int i = startColIndex; i < endColIndex + 1; i++) {
				sectionedMaze [randRowWall1, i, 2] = 1;
				sectionedMaze [randRowWall1 + 1, i, 0] = 1;
			}
			for (int i = startRowIndex; i < endRowIndex + 1; i++) {
				sectionedMaze [i, randColWall1, 1] = 1;
				sectionedMaze [i, randColWall1 + 1, 3] = 1;
			}
			int randColSlit1 = Random.Range (startColIndex, endColIndex);
			while (randColSlit1 == randRowWall1) {
				randColSlit1 = Random.Range (startColIndex, endColIndex);
			}
			sectionedMaze [randRowWall1, randColSlit1, 2] = 0;
			sectionedMaze [randRowWall1 + 1, randColSlit1, 0] = 0;

			//2nd slit
			int randRowSlit1 = Random.Range (startRowIndex, endRowIndex);
			while (randRowSlit1 == randColWall1) {
				randRowSlit1 = Random.Range (startRowIndex, endRowIndex);
			}
			sectionedMaze [randRowSlit1, randColWall1, 1] = 0;
			sectionedMaze [randRowSlit1, randColWall1 + 1, 3] = 0;

			//Getting 3rd slit
			if (Random.Range (0, 1) == 0) {
				if (randColSlit1 > randColWall1) {
					int thirdSlit = Random.Range (startColIndex, randColWall1 - 1);
					sectionedMaze [randRowWall1, thirdSlit, 2] = 0;
					sectionedMaze [randRowWall1 + 1, thirdSlit, 0] = 0;
				} else {
					int thirdSlit = Random.Range (randColWall1 + 1, endColIndex + 1);
					sectionedMaze [randRowWall1,thirdSlit, 2] = 0;
					sectionedMaze [randRowWall1 + 1, thirdSlit, 0] = 0;
				}

			} else {
				if (randRowSlit1 > randRowWall1) {
					int thirdSlit = Random.Range (startRowIndex, randRowWall1 - 1);
					sectionedMaze [thirdSlit, randColWall1, 1] = 0;
					sectionedMaze [thirdSlit, randColWall1 + 1, 3] = 0;
				} else {
					int thirdSlit = Random.Range (randRowWall1 + 1, endRowIndex + 1);
					sectionedMaze [thirdSlit, randColWall1, 1] = 0;
					sectionedMaze [thirdSlit, randColWall1 + 1, 3] = 0;
				}
			}

			return sectionedMaze;
		} 
		int randColWall = Random.Range (startColIndex + offset, endColIndex - 1 - offset); //Put wall right of this column
		//		print("randColWall " + randColWall); 
		int randRowWall = Random.Range (startRowIndex + offset, endRowIndex - 1 - offset); //Put wall below this column
		//		print("randRowWall " + randRowWall); 

		for (int i = startColIndex; i < endColIndex + 1; i++) {
			sectionedMaze [randRowWall, i, 2] = 1;
			sectionedMaze [randRowWall + 1, i, 0] = 1;
		}
		for (int i = startRowIndex; i < endRowIndex + 1; i++) {
			sectionedMaze [i, randColWall, 1] = 1;
			sectionedMaze [i, randColWall + 1, 3] = 1;
		}
		//1st slit
		int randColSlit = Random.Range (startColIndex, endColIndex);
		while (randColSlit == randRowWall) {
			randColSlit = Random.Range (startColIndex, endColIndex);
		}
		sectionedMaze [randRowWall, randColSlit, 2] = 0;
		sectionedMaze [randRowWall + 1, randColSlit, 0] = 0;

		//2nd slit
		int randRowSlit = Random.Range (startRowIndex, endRowIndex);
		while (randRowSlit == randColWall) {
			randRowSlit = Random.Range (startRowIndex, endRowIndex);
		}
		sectionedMaze [randRowSlit, randColWall, 1] = 0;
		sectionedMaze [randRowSlit, randColWall + 1, 3] = 0;

		//Getting 3rd slit
		if (Random.Range (0, 1) == 0) {
			if (randColSlit > randColWall) {
				int thirdSlit = Random.Range (startColIndex, randColWall - 1);
				sectionedMaze [randRowWall, thirdSlit, 2] = 0;
				sectionedMaze [randRowWall + 1, thirdSlit, 0] = 0;
			} else {
				int thirdSlit = Random.Range (randColWall + 1, endColIndex + 1);
				sectionedMaze [randRowWall,thirdSlit, 2] = 0;
				sectionedMaze [randRowWall + 1, thirdSlit, 0] = 0;
			}

		} else {
			if (randRowSlit > randRowWall) {
				int thirdSlit = Random.Range (startRowIndex, randRowWall - 1);
				sectionedMaze [thirdSlit, randColWall, 1] = 0;
				sectionedMaze [thirdSlit, randColWall + 1, 3] = 0;
			} else {
				int thirdSlit = Random.Range (randRowWall + 1, endRowIndex + 1);
				sectionedMaze [thirdSlit, randColWall, 1] = 0;
				sectionedMaze [thirdSlit, randColWall + 1, 3] = 0;
			}
		}
		sectionedMaze = generateMazeWithRecursiveDivision (sectionedMaze, startRowIndex, randRowWall, startColIndex, randColWall);
		sectionedMaze = generateMazeWithRecursiveDivision (sectionedMaze, randRowWall, endRowIndex, startColIndex, randColWall);
		sectionedMaze = generateMazeWithRecursiveDivision (sectionedMaze, startRowIndex, randRowWall, randColWall, endColIndex);
		sectionedMaze = generateMazeWithRecursiveDivision (sectionedMaze, randRowWall, endRowIndex, randColWall, endColIndex);
		return sectionedMaze;
	}

	public void startMaze(){
		removeMaze ();
		mazeMade = true;
		GlobalVariables.scaleOfEachCell = scaleOfEachCell;
		initializeMaze ();
		GlobalVariables.startingPosition [0] = Random.Range (0, rows);
		GlobalVariables.startingPosition [1] = Random.Range (0, cols);
		GlobalVariables.endingPosition [0] = Random.Range (0, rows);
		GlobalVariables.endingPosition [1] = Random.Range (0, cols);

		maze = generateMazeWithRecursiveDivision (maze, 0, rows - 1, 0, cols - 1);
		putMazeInUnity ();
		GlobalVariables.maze = maze;
		GlobalVariables.row = rows;
		GlobalVariables.col = cols;

		//		for (int i = 0; i < rows / 4; i++) {
		//			Vector3 pos = new Vector3(Random.Range (0, rows) *scaleOfEachCell, 0, Random.Range (0, cols) * scaleOfEachCell);
		//			GameObject enemy = PhotonNetwork.Instantiate ("CloroxBottle 1 1 1", pos, Quaternion.identity, 0) as GameObject;
		//			print ("enemy created");
		//
		//			enemy.AddComponent<EnemyHandler> ();
		//
		//			enemy.GetComponent<EnemyHandler>().setMaxHealth(Random.Range(100f * GlobalVariables.level * .7f, 100f * GlobalVariables.level * 1.3f));
		//			enemy.GetComponent<EnemyHandler>().setCurrentHealth(enemy.GetComponent<EnemyHandler>().getMaxHealth());
		//			enemy.GetComponent<EnemyHandler> ().setAttackPower (Random.Range (10f * GlobalVariables.level * .7f, 10f * GlobalVariables.level * 1.3f));
		//			enemy.GetComponent<EnemyHandler> ().setDefensePower (Random.Range (10f * GlobalVariables.level * .7f, 10f * GlobalVariables.level * 1.3f));
		//		}
	}

	public bool isMazeMade(){
		return mazeMade;
	}

}