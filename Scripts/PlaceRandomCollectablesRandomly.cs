using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRandomCollectablesRandomly : MonoBehaviour {

	public float scaleOfEachCell;
	public GameObject[] items;
	public float scale = 2f;

	//This script will place the random collectables randomly

	void placeRandomly(int item){
		int r = Random.Range (0, GlobalVariables.maze.GetLength (0));
		int c = Random.Range (0, GlobalVariables.maze.GetLength (1));
		GameObject newItem = Instantiate (items[item], new Vector3 (r*scaleOfEachCell, .5f, c*scaleOfEachCell), Quaternion.identity) as GameObject; 
		newItem.transform.localScale = new Vector3(scale,scale,scale); 
		newItem.AddComponent<CapsuleCollider> ();
		newItem.AddComponent<ItemHandler> ();
		newItem.GetComponent<ItemHandler> ().index = item;
	}
		
	//For every 30s, an item spawns
	void FixedUpdate () {
		//print (GlobalVariables.time);
		//print (GlobalVariables.time % 60f);
		if (! GlobalVariables.inGame){
			return;
		}
		if (GlobalVariables.time % 60f < .2f) {
			placeRandomly (Random.Range (0, items.Length));
		}
	}
}
