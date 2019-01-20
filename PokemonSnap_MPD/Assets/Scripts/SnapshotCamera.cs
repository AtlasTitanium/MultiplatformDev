using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class SnapshotCamera : MonoBehaviour {
	public PlayerController player;
	private Camera snapCam;
	int resWidth = 256;
	int resHeight = 256;
	bool takeScreenshotOnNextFrame = false;
	int amountPicture;
	void Awake () {
		snapCam = GetComponent<Camera>();
	}

	private void OnPostRender(){
		if(takeScreenshotOnNextFrame){
			takeScreenshotOnNextFrame = false;
			RenderTexture renderTexture = snapCam.targetTexture;
			
			Texture2D newPic = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24,false);
			Rect rect = new Rect(0,0,renderTexture.width, renderTexture.height);
			newPic.ReadPixels(rect,0,0);
			newPic.name = "Oof";
			Debug.Log(newPic);
			newPic.Apply(newPic);

			GameObject gm = new GameObject();
			gm.transform.parent = GameObject.FindGameObjectWithTag("Debug").transform;
			gm.transform.position = GameObject.FindGameObjectWithTag("Debug").transform.position;
			gm.AddComponent<RectTransform>();
			gm.AddComponent<CanvasRenderer>();
			gm.AddComponent<RawImage>().texture = newPic;
			gm.AddComponent<Picture>().amountInPic = amountPicture;
			newPic.filterMode = FilterMode.Point;
			player.pictures.Add(gm);

			RenderTexture.ReleaseTemporary(renderTexture);
			snapCam.targetTexture = null;
		}
	}
	
	public void Snap(int amount) {
		Debug.Log(amount);
		snapCam.targetTexture = RenderTexture.GetTemporary(resWidth,resHeight, 24);
		takeScreenshotOnNextFrame = true;
		amountPicture = amount;
	}

}

/*
void LateUpdate(){
		// if(snapCam.gameObject.activeInHierarchy){
		// 	Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
		// 	snapCam.Render();
		// 	RenderTexture.active = snapCam.targetTexture;
		// 	snapshot.ReadPixels(new Rect(0,0, resWidth, resHeight), 0, 0);

		// 	GameObject gm = new GameObject();
		// 	gm.transform.parent = GameObject.FindGameObjectWithTag("Debug").transform;
		// 	gm.AddComponent<RectTransform>();
		// 	gm.AddComponent<CanvasRenderer>();
		// 	gm.AddComponent<RawImage>().texture = snapshot;

			
		// 	byte[] bytes = snapshot.EncodeToPNG();
		// 	string fileName = SnapshotName();
		// 	System.IO.File.WriteAllBytes(fileName, bytes);
		// 	Debug.Log("Snapshot taken!");
		// 	phoneUI.AddPicture(fileName);
		// 	snapCam.gameObject.SetActive(false);
		// }
	} 

	string SnapshotName(){
		return string.Format("{0}/Resources/snap{1}.png", Application.dataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
	} */