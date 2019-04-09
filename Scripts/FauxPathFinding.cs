using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FauxPathFinding : MonoBehaviour {

	public string playerName;

	void Update () {
		Vector3 position = this.gameObject.transform.position;
		Vector3 playerPos = GameObject.Find (playerName).transform.position;
		Vector3 diff = playerPos - position;
		int[] element = new int[2];
		element [0] = Mathf.RoundToInt (position.x / 2);
		element [1] = Mathf.RoundToInt (position.z / 2);
		if (Mathf.Abs (diff.x) > Mathf.Abs (diff.z)) {
			if (diff.x < 0 && GlobalVariables.maze [element [0], element [1], 3] == 0) {
				this.gameObject.transform.Translate (new Vector3 (diff.x*Time.deltaTime, 0, 0));
			} else if (diff.x > 0 && GlobalVariables.maze [element [0], element [1], 1] == 0) {
				this.gameObject.transform.Translate (new Vector3 (diff.x*Time.deltaTime, 0, 0));
			}
		} else {
			if (diff.z < 0 && GlobalVariables.maze [element [0], element [1], 2] == 0) {
				this.gameObject.transform.Translate (new Vector3 (0, 0, diff.z*Time.deltaTime));
			} else if (diff.z > 0 && GlobalVariables.maze [element [0], element [1], 0] == 0) {
				this.gameObject.transform.Translate (new Vector3 (0, 0, diff.z*Time.deltaTime));
			}
		}
	}

}
