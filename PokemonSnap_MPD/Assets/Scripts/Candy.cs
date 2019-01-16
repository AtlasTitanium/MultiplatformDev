using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {
	public int lifetimeSeconds;
	void Start () {
		StartCoroutine(DeathTime());
	}
	
	void Update () {
		if(transform.position.y <= -10){
			Destroy(this.gameObject);
		}
	}

	IEnumerator DeathTime(){
		yield return new WaitForSeconds(lifetimeSeconds);
		Destroy(this.gameObject);
	}
}
