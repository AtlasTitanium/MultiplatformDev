using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StartTarget : NetworkBehaviour {

	public Material TargetOff;
	public Material TargetOn;
	public bool active = false;

	void Start(){
		this.GetComponent<Renderer>().material = TargetOff;
	}

	[Command]
	public void CmdHitTarget(){
		active = true;
		this.GetComponent<Renderer>().material = TargetOn;
	}
}
