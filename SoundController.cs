using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public GameObject Player;
	private int[] inventory = new int[11];

	public AudioClip shootingClip;
	public float vol1 = 1f;

	public AudioClip pickClip;
	public float vol2 = 1f;

	public AudioClip mainClip;
	public float vol3 = 1f;

	private AudioSource shootingSource;

	private AudioSource pickSource;

	private AudioSource mainSource;


	void Awake()
	{
		shootingSource = GetComponent<AudioSource>();
		pickSource = GetComponent<AudioSource>();
		mainSource = GetComponent<AudioSource>();

		mainSource.Play ();
	}

	private void Update()
	{
		if (Player.GetComponent<PlayerHandler> ().isAttacking ()) {

			shootingSource.PlayOneShot (shootingClip, vol1);
		}
		int[] inv = Player.GetComponent<PlayerHandler> ().getInventory ();
		for (int i = 0; i < 11; i++) {
			if (inv [i] != inventory [i]) {
				inventory [i] = inv [i];
				pickSource.PlayOneShot (pickClip, vol2);
			}
		}
	}
}
