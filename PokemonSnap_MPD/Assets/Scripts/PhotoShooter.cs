using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PhotoShooter : MonoBehaviour {

	[SteamVR_DefaultAction("Interact")]
	public SteamVR_Action_Boolean spawn;
	SteamVR_Behaviour_Pose trackedObj;
	FixedJoint joint;

	public SnapshotCamera snapCam;
	public GameObject[] pokemans;
	public float flareSpeed = 1;
	private bool flash = false;
	public RawImage image;
	public Light light;
	private float intensity;
	private float intensityOrigin;
	private bool active = false;

	private void Awake()
	{
		trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
		intensityOrigin = light.intensity;
		intensity = light.intensity;
		pokemans = GameObject.FindGameObjectsWithTag("Pokemans");
	}

	void Update(){
		if(flash){
			light.gameObject.SetActive(true);
			image.color = new Color(image.color.r, image.color.g, image.color.b, flareSpeed);
			light.intensity = intensity;
		}
	}

	private void FixedUpdate()
	{
		if (joint == null && spawn.GetStateDown(trackedObj.inputSource))
		{
			if(!active){
				snapCam.gameObject.SetActive(true);
				Debug.Log("shoot picture");
				int amountOfPokemansFound = 0;
				for(int i = 0; i < pokemans.Length; i++){
					Pokemans pk = pokemans[i].GetComponent<Pokemans>();
					if(IsInView(snapCam.gameObject, pk.gameObject)){
						Debug.Log("Pokeman is in view");
						amountOfPokemansFound++;
					}
				}
				snapCam.Snap(amountOfPokemansFound);
				flash = true;
				light.intensity = intensityOrigin;
				flareSpeed = 1;
				StartCoroutine(LensFlash());
				active = true;
			}
		}
	}

	IEnumerator LensFlash(){
		yield return new WaitForSeconds(0.05f);
		flareSpeed -= 0.1f;
		intensity -= intensityOrigin/10;
		if(flareSpeed <= 0){
			flash = false;
			light.gameObject.SetActive(false);
			intensity = intensityOrigin;
			snapCam.gameObject.SetActive(false);
			active = false;
		} else {
			StartCoroutine(LensFlash());
		}
	}

	private bool IsInView(GameObject origin, GameObject toCheck)
     {
         Vector3 pointOnScreen = snapCam.GetComponent<Camera>().WorldToScreenPoint(toCheck.GetComponentInChildren<Renderer>().bounds.center);
 
         //Is in front
         if (pointOnScreen.z < 0)
         {
             Debug.Log("Behind: " + toCheck.name);
             return false;
         }
 
         //Is in FOV
         if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) ||
                 (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height))
         {
             Debug.Log("OutOfBounds: " + toCheck.name);
             return false;
         }
 
         RaycastHit hit;
         Vector3 heading = toCheck.transform.position - origin.transform.position;
         Vector3 direction = heading.normalized;// / heading.magnitude;
         
         if (Physics.Linecast(snapCam.transform.position, toCheck.GetComponentInChildren<Renderer>().bounds.center, out hit))
         {
             if (hit.transform.name != toCheck.name)
             {
                 /* -->
                 Debug.DrawLine(cam.transform.position, toCheck.GetComponentInChildren<Renderer>().bounds.center, Color.red);
                 Debug.LogError(toCheck.name + " occluded by " + hit.transform.name);
                 */
                Debug.Log(toCheck.name + " occluded by " + hit.transform.name);
                 return false;
             }
         }
         return true;
     }
}
