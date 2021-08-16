using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenicCamera : MonoBehaviour {

    private Vector3 trackingPosition;
    private float trackingRadius = 20;
    private float trackingHeight = 10;
    private float trackingSpeed = 0.2f;
    private float angle;
    private bool tracking = false;
    

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        angle += Time.deltaTime * trackingSpeed;
    }

    // Update is called once per frame
    void Update () {
       
        // Rotating around the tracked target.
		if (tracking)
        {
            Vector3 newPosition = new Vector3(trackingPosition.x + (trackingRadius * Mathf.Sin(angle)), trackingPosition.y + trackingHeight, trackingPosition.z + (trackingRadius * Mathf.Cos(angle)));
            transform.position = newPosition;
           
        }

	}

    private void LateUpdate()
    {
        transform.LookAt(trackingPosition);
    }

    public void StartTracking(Vector3 impactPosition)
    {
        tracking = true;
        trackingPosition = impactPosition;
    }

    public void StopTracking()
    {
        tracking = false;
        trackingPosition = Vector3.zero;
    }
}
