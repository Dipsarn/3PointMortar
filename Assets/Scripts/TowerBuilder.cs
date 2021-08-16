using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TowerBuilder : MonoBehaviour {

    public GameObject stoneSlab;
    public int slabHeightCount;
    public float slabHeight;

    public int slabBreakForce;
    public int slabBreakTorque;
    
    public GameObject foundation;
    



    private GameObject[,] slabs;
    



	// Use this for initialization
	void Start () {

       
        
	}
	
	// Update is called once per frame
	void Update () {
        LayBricks();
	}


    public void LayBricks()
    {
        slabs = new GameObject[slabHeightCount, 8];
        
        for (int vert=0; vert < slabHeightCount; vert++)
        {
            for (int hori=0; hori < 8; hori++)
            {
                
                slabs[vert, hori] = Instantiate(stoneSlab,transform.position,Quaternion.identity) as GameObject;
                slabs[vert, hori].transform.parent = this.transform;

                slabs[vert, hori].transform.position += Vector3.up * slabHeight * vert;
                print(slabs[vert, hori].GetComponent<Collider>().bounds.size);
                if (vert % 2 == 0)
                {
                    slabs[vert, hori].transform.rotation = Quaternion.Euler(0, 45 * hori, 0);
                } else
                {
                    slabs[vert, hori].transform.rotation = Quaternion.Euler(0, (45 * hori) + 22.5f, 0);
                }
               
              
                slabs[vert, hori].GetComponent<Rigidbody>().mass = 5f;
                slabs[vert, hori].layer = 9;

            }
        }
        ConnectSlabs();
    }

    private void ConnectSlabs()
    {
        int numberOfJoints = 0;
        for (int vert = 0; vert < slabHeightCount; vert++)
        {
            for (int hori = 0; hori < 8; hori++)
            {
                numberOfJoints = 0;
                ConfigurableJoint[] joints;
                if (vert == 0)
                {
                    
                    slabs[vert, hori].AddComponent<ConfigurableJoint>();
                    joints = slabs[vert, hori].GetComponents<ConfigurableJoint>();

                    joints[numberOfJoints].connectedBody = foundation.GetComponent<Rigidbody>();
                    ConfigureJoint(joints[numberOfJoints]);
                    joints[numberOfJoints].breakForce = slabBreakForce + ((1 - (vert / slabHeightCount)) * 2000);
                    joints[numberOfJoints].breakTorque = slabBreakTorque + ((1 - (vert / slabHeightCount)) * 2000);
                    numberOfJoints++;
                    
                }

                

                if (vert < slabHeightCount - 1)
                {
                    slabs[vert, hori].AddComponent<ConfigurableJoint>();
                    joints = slabs[vert, hori].GetComponents<ConfigurableJoint>();
                    ConfigureJoint(joints[numberOfJoints]);
                    joints[numberOfJoints].connectedBody = slabs[vert+1, hori].GetComponent<Rigidbody>();
                    joints[numberOfJoints].breakForce = slabBreakForce + ((1 - (vert / slabHeightCount)) * 2000);
                    joints[numberOfJoints].breakTorque = slabBreakTorque + ((1 - (vert / slabHeightCount)) * 2000);
                    numberOfJoints++;
                }
               
                slabs[vert, hori].AddComponent<ConfigurableJoint>();
                joints = slabs[vert, hori].GetComponents<ConfigurableJoint>();
                ConfigureJoint(joints[numberOfJoints]);
                if (hori == 7)
                {
                    joints[numberOfJoints].connectedBody = slabs[vert, 0].GetComponent<Rigidbody>();
                } else
                {
                    joints[numberOfJoints].connectedBody = slabs[vert, hori + 1].GetComponent<Rigidbody>();
                }
                
                joints[numberOfJoints].breakForce = slabBreakForce + ((1 - (vert / slabHeightCount)) * 2000);
                joints[numberOfJoints].breakTorque = slabBreakTorque + ((1 - (vert / slabHeightCount)) * 2000);
                

                
        
            }
        }
    }

    private void ConfigureJoint(ConfigurableJoint cj)
    {
        cj.xMotion = ConfigurableJointMotion.Locked;
        cj.yMotion = ConfigurableJointMotion.Locked;
        cj.zMotion = ConfigurableJointMotion.Locked;
        cj.angularXMotion = ConfigurableJointMotion.Locked;
        cj.angularYMotion = ConfigurableJointMotion.Locked;
        cj.angularZMotion = ConfigurableJointMotion.Locked;
        cj.projectionMode = JointProjectionMode.PositionAndRotation;
        cj.projectionAngle = 0;
        cj.projectionDistance = 0;
    }
   
}
