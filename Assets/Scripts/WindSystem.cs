using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSystem : MonoBehaviour {

    public ParticleSystem windPS;

    private Vector3 windDirection;
    private float radius = 200f;
    
    

	// Use this for initialization
	void Start () {
        RandomizeWindDirection();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(transform.position, transform.position + (windDirection * 100));
	}

    private void RandomizeWindDirection()
    {
        float windX = Random.Range(-1f, 1f);
        float windZ = Random.Range(-1f, 1f);
        float windMagnitude;
        float psStartSpeed;
        float psStartLifetime;
        float psEmissionRate;
        windDirection = new Vector3(windX, 0, windZ);
        windMagnitude = windDirection.magnitude;
        
        windPS.transform.localPosition = windDirection * -radius * 1 / windMagnitude;
        windPS.transform.LookAt(transform.position);

       
        psStartSpeed = windMagnitude * 20f;
        psStartLifetime = 20 / (psStartSpeed / 20);
        psEmissionRate = 10 * (psStartSpeed / 20);
        
        // Matches the Particle Systems to the wind strength.
        var mainWind = windPS.transform.Find("Wind").GetComponent<ParticleSystem>().main;
        var emissionWind = windPS.transform.Find("Wind").GetComponent<ParticleSystem>().emission;
        mainWind.startSpeed = psStartSpeed;
        mainWind.startLifetime = psStartLifetime;
        emissionWind.rateOverTime = psEmissionRate;

        var mainGrass = windPS.transform.Find("Grass").GetComponent<ParticleSystem>().main;
        var emissionGrass = windPS.transform.Find("Grass").GetComponent<ParticleSystem>().emission;
        mainGrass.startSpeed = psStartSpeed;
        mainGrass.startLifetime = psStartLifetime;
        emissionGrass.rateOverTime = psEmissionRate;
        
        windPS.transform.Find("Wind").GetComponent<ParticleSystem>().Play();
        windPS.transform.Find("Grass").GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().volume = windMagnitude * 0.1f;
    }

    public Vector3 GetWindDirection()
    {
        return windDirection;
    }

}
