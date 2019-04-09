using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public bool isStart = false;
	public bool isQuit = false;

	public int save = -1;
	public int load = -1;



	void OnMouseUp(){
		//print ("click");
		if (isStart) {
			GameObject.Find ("BackView").SetActive (true);
			GameObject.Find ("MenuView").SetActive (false);
			GameObject.Find ("Terrain").GetComponent<GameHandler> ().gameStart ();
		}
		if (isQuit) {
			Application.Quit ();
		}

		if (save == 0) {
			GameObject.Find ("Terrain").GetComponent<GameHandler> ().goToMenu (2);
		}
		if (save == 1) {
			GameObject.Find ("Terrain").GetComponent<SaveFiles> ().SaveToText (1);
		}
		if (save == 2) {
			GameObject.Find ("Terrain").GetComponent<SaveFiles> ().SaveToText (2);
		}
		if (save == 3) {
			GameObject.Find ("Terrain").GetComponent<SaveFiles> ().SaveToText (3);
		}
		if (save == 4) {
			GameObject.Find ("Terrain").GetComponent<SaveFiles> ().SaveToText (4);
		}


		if (load == 0) {
			GameObject.Find ("Terrain").GetComponent<GameHandler> ().goToMenu (3);
		}
		if (load == 1) {
			GameObject.Find ("Terrain").GetComponent<SaveFiles> ().LoadFromText (1);
		}
		if (load == 2) {
			GameObject.Find ("Terrain").GetComponent<SaveFiles> ().LoadFromText (2);
		}
		if (load == 3) {
			GameObject.Find ("Terrain").GetComponent<SaveFiles> ().LoadFromText (3);
		}
		if (load == 4) {
			GameObject.Find ("Terrain").GetComponent<SaveFiles> ().LoadFromText (4);
		}
	}


}
