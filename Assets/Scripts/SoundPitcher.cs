using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Changes the pitch a of a newly created AudioSource to match time-scale (when slow-motion is alrady activated).
public class SoundPitcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().pitch = Time.timeScale;
	}
	
	
}
