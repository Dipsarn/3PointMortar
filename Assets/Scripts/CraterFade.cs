using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterFade : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DelayedActivation());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DelayedActivation()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
