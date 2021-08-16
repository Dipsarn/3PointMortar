using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundController : MonoBehaviour {

    public AudioClip mouseOverClip;

    private AudioSource mscAS;

	// Use this for initialization
	void Start () {
        mscAS = GetComponent<AudioSource>();
	}
    
    public void PlayMouseOver()
    {
        mscAS.PlayOneShot(mouseOverClip);
    }

}
