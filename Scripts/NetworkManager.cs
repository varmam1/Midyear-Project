using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	public GameObject cam;
	public bool offlineMode = false;



	// Use this for initialization
	void Start () {
		Connect();
	}

	void Connect(){
		if (offlineMode) {
			PhotonNetwork.offlineMode = true;
			OnJoinedLobby ();
		} 
		else {
			PhotonNetwork.ConnectUsingSettings ("V1.0.0");
		}
	}

	void OnGUI(){
		Debug.Log ("Loading");
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ()); 
	}

	private void OnConnectedToMaster(){
		Debug.Log ("Joining lobby");
		PhotonNetwork.JoinLobby ();
	}

	void OnJoinedLobby(){
		Debug.Log ("Joined lobby");
		PhotonNetwork.JoinRandomRoom();

	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log ("Failed to join room");
		PhotonNetwork.CreateRoom (null);
		//gameObject.GetComponent<MazeGeneratorPhoton> ().startMaze ();

	}

	void OnJoinedRoom(){
		Debug.Log ("Joined room");
		if (PhotonNetwork.isMasterClient) {
			gameObject.GetComponent<MazeGeneratorPhoton> ().startMaze ();
		}
		SpawnMyPlayer ();
	}



	void SpawnMyPlayer(){
		Debug.Log ("Spawned Player");
		GameObject myPlayer = (GameObject) PhotonNetwork.Instantiate ("unitychan", new Vector3 (0f, 1f, 0f), Quaternion.identity, 0);
		cam.SetActive (false);
		myPlayer.GetComponent<FaceUpdate> ().enabled = true;
		myPlayer.GetComponent<UnityChanControlScriptWithRgidBody> ().enabled = true;
		myPlayer.GetComponent<ShootPotato> ().enabled = true;
		myPlayer.transform.Find ("Camera").gameObject.SetActive (true);
	}


}
