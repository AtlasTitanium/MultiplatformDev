using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneUI : MonoBehaviour {
	GUIStyle button;
	public Texture2D shoot;
	public GameObject oof;
	public Mesh meshia;
	public GameObject newCamera;

	void Start(){
		button = new GUIStyle("button");
	}

	void Update(){
		oof = GameObject.Find("Floor");
		newCamera = GameObject.FindGameObjectWithTag("NewCamera");
	}

	void OnGUI(){
		button.normal.background = shoot;
		if(GUI.Button(new Rect(Screen.width - Screen.width/4, Screen.height - Screen.height/4, Screen.width/4, Screen.height/4), "", button)){
			Shoot();
		}
	}

	void Shoot(){
		GameObject gm = new GameObject();
		gm.transform.position = newCamera.transform.position;
		gm.AddComponent<MeshFilter>().mesh = meshia;
		gm.AddComponent<MeshRenderer>();
		gm.AddComponent<SphereCollider>();
		Rigidbody rb = gm.AddComponent<Rigidbody>();
		rb.AddForce(Camera.current.transform.forward * 1000);

		oof.GetComponent<Renderer>().material.color = Color.magenta;
	}
}
