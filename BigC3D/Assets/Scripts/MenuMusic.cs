using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {

    float vol;
    AudioSource audioM;

    // Use this for initialization
    void Start () {
		audioM = GetComponent<AudioSource>();
		audioM.Play();
        

    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
