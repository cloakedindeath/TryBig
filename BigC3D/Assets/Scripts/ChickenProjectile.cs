using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenProjectile : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy_Chicken")
		{
			
			Debug.Log ("Hit");
			EnemySpawner.instance.count--;
			Destroy (this.gameObject);
			Destroy (col.gameObject);
			ScoreManager.instance.EnemyKill ();
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt++;

		}
		else if (col.gameObject.tag == "Enemy_KoolAid")
		{
			
			Destroy (this.gameObject);
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt = 0;

		}
		else if (col.gameObject.tag == "Enemy_Waffle")
		{

			Destroy (this.gameObject);
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt = 0;

		}
		else if (col.gameObject.tag == "ProjectileRemover")
		{

			Destroy (this.gameObject);
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt = 0;
		}
	}
}
