using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//should have these to function properly
//[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (BoxCollider))]
[RequireComponent(typeof (Rigidbody))]

public class EnemyHandler : MonoBehaviour {

	//private Animator anim;
	public GameObject player;

	//parameters for the rpg element
	private static float maxHealth = 100f;
	private float currentHealth = maxHealth;
	private float attackPower = 10f;
	private float defensePower = 10f;
	private float luck = 10f;
	private float accuracy = 10f;


	//should not be effected outside of status effects
	private float speed = 10f;
	private float jumpPower = 10f;

	public float getMaxHealth(){ return maxHealth; }
	public void setMaxHealth(float HP){ maxHealth = HP; }

	float getCurrentHealth(){ return currentHealth; }
	public void setCurrentHealth(float HP){ currentHealth = HP; }

	public float getAttackPower(){ return attackPower; }
	public void setAttackPower(float atk){ attackPower = atk; }

	float getDefensePower(){ return defensePower; }
	public void setDefensePower(float def){ defensePower = def; }

	float getLuck(){ return luck; }
	public void setLuck(float lck){ luck = lck; }

	float getAccuracy(){ return accuracy; }
	public void setAccuracy(float acc){ accuracy = acc; }

	float getSpeed(){ return speed; }
	public void setSpeed(float spd){ speed = spd; }

	float getJumpPower(){ return jumpPower; }
	public void setJumpPower(float jump){ jumpPower = jump; }

	void reset(){
		maxHealth = 100f;
		currentHealth = maxHealth;
		attackPower = 10f;
		defensePower = 10f;
		luck = 10f;
		accuracy = 10f;
	}

	public float calculateDamage(){
		//a number from 90% to 100% of the base attack power is taken and then has the status and passive items applied to calculate the damage
		return Random.Range(attackPower * .9f, attackPower * 1.1f);
	}

	//method that takes the input damage and readjusts it value based on defense
	void takeDamage(float damage, float enemyAttackPower){
		//readjust the damage based on the percentage difference of the enemy's attack power and the player effecctive defense
		damage = damage * (1 - (defensePower - enemyAttackPower) / defensePower);
		currentHealth -= damage;
		//sets HP to zero if you take too much damage
		if (currentHealth < 0) {
			currentHealth = 0;
		}
	}

	//decides whether or not the attack will hit or miss, based on your luck and the enemy's accuracy
	bool willTakeHit(int enemyAccuracy){
		if (luck > enemyAccuracy * 1.5) {
			return false;
		}
		if (luck * 1.5 < enemyAccuracy) {
			return true;
		}
		return Random.Range (0, 1) < .7;
	}

	void onCollisionEnter(Collision col){
		if (col.gameObject.CompareTag ("Bullet")) {
			print ("I am hit");
			takeDamage (player.GetComponent<PlayerHandler> ().calculateDamage (), player.GetComponent<PlayerHandler>().getAttackPower());
		}
	}

	void Start () {
		//anim = GetComponent<Animator>();
		tag = "Enemy";
		//print ("I live");
		reset ();
		player = GameObject.Find("CloroxBottle 1");
	}

	void Update () {
		//print (currentHealth);
		if (currentHealth <= 0f) {
			//print ("farewell");
			Destroy (this.gameObject);
		}



		/*
		Vector3 position = this.gameObject.transform.position;
		Vector3 playerPos = player.transform.position;
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
		*/
	}
}
