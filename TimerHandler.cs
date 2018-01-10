using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHandler : MonoBehaviour {

	void FixedUpdate() {
		if (GlobalVariables.inGame) {
			GlobalVariables.score = (GlobalVariables.timeMultiplier) * GlobalVariables.time + (GlobalVariables.levelMultiplier) * GlobalVariables.level + GlobalVariables.monstersPoints;
			GlobalVariables.time += Time.deltaTime;
		}
	}
}
