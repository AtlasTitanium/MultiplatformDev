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
	public float speed = 2.0f;
	private Vector3 currenPos;
	void Start(){
		#if UNITY_STANDALONE_WIN
		Mobile.SetActive(false);
		StartPos.position = new Vector3(StartPos.position.x, StartPos.position.y, StartPos.position.z);
		for(int i = 0; i < Positions.Count; i++){
			Positions[i].position = new Vector3(Positions[i].position.x, Positions[i].position.y, Positions[i].position.z);
		}
		#elif UNITY_ANDROID
		StartPos.position = new Vector3(StartPos.position.x, StartPos.position.y + 4, StartPos.position.z);
		for(int i = 0; i < Positions.Count; i++){
			Positions[i].position = new Vector3(Positions[i].position.x, Positions[i].position.y + 5, Positions[i].position.z);
		}
		Vr.SetActive(false);
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
}
