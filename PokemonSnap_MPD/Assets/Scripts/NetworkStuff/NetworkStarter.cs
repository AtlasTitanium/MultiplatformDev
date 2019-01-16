using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class NetworkStarter : MonoBehaviour {
	private uint roomSize = 2;
	private string roomName;
	private NetworkManager networkManager;
	private GUIStyle customButton;
	private GUIStyle customInputField;	

	public Texture2D Button;
	public Texture2D inpField;
	public Texture2D Background;

	void Start(){
		networkManager = NetworkManager.singleton;
		if(networkManager.matchMaker == null){
			networkManager.StartMatchMaker();
		}

		roomName = "/room name/";

		customInputField = new GUIStyle("");
		customInputField.normal.background = inpField;
		customInputField.hover.background = inpField;
		customInputField.fontSize = Screen.height/24;
		customInputField.alignment = TextAnchor.MiddleCenter;
		customInputField.normal.textColor = Color.black;

		customButton = new GUIStyle("button");
		customButton.normal.background = Button;
		customButton.hover.background = Button;
		customButton.fontSize = Screen.height/12;
	}
	void OnGUI(){
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background, ScaleMode.StretchToFill, true, 10.0F);
		roomName = GUI.TextField(new Rect(0, Screen.height - (Screen.height/8)*3, Screen.width, Screen.height/8), roomName, customInputField);
		
		//Host game Button
		if (GUI.Button(new Rect(0, Screen.height - Screen.height/4, Screen.width, Screen.height/8), "Host Game", customButton)){
			CreateRoom();
		}

		//Join game Button
		if (GUI.Button(new Rect(0, Screen.height - Screen.height/8, Screen.width, Screen.height/8), "Join Game", customButton)){
			JoinRoom();
		}		
	}

	//HOST====================================
	public void CreateRoom(){
		if(roomName != "" && roomName != null && roomName != "/room name/"){
			networkManager.matchMaker.ListMatches(0, 10, roomName, true, 0, 0, OnMatchListHost);
		}
	}
	public void OnMatchListHost(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
		if(matches.Count >= 1){
			Debug.Log("Already a Match");
		} else{
			Debug.Log("No matches found!");
			networkManager.matchMaker.CreateMatch(roomName,roomSize,true,"","","",0,0,networkManager.OnMatchCreate);
		}
    }
	//HOST=================================

	//JOIN===================================
	public void JoinRoom (){
		if(roomName != "" && roomName != null && roomName != "/room name/"){
			networkManager.matchMaker.ListMatches(0, 10, roomName, true, 0, 0, OnMatchListJoin);
		}
	}

	public void OnMatchListJoin(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
		if(matches.Count >= 1){
			Debug.Log("MatchFound!");
			networkManager.matchMaker.JoinMatch(matches[0].networkId,"","","",0,0,networkManager.OnMatchJoined);
		} else{
			Debug.Log("no matches found");
		}
    }
	//JOIN=======================================
}
