using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerController : NetworkBehaviour {
	private GameObject WaitArea;
	private GameObject StartLocation;
	private GameObject EndLocation;
	private GameObject Vr;
	private GameObject Mobile;
	void Start(){
		#if UNITY_EDITOR
		if(isServer){
			RpcChangePlayer(true,false);
		}
		if(isClient){
			CmdChangePlayer(true,false);
		} else {
			Debug.Log("not online");
		}

		#elif UNITY_STANDALONE_WIN
		if(isServer){
			RpcChangePlayer(true,false);
		}
		if(isClient){
			CmdChangePlayer(true,false);
		} else {
			Debug.Log("not online");
		}

		#elif UNITY_ANDROID
		if(isServer){
			CmdChangePlayer(false,true);
		}
		if(isClient){
			CmdChangePlayer(false,true);
		} else {
			Debug.Log("not online");
		}
		#endif
	}

	[Command]
	public void CmdChangePlayer(bool vr, bool mobile){
		RpcChangePlayer(vr,mobile);
	}

	[ClientRpc]
	public void RpcChangePlayer(bool vr, bool mobile){
		if(vr){
			Mobile = transform.GetChild(1).gameObject;
			Debug.Log(Mobile);
			Mobile.SetActive(false);

			Debug.Log("StartVr");
			this.GetComponent<NetworkStarter>().enabled = false;

			WaitArea = GameObject.FindGameObjectWithTag("VrWait");
			//StartLocation = GameObject.FindGameObjectWithTag("VrStart");
			//EndLocation = GameObject.FindGameObjectWithTag("VrEnd");

			if(localPlayerAuthority && WaitArea != null){
				transform.position = WaitArea.transform.position;
			}
		}
		if(mobile){
			Vr = transform.GetChild(0).gameObject;
			Debug.Log(Vr);
			Vr.SetActive(false);

			Debug.Log("StartMobile");
			this.GetComponent<NetworkStarter>().enabled = false;

			WaitArea = GameObject.FindGameObjectWithTag("MobileWait");
			//StartLocation = GameObject.FindGameObjectWithTag("MobileStart");
			//EndLocation = GameObject.FindGameObjectWithTag("MobileEnd");

			if(localPlayerAuthority && WaitArea != null){
				transform.position = WaitArea.transform.position;
			}
		}
	}
}
