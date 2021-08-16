using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displaceable : MonoBehaviour {

    
    public float scoringFactor;
    public GameObject trackedObject;


    private Vector3 originPosition;
    // Use this for initialization
    void Start()
    {
        originPosition = trackedObject.transform.position;

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
