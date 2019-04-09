using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPotato : Photon.MonoBehaviour {

    public GameObject projectile;
    public GameObject Launcher;
	public int life = 100;

    public float sensitivityY = 10F;
    public float minimumY = -60F;
    public float maximumY = 60F;

    public float scale;
    public float force = 1000f;

	public float cooldown = .5f;
	private float currentCD = 0f;

	public Vector3 velocity;

	void OnCollisionEnter(Collision col){
		if (this.gameObject.tag == "Bullet") {
			
			gameObject.GetComponent<Rigidbody> ().AddForce (velocity * -.1f);
			velocity = Vector3.zero;
		}
	}

	void Fire(){
		Ray ray = new Ray (transform.position + new Vector3 (0, 1.5f, 0f), Launcher.transform.forward);
		Transform hitTransform; 
		Vector3 hitPoint;

		hitTransform = FindClosestHitInfo (ray, out hitPoint);

		if (hitTransform != null){
			Health h = hitTransform.GetComponent<Health> ();

			while (h == null && hitTransform.parent) {
				hitTransform = hitTransform.parent;
				h = hitTransform.GetComponent<Health> ();
			}

			if (h != null ) {
				h.GetComponent<PhotonView> ().RPC ("TakeDamage", PhotonTargets.All, 10f);
			}

		}

	

	}

	Transform FindClosestHitInfo(Ray ray, out Vector3 hitPoint){
		RaycastHit[] hits = Physics.RaycastAll (ray);
		Transform closestHit = null;
		float distance = 0;
		hitPoint = Vector3.zero;

		foreach (RaycastHit hit in hits) {
			if (hit.transform != this.transform && ( closestHit != null && hit.distance < distance )){
				closestHit = hit.transform;
				distance = hit.distance;
				hitPoint = hit.point;
			}
		}
		return closestHit;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (photonView.isMine) {
			if (currentCD <= 0 && this.gameObject.CompareTag ("Player") && Input.GetMouseButton (0)) {
				Fire ();
				GameObject bullet = PhotonNetwork.Instantiate ("Tomato", transform.position + new Vector3 (0, 1.5f, 0f) + Launcher.transform.forward , Quaternion.identity, 0) as GameObject;
//       	 	bullet.AddComponent<ShootPotato>();
//       	    bullet.AddComponent<Rigidbody>();
//				bullet.AddComponent<SphereCollider> ();
//				bullet.tag = "Bullet";
				bullet.GetComponent<ShootPotato> ().enabled = true;
				bullet.transform.localScale = new Vector3 (scale, scale, scale);
				bullet.GetComponent<ShootPotato>().velocity = Launcher.transform.forward * force; 
				currentCD = cooldown;
			} else if (currentCD > 0) {
				currentCD -= Time.deltaTime;
			}

			if (this.gameObject.CompareTag ("Bullet")) {
				transform.position += velocity * Time.deltaTime;
				if (life > 0) {
					life--;
				} else {
					PhotonNetwork.Destroy (this.gameObject);
				}
			}
		}
	}
}
