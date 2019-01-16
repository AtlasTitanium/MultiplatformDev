using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DebugStart : NetworkBehaviour {

	public GameObject floor;
	public GameObject VR;
	public GameObject MOBILE;
	public GameObject VrCameraLocation;
	public GameObject MobileCameraLocation;

	void Start () {
		VR = GameObject.FindGameObjectWithTag("VR");
		MOBILE = GameObject.FindGameObjectWithTag("MOBILE");

		#if UNITY_ANDROID
		Debug.Log("IsMobile");
		floor.GetComponent<Renderer>().material.color = Color.blue;  
		#elif UNITY_STANDALONE_WIN
		Debug.Log("IsStandalone");
		floor.GetComponent<Renderer>().material.color = Color.red;  
		#endif			

		CmdUpdateCamera();
	}

	void Update(){
		VR = GameObject.FindGameObjectWithTag("VR");
		MOBILE = GameObject.FindGameObjectWithTag("MOBILE");
		CmdUpdateCamera();
		MobileCameraLocation.transform.position = new Vector3(Mathf.PingPong(Time.time, 3), MobileCameraLocation.transform.position.y, MobileCameraLocation.transform.position.z);
		VrCameraLocation.transform.position = new Vector3(Mathf.PingPong(Time.time, 3), VrCameraLocation.transform.position.y, VrCameraLocation.transform.position.z);
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

	[Command]
	void CmdUpdateCamera(){
		RpcUpdateCamera();
	}

	[ClientRpc]
	void RpcUpdateCamera(){
		if(MOBILE != null){
			MOBILE.transform.GetChild(0).transform.position = MobileCameraLocation.transform.position;
			MOBILE.transform.GetChild(0).transform.rotation = MobileCameraLocation.transform.rotation;
		} else {
			Debug.Log("noMobileFound");
		}
		if(VR != null){
			VR.transform.GetChild(0).transform.position = VrCameraLocation.transform.position;
			VR.transform.GetChild(0).transform.rotation = VrCameraLocation.transform.rotation;
		} else {
			Debug.Log("noVRFound");
		}
	}
}
