using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// For splashing effects when anything moveable hits the water.
public class WaterPlane : MonoBehaviour {

    public GameObject waterExplosionPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 0)
        {
            if (other.GetComponent<Projectile>() != null)
            {
                other.GetComponent<Projectile>().WaterImpact();
            }
            Destroy(Instantiate(waterExplosionPrefab, other.transform.position, Quaternion.Euler(-90, 0, 0)), 5);
        }
     
    }
}
