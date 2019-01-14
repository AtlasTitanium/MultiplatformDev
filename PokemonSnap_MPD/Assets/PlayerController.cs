using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	private GameObject WaitArea;
	private GameObject StartLocation;
	private GameObject EndLocation;
	private GameObject Vr;
	private GameObject Mobile;
	void Start(){
		#if UNITY_EDITOR
		Mobile = transform.GetChild(1).gameObject;
		Mobile.SetActive(false);

		if(isServer || isClient){
			Debug.Log("Yeet");
			this.GetComponent<NetworkStarter>().enabled = false;

			WaitArea = GameObject.FindGameObjectWithTag("VrWait");
			StartLocation = GameObject.FindGameObjectWithTag("VrStart");
			EndLocation = GameObject.FindGameObjectWithTag("VrEnd");

			if(localPlayerAuthority && WaitArea != null){
				transform.position = WaitArea.transform.position;
			}
		}

		#elif UNITY_STANDALONE_WIN
		Mobile = transform.GetChild(1).gameObject;
		Mobile.SetActive(false);

		if(isServer || isClient){
			this.GetComponent<NetworkStarter>().enabled = false;

			WaitArea = GameObject.FindGameObjectWithTag("VrWait");
			StartLocation = GameObject.FindGameObjectWithTag("VrStart");
			EndLocation = GameObject.FindGameObjectWithTag("VrEnd");

			if(localPlayerAuthority && WaitArea != null){
				transform.position = WaitArea.transform.position;
			}
		}

		#elif UNITY_ANDROID
		Vr = transform.GetChild(0).gameObject;
		Vr.SetActive(false);

		if(isServer || isClient){
			this.GetComponent<NetworkStarter>().enabled = false;

			WaitArea = GameObject.FindGameObjectWithTag("MobileWait");
			StartLocation = GameObject.FindGameObjectWithTag("MobileStart");
			EndLocation = GameObject.FindGameObjectWithTag("MobileEnd");

			if(localPlayerAuthority && WaitArea != null){
				transform.position = WaitArea.transform.position;
			}
		}
		#endif
	}
}
