//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityStandardAssets.Characters.FirstPerson;
//
//public class FirstPersonNetwork : Photon.MonoBehaviour {
//	MouseLook cameraScript;
//	//CharacterMotor controllerScript;
//	//FPSInputController controllerScript2;
//	CharacterController charControl;
//	GameObject cam;
//
//	public void Awake()
//	{
//		cameraScript = GetComponent<MouseLook>();
//		//controllerScript = GetComponent<CharacterMotor>();
//		//controllerScript2 = GetComponent<FPSInputController>();
//		charControl = GetComponent<CharacterController>();
//	}
//
//	public void Start()
//	{
//		if (photonView.isMine)
//		{
//			//MINE: local player, simply enable the local scripts
//			cameraScript.enabled = true;
//			controllerScript.enabled = true;
//			controllerScript2.enabled = true;
//			charControl.enabled = true;
//		}
//		else
//		{
//			enabled = true;
//			cameraScript.enabled = false;
//			controllerScript.enabled = false;
//			controllerScript2.enabled = false;
//			charControl.enabled = false;
//		}
//		Screen.lockCursor = true;
//	}
//
//	public void Update()
//	{
//		if (Input.GetKeyDown (KeyCode.C))
//		{
//			Screen.lockCursor = !Screen.lockCursor;
//		}
//	}
//}