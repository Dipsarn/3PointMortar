using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WallGenerator : MonoBehaviour {

    public GameObject stoneSlab;
    public int brickCountVert;
    public int brickCountHori;
    public float brickLength;
    public float brickHeight;
    public float brickDepth;

    public int brickBreakForce;
    public int brickBreakTorque;
    
    public GameObject foundation;
    



    private GameObject[,] bricks;
    



	// Use this for initialization
	void Start () {

       
        
	}
	
	// Update is called once per frame
	void Update () {
        LayBricks();
	}


    public void LayBricks()
    {
        bricks = new GameObject[brickCountVert, brickCountHori];
        float wallWidth = brickLength * brickCountHori;
        for (int vert=0; vert < brickCountVert; vert++)
        {
            for (int hori=0; hori < brickCountHori; hori++)
            {
                
                bricks[vert, hori] = Instantiate(stoneSlab,Vector3.zero,Quaternion.identity) as GameObject;
                print(bricks[vert, hori].GetComponent<Collider>().bounds.size);
                
                bricks[vert, hori].transform.position = transform.position + transform.right * ((wallWidth / 2) - (hori * brickLength) - (brickLength / 2))
                    + (transform.up * (brickHeight * vert + (brickHeight / 2)))
                    + (transform.forward * -brickDepth / 2);
                bricks[vert, hori].transform.rotation = Quaternion.Euler(90, 0, 0);
                bricks[vert, hori].AddComponent<Rigidbody>();
                bricks[vert, hori].GetComponent<Rigidbody>().isKinematic = true;
                bricks[vert, hori].GetComponent<Rigidbody>().mass = 5f;
                bricks[vert, hori].layer = 9;

            }
        }
        ConnectBricks();
    }

    private void ConnectBricks()
    {
        int numberOfJoints = 0;
        for (int vert = 0; vert < brickCountVert; vert++)
        {
            for (int hori = 0; hori < brickCountHori; hori++)
            {
                numberOfJoints = 0;
               
                if (vert == 0)
                {
                    
                    bricks[vert, hori].AddComponent<ConfigurableJoint>();
                    ConfigurableJoint[] joints = bricks[vert, hori].GetComponents<ConfigurableJoint>();

                    joints[numberOfJoints].connectedBody = foundation.GetComponent<Rigidbody>();
                    ConfigureJoint(joints[numberOfJoints]);
                    joints[numberOfJoints].breakForce = brickBreakForce;
                    joints[numberOfJoints].breakTorque = brickBreakTorque;
                    numberOfJoints++;
                    
                }

                if (hori < brickCountHori-1)
                {
                    bricks[vert, hori].AddComponent<ConfigurableJoint>();
                    ConfigurableJoint[] joints = bricks[vert, hori].GetComponents<ConfigurableJoint>();
                    ConfigureJoint(joints[numberOfJoints]);
                    joints[numberOfJoints].connectedBody = bricks[vert, hori + 1].GetComponent<Rigidbody>();
                    joints[numberOfJoints].breakForce = brickBreakForce;
                    joints[numberOfJoints].breakTorque = brickBreakTorque;
                    numberOfJoints++;

                }

                if (vert < brickCountVert-1)
                {
                    bricks[vert, hori].AddComponent<ConfigurableJoint>();
                    ConfigurableJoint[] joints = bricks[vert, hori].GetComponents<ConfigurableJoint>();
                    ConfigureJoint(joints[numberOfJoints]);
                    joints[numberOfJoints].connectedBody = bricks[vert+1, hori].GetComponent<Rigidbody>();
                    joints[numberOfJoints].breakForce = brickBreakForce;
                    joints[numberOfJoints].breakTorque = brickBreakTorque;
                }
              

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
