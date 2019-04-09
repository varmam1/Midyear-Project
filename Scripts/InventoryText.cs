using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryText : MonoBehaviour {

	public Text inventory;

	// Use this for initialization
	void Start () {
		inventory = GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () {
		inventory.text = "Potions: " + gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<PlayerHandler>().getInventory()[0];
	}
}
