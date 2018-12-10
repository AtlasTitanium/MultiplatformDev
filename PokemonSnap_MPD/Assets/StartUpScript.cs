using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpScript : MonoBehaviour {

	public GameObject vrPrefab;
	public GameObject phonePrefab;
	public GameObject editorPrefab;
	public GameObject TestObject;
	void Start () {
		#if UNITY_EDITOR
		Instantiate(editorPrefab, transform.position, transform.rotation);
      	Debug.Log("Unity Editor");
		TestObject.GetComponent<Renderer>().material.color = Color.blue;
		#elif UNITY_STANDALONE_WIN
		Instantiate(vrPrefab, transform.position, transform.rotation);
		Debug.Log("Stand Alone Windows");
		TestObject.GetComponent<Renderer>().material.color = Color.red;
		#endif
		
		#if UNITY_ANDROID
		Instantiate(phonePrefab, transform.position, transform.rotation);
		Debug.Log("Iphone");
		TestObject.GetComponent<Renderer>().material.color = Color.green;
		#endif

		
	}
	
	void Update () {
		
	}
}
