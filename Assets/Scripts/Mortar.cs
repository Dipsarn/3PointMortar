using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour {

    public AudioClip mortarFireClip;
    public GameObject projectilePrefab;
    public ParticleSystem muzzleFlash;
    public Transform mortarMuzzle;
    public Transform mortarBipod;
    public Transform mortarTube;
    public Transform mortarPiston;
    public float verticalSens;
    public float horizontalSens;

    private AudioSource mortarAS;
    private float verticalAngle = -25f;
    
    


	// Use this for initialization
	void Start () {
        VerticalAimCompensation();
        mortarAS = GetComponentInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire(float firePower)
    {
        
        mortarAS.PlayOneShot(mortarFireClip);
        muzzleFlash.Play();
        GameObject projectile = Instantiate(projectilePrefab, mortarMuzzle.position, mortarTube.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.up * (firePower + 10f), ForceMode.Impulse);


    }


    public void AimVertical(int pitch)
    {
        verticalAngle += Time.deltaTime * verticalSens * pitch;
        verticalAngle = Mathf.Clamp(verticalAngle, -40, -12);
        mortarTube.rotation = Quaternion.Euler(verticalAngle,mortarTube.rotation.eulerAngles.y,mortarTube.rotation.eulerAngles.z);
        VerticalAimCompensation();

    }

    public void AimHorizontal(int direction)
    {
        float angleChange = Time.deltaTime * horizontalSens;
        transform.Rotate(0, angleChange * direction, 0);
    }


    private void VerticalAimCompensation()
    {
        // Mechanical "animation" that moves the parts correctly.
        mortarPiston.rotation = Quaternion.LookRotation(mortarBipod.position - mortarPiston.position) * Quaternion.Euler(180, 0, 0);
        mortarBipod.rotation = Quaternion.LookRotation(mortarPiston.position - mortarBipod.position) * Quaternion.Euler(90, 0, 0);
    }
}
