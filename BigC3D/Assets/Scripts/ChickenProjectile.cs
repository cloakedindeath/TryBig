using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenProjectile : MonoBehaviour {

	public static ChickenProjectile instance;
	public bool correct;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

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
			correct = true;
			Debug.Log ("Hit");
			EnemySpawner.instance.count--;

			//Destroy (col.gameObject);
			col.gameObject.transform.position = new Vector3 (-20, -0.1f, 0);
			StartCoroutine (DestroyEnemy ());
			Destroy (this.gameObject);
			ScoreManager.instance.EnemyKill ();
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt++;

		}
		else if (col.gameObject.tag == "Enemy_KoolAid")
		{
			correct = false;
			Destroy (this.gameObject);
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt = 0;

		}
		else if (col.gameObject.tag == "Enemy_Waffle")
		{
			correct = false;
			Destroy (this.gameObject);
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt = 0;

		}
		else if (col.gameObject.tag == "ProjectileRemover")
		{
			correct = false;
			Destroy (this.gameObject);
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt = 0;
		}
	}
	IEnumerator DestroyEnemy()
	{
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}
}
