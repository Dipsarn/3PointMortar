using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float force;
    public int explosionRadius;
    public LayerMask lMask;
    
    public GameObject chaseCamera;
    public GameObject explosion;
    public GameObject craterSprite;

    private Vector3 windDirection;
    private Rigidbody rb;
    private bool impact = false;


	// Use this for initialization
	void Start () {

        GameObject.Find("GameEngine").GetComponent<GameEngine>().AddProjectile(this.gameObject);
        windDirection = GameObject.Find("GameEngine").GetComponent<GameEngine>().GetWindDirection();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        
        rb.AddForce(windDirection);
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosion.GetComponent<Explosion>().TriggerExplosion(explosionRadius,force);

        // Destructible layer.
        if (collision.gameObject.layer == 10)
        {
            CreateCrater(collision.contacts[0]);
        }
      
        impact = true;
    }

    private void CreateCrater(ContactPoint contactPoint)
    {

        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contactPoint.normal);
        GameObject crater = Instantiate(craterSprite, contactPoint.point + Vector3.up * 0.03f, rot);
        crater.GetComponent<SpriteRenderer>().enabled = false;
    }
    

    public void WaterImpact()
    {
        impact = true;
    }

    public bool HasImpacted()
    {
        return impact;
    }

    public void DetachCamera()
    {
        chaseCamera.GetComponent<ChaseCamera>().DetachCamera(gameObject);
    }
}
