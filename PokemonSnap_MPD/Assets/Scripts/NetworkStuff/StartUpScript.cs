using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StartUpScript : MonoBehaviour {
	private GameObject player;
	public GameObject playerPrefab;
	private GameObject Vr;
	private GameObject Mobile;
	public GameObject vrStartLocation;
	public GameObject phoneStartLocation;
	public GameObject mainCamera;
	public GameObject NetworkObject;
	void Start () {
		#if UNITY_EDITOR
		player = Instantiate(playerPrefab, transform.position, vrStartLocation.transform.rotation);
		Vr = player.transform.GetChild(0).gameObject;
		Mobile = player.transform.GetChild(1).gameObject;
		Mobile.SetActive(false);
      	Debug.Log("Unity Editor");
		  
		#elif UNITY_STANDALONE_WIN
		player = Instantiate(playerPrefab, transform.position, vrStartLocation.transform.rotation);
		Vr = player.transform.GetChild(0).gameObject;
		Mobile = player.transform.GetChild(1).gameObject;
		Mobile.SetActive(false);
		Debug.Log("Stand Alone Windows");

		#elif UNITY_ANDROID
		player = Instantiate(playerPrefab, transform.position, phoneStartLocation.transform.rotation);
		Vr = player.transform.GetChild(0).gameObject;
		Mobile = player.transform.GetChild(1).gameObject;
		Debug.Log("Iphone");
		Vr.SetActive(false);
		#endif
	}
}
