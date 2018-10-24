using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLandingFootStep : MonoBehaviour {

    AudioSource aSource;
	// Use this for initialization
	void Start () {
        aSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Landing() {
        MessageDispatch.GetInstance().SendAudioMessageForDispatch("Landed", aSource);
    }
}
