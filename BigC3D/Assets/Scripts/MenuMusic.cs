using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioSource audioM = GetComponent<AudioSource>();
		audioM.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
