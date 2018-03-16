using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyANIM : MonoBehaviour 
{
	public static EnemyANIM instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	public GameObject model;

	// Use this for initialization
	void Start () {
		//model.GetComponent<Animator> ().Play ("ANIM_Monster_Walk_01");
	}
	
	// Update is called once per frame
	void Update () {
		//model.GetComponent<Animator> ().Play ("ANIM_Monster_Walk_01");
	}
}
