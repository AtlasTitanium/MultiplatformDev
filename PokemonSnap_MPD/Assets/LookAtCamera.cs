using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
	public GameObject pc;
	void Start(){
		pc = GameObject.FindGameObjectWithTag("Player");
	}
	void Update () {
		transform.LookAt(pc.transform);
	}
}
