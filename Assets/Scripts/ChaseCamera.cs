using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCamera : MonoBehaviour {


    private Transform target;
    private float offsetDistance;

    // Use this for initialization
    void Start () {
        
        
        
	}

    private void LateUpdate()
    {
        transform.position = target.position + target.transform.forward * -offsetDistance;
        transform.LookAt(target);
        
    }

    public void DetachCamera(GameObject chaseTarget)
    {
        
        target = chaseTarget.transform;
        offsetDistance = Vector3.Distance(transform.position, target.position);
        transform.parent = null;
    }

}
