using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float maxHP = 100f;
	float currentHP;
	public RectTransform healthbar;

	// Use this for initialization
	void Start () {
		currentHP = maxHP;
	}
//
//	void OnCollisionEnter(Collision col){
//		print("A collision occurred between a " + this.gameObject.tag + " and a " + col.gameObject.tag);
//		Debug.Log ("A collision occurred between a " + this.gameObject.tag + " and a " + col.gameObject.tag);
//		if (this.gameObject.tag == "Player" && col.gameObject.tag == "Bullet"){
//			Debug.Log("A player has been hit by a bullet!");
//			TakeDamage (10f);
//		}
//	}
//
	[RPC]
	public void TakeDamage(float dmg){
		currentHP -= dmg;
		if (currentHP <= 0) {
			Die ();
		}

	}



	void Die(){
		
	}
	
	// Update is called once per frame
	void Update () {
		healthbar.sizeDelta = new Vector2 (currentHP, healthbar.sizeDelta.y);
	}
}
