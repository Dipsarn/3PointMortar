using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public GameObject explosionPrefab;
    
    public Material materialWhite;
    private Renderer rend;
    private Color renderAlpha;

    private float force;
    private float explosionRadius;
    private List<Collider> alreadyCollidedList;
    
    // Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        renderAlpha = rend.material.color;
        alreadyCollidedList = new List<Collider>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TriggerExplosion(float explosionRadius, float force)
    {
        this.force = force;
        this.explosionRadius = explosionRadius;
        
        Destroy(GameObject.Instantiate(explosionPrefab, transform.position + (Vector3.up*0.2f), Quaternion.identity), 10);
        GetComponent<MeshRenderer>().enabled = true;
        transform.parent = null;
        StartCoroutine(ShockEffect());
    }

    private void OnTriggerEnter(Collider other)
    {

        float relativeForce = 0f;
        Vector3 explosionPos = transform.position;

        // Destructible
        if (other.gameObject.layer == 9 && !alreadyCollidedList.Contains(other))
        {
            
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            if (explosionRadius != 0)
            {
                relativeForce = force * (1 - (Vector3.Distance(transform.position, other.transform.position) / explosionRadius));
            } else
            {
                relativeForce = force;
            }
            

      
           
            Vector3 dir = other.transform.position - transform.position;
            dir = dir.normalized;
           
            rb.AddForce(dir * relativeForce,ForceMode.Impulse);
            alreadyCollidedList.Add(other);
        }
        

        // Ragdolls
        if (other.gameObject.layer == 11 && !alreadyCollidedList.Contains(other))
        {
            
            
            
            
                if (other.GetComponentInParent<Animator>().enabled)
                {
                    other.GetComponentInParent<RagdollMaster>().ImpactDetected(); 
                }
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (explosionRadius != 0)
                {
                    relativeForce = force * (1 - (Vector3.Distance(transform.position, other.transform.position) / explosionRadius));
                }
                else
                {
                    relativeForce = force;
                }
                
                Vector3 dir = other.transform.position - transform.position;
                dir = dir.normalized;
                
                rb.AddForce(dir * relativeForce, ForceMode.Impulse);
                alreadyCollidedList.Add(other);

            

        }


        // Displaceable
        if (other.gameObject.layer == 12 && !alreadyCollidedList.Contains(other))
        {
          
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (explosionRadius != 0)
            {
                relativeForce = force * (1 - (Vector3.Distance(transform.position, other.transform.position) / explosionRadius));
            }
            else
            {
                relativeForce = force;
            }

            Vector3 dir = other.transform.position - transform.position;
            dir = dir.normalized;

            rb.AddForce(dir * relativeForce, ForceMode.Impulse);
            alreadyCollidedList.Add(other);
        }



    }



    IEnumerator ShockEffect()
    {
        
        float duration = 0.3f;
        
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float normalizedAlpha = 1 - (timer / duration);
            float normalizedSize = explosionRadius * (timer / duration);

            renderAlpha.a = normalizedAlpha;
            rend.material.color = renderAlpha;
            transform.localScale = new Vector3(normalizedSize, normalizedSize, normalizedSize);
            yield return null;
        }
        Destroy(gameObject);
    }
}
