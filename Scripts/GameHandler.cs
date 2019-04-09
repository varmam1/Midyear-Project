using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	public GameObject Player;
	public GameObject m1;
	public GameObject m2;
	public GameObject m3;

	public GameObject mCam;
	public GameObject pCam;

	public void gameStart(){
		GlobalVariables.inGame = true;
		this.gameObject.GetComponent<MazeGenerator> ().startMaze();
		Player.transform.position = new Vector3 (GlobalVariables.startingPosition[0] * GlobalVariables.scaleOfEachCell, 0.11f, GlobalVariables.startingPosition[1] * GlobalVariables.scaleOfEachCell);

		print (GlobalVariables.endingPosition [0]);
		print (GlobalVariables.endingPosition [1]);
	}

	public void nextLevel(){
		GlobalVariables.level++;
		this.gameObject.GetComponent<MazeGenerator> ().setSize (16 + GlobalVariables.level - 1, 16 + GlobalVariables.level - 1);
		gameStart();
	}

	public void goToMenu(int m){
		m1.SetActive (false);
		m2.SetActive (false);
		m3.SetActive (false);
		if (m == 1) {
			m1.SetActive (true);
		}
		if (m == 2) {
			m2.SetActive (true);
		}
		if (m == 3) {
			m3.SetActive (true);
		}

		pCam.SetActive (false);
		mCam.SetActive (true);
		GlobalVariables.inGame = false;
	}

	// Use this for initialization
	void Start () {
		GlobalVariables.inGame = false;
		GameObject.Find ("MenuView").SetActive(true);
		GameObject.Find ("Name").GetComponent<Renderer> ().material.color = Color.green;
		GameObject.Find ("Name").GetComponent<TextMesh> ().color = Color.green;
		GameObject.Find ("SaveMenu").SetActive(false);
		GameObject.Find ("LoadMenu").SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			goToMenu(1);
		}
	}
}
