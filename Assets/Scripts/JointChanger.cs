using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class JointChanger : MonoBehaviour {

    public float breakForce;
    public float breakTorque;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ChangeJoints();
	}

    private void ChangeJoints()
    {
        ConfigurableJoint[] joints = GetComponentsInChildren<ConfigurableJoint>();
        foreach (ConfigurableJoint cj in joints)
        {
            if (cj.connectedBody.gameObject.layer == 10)
            {
                cj.breakForce = Mathf.Infinity;
                cj.breakTorque = Mathf.Infinity;
            } else
            {
                cj.breakForce = breakForce;
                cj.breakTorque = breakTorque;
            }
            
            
        }
    }
}
