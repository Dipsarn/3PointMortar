using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Master script for all animated objects with several parts with individual colliders.
public class RagdollMaster : MonoBehaviour {

    public List<GameObject> associatedAnimatedObjects;
    public float scoringFactor;
    public GameObject trackedObject;
    

    private Vector3 originPosition;
	// Use this for initialization
	void Start () {
        originPosition = trackedObject.transform.position;
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ImpactDetected()
    {
        foreach (GameObject go in associatedAnimatedObjects)
        {
            go.GetComponent<RagdollMaster>().ImpactDetected();
        }
        GetComponent<Animator>().enabled = false;
        Rigidbody[] rbList = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbList)
        {
            rb.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ImpactDetected();
    }

    public void SetNewOriginPosition(Vector3 newPosition)
    {
        originPosition = newPosition;
    }

    public Vector3 GetOriginPosition()
    {
        return originPosition;
    }

    public Vector3 GetTrackedObjectPosition()
    {
        return trackedObject.transform.position;
    }

    public float GetScoringFactor()
    {
        return scoringFactor;
    }
        
}
