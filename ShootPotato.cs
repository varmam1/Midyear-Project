using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPotato : MonoBehaviour {

    public GameObject projectile;
    public GameObject Launcher;
	public int life = 100;

    public float sensitivityY = 10F;
    public float minimumY = -60F;
    public float maximumY = 60F;

    public float scale;
    public float force = 1000f;


    // Use this for initialization
    void Start () {
		
	}
		
	
	// Update is called once per frame
	void Update () {
		if (!GlobalVariables.inGame) {
			return;
		}

		if (this.gameObject.CompareTag("Player") && Input.GetKey (KeyCode.Space) && Launcher.GetComponent<PlayerHandler>().isAttacking())
        {
			GameObject bullet = Instantiate(projectile, transform.position + new Vector3(0, .9f, 0) + Launcher.transform.forward * .4f, Quaternion.identity) as GameObject;
            bullet.AddComponent<ShootPotato>();
            bullet.AddComponent<Rigidbody>();
			bullet.AddComponent<SphereCollider> ();
			bullet.tag = "Bullet";
            bullet.transform.localScale = new Vector3(scale, scale, scale);
			Vector3 vectorOfTravel = Launcher.transform.forward; 
            bullet.GetComponent<Rigidbody>().AddForce(vectorOfTravel * force);
        }
		if (this.gameObject.CompareTag ("Bullet")) {
			if (life > 0) {
				life--;
			} else {
				Destroy (this.gameObject);
			}
		}
    }
}
