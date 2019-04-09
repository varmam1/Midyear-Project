using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {

	Vector3 realPos = Vector3.zero;
	Quaternion realRot = Quaternion.identity;

	public float sync = .005f;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			//Nothing
		} 
		else {
			transform.position = Vector3.Lerp (transform.position, realPos, sync);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRot, sync);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
			if (anim != null) {
				stream.SendNext (anim.GetFloat("Speed"));
				stream.SendNext (anim.GetFloat("Direction"));
				stream.SendNext (anim.GetBool("Rest"));
				stream.SendNext (anim.GetBool("Jump"));
			}
		}
		else{
			realPos = (Vector3)stream.ReceiveNext ();
			realRot = (Quaternion)stream.ReceiveNext ();
			if (anim != null) {
				anim.SetFloat("Speed", (float) stream.ReceiveNext ());
				anim.SetFloat("Direction", (float) stream.ReceiveNext ());
				anim.SetBool("Rest", (bool) stream.ReceiveNext ());
				anim.SetBool("Jump", (bool) stream.ReceiveNext ());
			}
		}
	}
}
