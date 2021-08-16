using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlight : MonoBehaviour {

    private float startRotation = 0f;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        startRotation += Time.deltaTime * 200;
        if (GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            transform.Rotate(0, 0, startRotation);
        }
        //print("Rotation: " + transform.rotation.eulerAngles);

    }
}
