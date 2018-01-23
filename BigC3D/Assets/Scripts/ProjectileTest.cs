using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTest : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "ProjectileRemover")
		{
			Destroy (this.gameObject);
		}

		if(col.gameObject.tag == "Enemy")
		{
			Debug.Log ("Hit");
			EnemySpawner.instance.count--;
			Destroy (this.gameObject);
			Destroy (col.gameObject);
			ScoreManager.instance.EnemyKill ();
		}
	}
}
