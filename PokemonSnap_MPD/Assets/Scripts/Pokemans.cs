using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemans : MonoBehaviour {

	public bool foundMe;
	Renderer m_Renderer;
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();

    void Start(){
        StartCoroutine("FindTargetsWithDelay", 0.2f);
        m_Renderer = GetComponent<Renderer>();
    }


    IEnumerator FindTargetsWithDelay(float delay){
        while(true){
            yield return new WaitForSeconds (delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets(){
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i=0; i< targetsInViewRadius.Length; i++){
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward,dirToTarget) < viewAngle /2){
                float dstToTarget = Vector3.Distance (transform.position, target.position);

                if(!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)){
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal){
        if(!angleIsGlobal){
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void Update(){
        if (m_Renderer.isVisible){
            //Debug.Log("pokeman is visible");
            foundMe = true;
        }else{
            //Debug.Log("pokeman is not");
			foundMe = false;
		}
        if(visibleTargets.Count > 0){
            Vector3 relativePos = visibleTargets[0].position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;

            GetComponent<Rigidbody>().AddForce(transform.forward * 25, ForceMode.Force);
        }
    }
}
