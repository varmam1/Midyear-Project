using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


//should have these to function properly
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class PlayerHandler : Photon.MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

	public float animSpeed = 1.5f;
	public float speedScale = 1.0f;



	private Animator anim;
	private AnimatorStateInfo currentState;
	private CapsuleCollider col;
	private Rigidbody rb;
	private Vector3 velocity;
	public RectTransform healthBar;

	static int attackState = Animator.StringToHash("Base Layer.Shooting");

	//parameters for the rpg element
	private int playerLevel = 1;
	private int experienceRequired = 10;
	private int currentExperience = 0;
	private static float maxHealth = 100f;
	private float currentHealth = maxHealth;
	private float attackPower = 10f;
	private float defensePower = 10f;
	private float luck = 10f;
	private float accuracy = 10f;


	//should not be effected outside of status effects
	private float speed = 10f;
	private float jumpPower = 10f;

	//maxHealth (0, 7), attackPower (1, 8), defensePower (2, 9), speed (3, 10), jumpPower (4, 11), luck (5, 12), accuracy (6, 13) (+/-)
	//first Parameter is the percent change, second is the duration
	private float[,] status = new float[14,2];

	//11 items with a max of 100 in inventory
	//0 - Potion (30% heal)
	//1 - Panacea (clear negative status) 
	//2 - Temporary MaxHP Boost (15s)
	//3 - Temporary Attack Boost (15s)
	//4 - Temporary Defense Boost (15s)
	//5 - Temporary Speed Boost (15s)
	//6 - Temporary Jump Boost (15s)
	//7 - Temporary Luck Boost (15s)
	//8 - Temporary Accuracy Boost (15s)
	//9 - Armor Level (scales percentage-wise, level 1 = 1% boost, Level 2 = 2%, etc.)
	//10 - Spray Level (scales percentage-wise, level 1 = 1% boost, Level 2 = 2%, etc.)
	private int[] inventory = new int[11];


	//________________________________________________________________________________________________________________________________________
	//set and get methods for each parameter

	public int getPlayerLevel(){ return playerLevel; }
	public void setPlayerLevel(int level, bool withIncrement = true){
		if (withIncrement) {
			reset();
			for (int i = 1; i < level; i++) {
				setCurrentExperience( getExperienceRequired() );
				checkLevelUp();
			}
		}
		else {
			playerLevel = level;
		}
	}

	public int getExperienceRequired(){ 
		return experienceRequired; 
	}
	public void setExperienceRequired(int exp){ experienceRequired = exp; }

	public int getCurrentExperience(){
		return currentExperience; 
	}
	public void setCurrentExperience(int exp){ currentExperience = exp; }

	public float getMaxHealth(){ 
		if (GlobalVariables.inGame) {
			return maxHealth * (1f + status [0, 0] + status [7, 0]); 
		} else {
			return maxHealth;		
		}
	}
	public void setMaxHealth(float HP){ maxHealth = HP; }

	public float getCurrentHealth(){ return currentHealth; }
	public void setCurrentHealth(float HP){ currentHealth = HP; }

	public float getAttackPower(){ 
		if (GlobalVariables.inGame) {
			return attackPower * (1f + status[1,0] + status[8,0]);
		} else {
			return attackPower;		
		}

	}
	public void setAttackPower(float atk){ attackPower = atk; }

	public float getDefensePower(){ 
		if (GlobalVariables.inGame) {
			return defensePower * (1f + status[2,0] + status[9,0]); 
		} else {
			return defensePower;		
		}

	}
	public void setDefensePower(float def){ defensePower = def; }

	public float getLuck(){
		if (GlobalVariables.inGame) {
			return luck * (1f + status[5,0] + status[12,0]) ;
		} else {
			return luck;		
		}

	}
	public void setLuck(float lck){ luck = lck; }

	public float getAccuracy(){
		if (GlobalVariables.inGame) {
			return accuracy * (1f + status[6,0] + status[13,0]) ;
		} else {
			return accuracy;		
		}

	}
	public void setAccuracy(float acc){ accuracy = acc; }

	public float getSpeed(){
		if (GlobalVariables.inGame) {
			return speed * (1f + status[3,0] + status[10,0]);
		} else {
			return speed;		
		}

	}
	public void setSpeed(float spd){ speed = spd; }

	public float getJumpPower(){ 
		if (GlobalVariables.inGame) {
			return .8f * jumpPower * (1f + status[4,0] + status[11,0]); 
		} else {
			return jumpPower;		
		}

	}
	public void setJumpPower(float jump){ jumpPower = jump; }

	public float[,] getStatus(){ return status; }


	public int[] getInventory(){ return inventory; }
	public void setInventory(int index, int amount){	inventory [index] = amount;	}


	//________________________________________________________________________________________________________________________________________


	//
	void reset(){
		playerLevel = 1;
		experienceRequired = 10;
		currentExperience = 0;
		maxHealth = 100f;
		currentHealth = maxHealth;
		attackPower = 10f;
		defensePower = 10f;
		luck = 10f;
		accuracy = 10f;
	}

	public void addItem(int itemIndex){
		//print ("called");
		if (inventory [itemIndex] != 99) {
			inventory [itemIndex]++;
		}
	}

	//method to use an item, there should be some cooldown method attached to the button handler
	void UseItem(int itemIndex){
		if (inventory[itemIndex] == 0){
			//play error sound
			//print("None");
		}

		else if (itemIndex == 0){
			//use health potion
			inventory[itemIndex] -= 1;
			currentHealth += maxHealth*.3f;
			//keeps health below max health
			if (currentHealth > maxHealth){
				currentHealth = maxHealth;
			}
			print ("used");
		}

		else if (itemIndex == 1){
			//use Panacea
			inventory[itemIndex] -= 1;
			for (int i = 7; i < 12; i++){
				status [i, 0] = 0;
				status[i, 1] = 0;
			}
			print ("used");
		}

		else if (itemIndex > 1 && itemIndex < 9){
			//use on of the temporary boosts
			inventory[itemIndex] -= 1;
			status [itemIndex - 2, 0] = 20;
			status[itemIndex - 2, 1] = 15;
			print ("used");
		}

		//armor and broom level are not included since they are passive items
	}

	//method to calculate a output damage within a range
	public float calculateDamage(){
		//a number from 90% to 100% of the base attack power is taken and then has the status and passive items applied to calculate the damage
		return Random.Range(attackPower * .9f, attackPower * 1.1f) * (1 + inventory [10] * .01f) * (1 + status [1, 0] - status [8, 0]);
	}

	//method that takes the input damage and readjusts it value based on defense
	void takeDamage(float damage, float enemyAttackPower){
		//readjust the damage based on the percentage difference of the enemy's attack power and the player effecctive defense
		damage = damage * (1 - (defensePower * (1 + status[2, 0] - status[9, 0]) - enemyAttackPower) / defensePower);
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

	//method that will update the status effects on the player 
	//should be processed by a timer handler to be executed every second
	void statusUpdate(float time){
		for (int i = 0; i < status.GetLength(0); i++) {
			if (status [i, 1] == 0) {
				status [i, 0] = 0;
			} 
			else {
				status[i, 1] -= time;
			}
		}
	}

	//put in update function to constatnly check if you can level up
	//randomly increments parameters
	void checkLevelUp(){
		if (currentExperience >= experienceRequired) {
			//carries over experience
			//can level up multiple times
			currentExperience = currentExperience - experienceRequired;
			playerLevel++;
			//increments experience needed for the next level
			experienceRequired = (int)(10 * Mathf.Pow(playerLevel, 2.25f));
			maxHealth += Random.Range (10, 20);
			//refresh health
			currentHealth = maxHealth;
			//increments over parameters randomly
			attackPower += Random.Range (3, 7);
			defensePower += Random.Range (3, 7);
			luck += Random.Range (3, 7);
			accuracy += Random.Range (3, 7);
		}
	}


	public bool isAttacking(){
		AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		return currentBaseState.fullPathHash == attackState;
	}

	void onCollisionEnter(Collision col){
		if (col.gameObject.CompareTag ("Enemy")) {
			takeDamage (col.gameObject.GetComponent<EnemyHandler> ().calculateDamage (), col.gameObject.GetComponent<EnemyHandler>().getAttackPower());
		}
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		col = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();
		enabled = photonView.isMine;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (photonView.isMine) {
			if (!GlobalVariables.inGame) {
				return;
			}

			if (currentHealth > maxHealth / 3) {
				currentHealth -= 1;
			}

			healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);

			rb.useGravity = true;
			anim.speed = animSpeed;	

			if (Input.GetKey (KeyCode.Space)) {
				anim.SetBool ("isShooting", true);
				//print ("Shoot");
			} else {
				anim.SetBool ("isShooting", false);
			}

			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
				anim.SetInteger ("Moving", 1);
				print ("Move");
			} else {
				anim.SetInteger ("Moving", 0);
				//print ("Not Moving");
			}

			if (Input.GetKeyDown (KeyCode.D) && (transform.localPosition.y < .11f)) {
				print ("jump");
				rb.AddForce (Vector3.up * getJumpPower (), ForceMode.VelocityChange);
			}

			velocity = new Vector3 (0, 0, getSpeed ());
			velocity = transform.TransformDirection (velocity);
			if (Input.GetKey (KeyCode.W)) {
				velocity *= (getSpeed () * 1.0f * speedScale);		
			} else if (Input.GetKey (KeyCode.S)) {
				velocity *= (getSpeed () * (-.8f) * speedScale);	
			} else {
				velocity = Vector3.zero;
			}
			transform.localPosition += velocity * Time.fixedDeltaTime;

			if (axes == RotationAxes.MouseXAndY) {
				float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;

				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

				transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
			} else if (axes == RotationAxes.MouseX) {
				transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);
			} else {
				rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

				transform.localEulerAngles = new Vector3 (-rotationY, transform.localEulerAngles.y, 0);
			}

			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				//print ("pressed 1");
				UseItem (0);
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				UseItem (1);
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				UseItem (2);
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				UseItem (3);
			}
			if (Input.GetKeyDown (KeyCode.Alpha5)) {
				UseItem (4);
			}
			if (Input.GetKeyDown (KeyCode.Alpha6)) {
				UseItem (5);
			}
			if (Input.GetKeyDown (KeyCode.Alpha7)) {
				UseItem (6);
			}
			if (Input.GetKeyDown (KeyCode.Alpha8)) {
				UseItem (7);
			}
			if (Input.GetKeyDown (KeyCode.Alpha9)) {
				UseItem (8);
			}
					
			statusUpdate (Time.deltaTime);
		}
	}
}
