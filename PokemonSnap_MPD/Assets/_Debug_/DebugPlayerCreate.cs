using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DebugPlayerCreate : NetworkBehaviour {
	public GameObject Camera;
	void Start () {
		if(isLocalPlayer){
			Camera.SetActive(true);
		}
		CmdStart();
	}

	void Update(){
		if(isLocalPlayer){
			CmdStart();
		}
	}

	[Command]
	void CmdStart(){
		RpcStart();
	}

	[ClientRpc]
	void RpcStart(){
		if(isLocalPlayer){
		#if UNITY_ANDROID
		this.gameObject.tag = "MOBILE";
		this.transform.GetChild(0).gameObject.layer = 9;
		#elif UNITY_STANDALONE_WIN
		this.gameObject.tag = "VR";
		this.transform.GetChild(0).gameObject.layer = 9;
		#endif
		}
	}
}
