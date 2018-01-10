using UnityEngine;
using System.Collections;

public class MouseHover : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Color.black;
	}
	
	void OnMouseEnter() {
		//print ("Detect");
		GetComponent<Renderer>().material.color = Color.red;
	}
	
	void OnMouseExit() {
		GetComponent<Renderer>().material.color = Color.black;
	}

	//In order for script to work, attach script to each of the texts in the menu
	//Also add box colliders to the 3D Scripts
}
