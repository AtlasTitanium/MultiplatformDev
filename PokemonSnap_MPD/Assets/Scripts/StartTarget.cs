using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTarget : MonoBehaviour {

	public Material TargetOff;
	public Material TargetOn;
	public bool active = false;

	void Start(){
		this.GetComponent<Renderer>().material = TargetOff;
	}

	void OnCollisionEnter (Collision col){
        if(col.gameObject.GetComponent<Candy>()){
			this.GetComponent<Renderer>().material = TargetOff;
			active = true;
		}
    }
}
