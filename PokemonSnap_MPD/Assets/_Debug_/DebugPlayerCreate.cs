using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DebugPlayerCreate : NetworkBehaviour {
	private GameObject floor;
	public GameObject Camera;
	private GameObject CameraLocation;

	void Start () {
		floor = GameObject.FindGameObjectWithTag("Debug");
		#if UNITY_ANDROID
		Debug.Log("IsMobile");
		CameraLocation = GameObject.FindGameObjectWithTag("MobileStart");
		floor.GetComponent<Renderer>().material.color = Color.blue;  
		#elif UNITY_STANDALONE_WIN
		Debug.Log("IsStandalone");
		CameraLocation = GameObject.FindGameObjectWithTag("VrStart");
		floor.GetComponent<Renderer>().material.color = Color.red;  
		#endif

		if(CameraLocation != null){
			Camera.transform.position = CameraLocation.transform.position;
			Camera.transform.rotation = CameraLocation.transform.rotation;
		} else {
			Debug.Log("nothing found");
		}
	}

	void OnApplicationQuit(){
		if(isServer){
			NetworkManager.singleton.StopHost();
			NetworkManager.singleton.StopServer();
		}
		if(isClient){
			NetworkManager.singleton.StopClient();
		}
	}
}
