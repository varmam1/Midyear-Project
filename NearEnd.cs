using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnd : MonoBehaviour {

	public GameObject player;

	void Update () {
		Vector3 playerPos = player.transform.position;
		Vector3 endPos = new Vector3(GlobalVariables.endingPosition[0] * GlobalVariables.scaleOfEachCell, 0 , GlobalVariables.endingPosition[1] * GlobalVariables.scaleOfEachCell);
		//print ((playerPos-endPos).sqrMagnitude);
		if ((playerPos-endPos).sqrMagnitude < 1) {
			this.gameObject.GetComponent<GameHandler> ().nextLevel ();
		}
	}
}
