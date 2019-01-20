using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public GameObject Vr;
	public GameObject Mobile;
	public Transform StartPos;
	public List<Transform> Positions = new List<Transform>();
	public Transform WaitArea;
	public StartTarget Target;
	private float startTime;
	private float journeyLength;
	private bool journeying;
	private int countingPositions = 0;
	public float speed = 5.0f;
	private Vector3 currenPos;
	public List<GameObject> pictures = new List<GameObject>();

	public LayerMask targetMask;
    public LayerMask obstacleMask;
	void Awake(){
		#if UNITY_STANDALONE_WIN
		Mobile.SetActive(false);
		StartPos.position = new Vector3(StartPos.position.x, StartPos.position.y-1, StartPos.position.z);
		for(int i = 0; i < Positions.Count; i++){
			Positions[i].position = new Vector3(Positions[i].position.x, Positions[i].position.y-1, Positions[i].position.z);
		}
		#elif UNITY_ANDROID
		Vr.SetActive(false);
		StartPos.position = new Vector3(StartPos.position.x, StartPos.position.y + 2, StartPos.position.z);
		for(int i = 0; i < Positions.Count; i++){
			Positions[i].position = new Vector3(Positions[i].position.x, Positions[i].position.y + 2, Positions[i].position.z);
		}
		#endif
	}

	void Update(){
		if(Target.active){
			transform.position = StartPos.position;
			startTime = Time.time;
			journeying = true;
			Target.active = false;
			currenPos = StartPos.position;
			journeyLength = Vector3.Distance(StartPos.position, Positions[countingPositions].position);
		}

		if(journeying){
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(currenPos, Positions[countingPositions].position, fracJourney);
			if(transform.position == Positions[countingPositions].position){
				currenPos = Positions[countingPositions].position;
				countingPositions++;
				if(countingPositions == Positions.Count){
					transform.position = WaitArea.position;
					countingPositions = 0;
					journeying = false;
				}
				journeyLength = Vector3.Distance(currenPos, Positions[countingPositions].position);
				startTime = Time.time;
			}
		}
	}

	void OnGUI(){
		GUIStyle text = new GUIStyle();
		text.fontSize = Screen.height/10;
		GUI.Box(new Rect(0,0,Screen.width/4,Screen.height/8),"amount of picture: "+pictures.Count,text);
		if(pictures.Count >= 1){
			GUI.Box(new Rect(0,Screen.height/8,Screen.width/4,Screen.height/8),"poke in last picture: "+pictures[pictures.Count - 1].GetComponent<Picture>().amountInPic,text);
		}
	}
}
