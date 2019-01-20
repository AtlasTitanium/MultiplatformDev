using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PhoneUI : MonoBehaviour {
	GUIStyle Candy;
	GUIStyle Picture;
	public Texture2D shoot;
	public Texture2D pictureButton;
	public GameObject CandyPrefab;
	public GameObject MainCamera;
	public PlayerController pc;
	public float flareSpeed = 1;
	private bool flash = false;
	public GameObject canvas;
	public Image image;
	public SnapshotCamera snapCam;
	public List<string> pictureLocations = new List<string>();
	public List<Texture> pictures = new List<Texture>();
	public GameObject[] pokemans;
	private Texture newPicture;

	void Start(){
		Candy = new GUIStyle("button");
		Picture = new GUIStyle("button");
		pokemans = GameObject.FindGameObjectsWithTag("Pokemans");
	}

	void Update(){
		Resources.UnloadUnusedAssets();
	}

	void OnGUI(){
		Picture.normal.background = pictureButton;
		if(GUI.Button(new Rect(Screen.width - Screen.width/4, Screen.height/2, Screen.width/4, Screen.height/2), "", Picture)){
			int amountOfPokemansFound = 0;
			for(int i = 0; i < pokemans.Length; i++){
				Pokemans pk = pokemans[i].GetComponent<Pokemans>();
				if(IsInView(MainCamera.gameObject, pk.gameObject)){
					Debug.Log("Pokeman is in view");
					amountOfPokemansFound++;
				}
				// if(pk.foundMe){
				// 	amountOfPokemansFound++;
				// }
			}
			snapCam.Snap(amountOfPokemansFound);
			flash = true;
			flareSpeed = 1;
			StartCoroutine(LensFlash());
		}
		Candy.normal.background = shoot;
		if(GUI.Button(new Rect(0, Screen.height/2, Screen.width/4, Screen.height/2), "", Candy)){
			Shoot();
		}

		if(flash){
			canvas.SetActive(true);
			image.color = new Color(image.color.r, image.color.g, image.color.b, flareSpeed);
		}
	}

	void Shoot(){
		GameObject gm = Instantiate(CandyPrefab,transform.position,transform.rotation);
		gm.GetComponent<Rigidbody>().AddForce(MainCamera.transform.forward * (pc.speed * 200));
	}

	IEnumerator LensFlash(){
		yield return new WaitForSeconds(0.05f);
		flareSpeed -= 0.1f;
		if(flareSpeed <= 0){
			flash = false;
			canvas.SetActive(false);
		} else {
			StartCoroutine(LensFlash());
		}
	}

	void OnApplicationQuit(){
		for(int i = 0; i < pictures.Count; i++){
			string metaFile = pictureLocations[i] + ".meta";
			System.IO.File.Delete(pictureLocations[i]);
			System.IO.File.Delete(metaFile);
		}
		newPicture=null;
		pictures.Clear();
		Resources.UnloadUnusedAssets();
		
	}

	private bool IsInView(GameObject origin, GameObject toCheck)
     {
         Vector3 pointOnScreen = MainCamera.GetComponent<Camera>().WorldToScreenPoint(toCheck.GetComponentInChildren<Renderer>().bounds.center);
 
         //Is in front
         if (pointOnScreen.z < 0)
         {
             //Debug.Log("Behind: " + toCheck.name);
             return false;
         }
 
         //Is in FOV
         if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) ||
                 (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height))
         {
             //Debug.Log("OutOfBounds: " + toCheck.name);
             return false;
         }
 
         RaycastHit hit;
         Vector3 heading = toCheck.transform.position - origin.transform.position;
         Vector3 direction = heading.normalized;// / heading.magnitude;
         
         if (Physics.Linecast(MainCamera.transform.position, toCheck.GetComponentInChildren<Renderer>().bounds.center, out hit))
         {
             if (hit.transform.name != toCheck.name)
             {
                 /* -->
                 Debug.DrawLine(cam.transform.position, toCheck.GetComponentInChildren<Renderer>().bounds.center, Color.red);
                 Debug.LogError(toCheck.name + " occluded by " + hit.transform.name);
                 */
                // Debug.Log(toCheck.name + " occluded by " + hit.transform.name);
                 return false;
             }
         }
         return true;
     }
}

/*public void AddPicture(string image){
		// byte[] bytes;
		// bytes = System.IO.File.ReadAllBytes(image);
		// Texture2D newPicture = new Texture2D(1,1);
     	// newPicture.LoadImage(bytes);
		// pictures.Add(newPicture);
		
		// pictureLocations.Add(image);
		// // Debug.Log(image);
		//  image = image.Replace("C:/Users/Atlas/Desktop/HKU School/Year2/GitHubFiles/MultiplatformDev/PokemonSnap_MPD/Assets/Resources/","");
		//  image = image.Replace(".png","");
		// // Debug.Log(image);
		// // Debug.Log("snap" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
		// // var oof = Resources.Load("Atlas_present - Copy");
		// // Debug.Log(oof);
		// //pictures.Add(newPicture);
		// StartCoroutine(WaitToAdd(image));
		//AssetDatabase.Refresh();
	} 
	
	IEnumerator WaitToAdd(string oofHelp){
		yield return new WaitForSeconds(1);
		newPicture = Resources.Load<Texture>(oofHelp);
		Debug.Log(newPicture);
		pictures.Add(newPicture);
		newPicture=null;
	}*/