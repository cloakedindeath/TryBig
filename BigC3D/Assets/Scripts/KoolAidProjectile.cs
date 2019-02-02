using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoolAidProjectile : MonoBehaviour {

	public static KoolAidProjectile instance;
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
		if (col.gameObject.tag == "Enemy_KoolAid")
		{
			correct = true;
			Debug.Log ("Hit");
			EnemySpawner.instance.count--;
			//col.gameObject.transform.position = new Vector3 (-20, -0.1f, 0);
			//col.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			StartCoroutine (DestroyEnemy ());
			Destroy (this.gameObject);
			//Destroy (col.gameObject);
			ScoreManager.instance.EnemyKill ();
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt++;
			UIManager.instance.shieldCnt++;
			UIManager.instance.bombCnt++;
		}
		else if (col.gameObject.tag == "Enemy_Waffle")
		{
			correct = false;
			Destroy (this.gameObject);
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt = 0;
			UIManager.instance.shieldCnt = 0;
			ScoreManager.instance.dingCnt = 0;

		}
		else if (col.gameObject.tag == "Enemy_Chicken")
		{
			correct = false;
			Destroy (this.gameObject);
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt = 0;
			UIManager.instance.shieldCnt = 0;
			ScoreManager.instance.dingCnt = 0;

		}
		else if (col.gameObject.tag == "ProjectileRemover")
		{
			correct = false;
			Destroy (this.gameObject);
			TouchTest.instance.ammoOnScreen--;
			UIManager.instance.mpCnt = 0;
			UIManager.instance.shieldCnt = 0;
			ScoreManager.instance.dingCnt = 0;
		}
	}

	IEnumerator DestroyEnemy()
	{
		yield return new WaitForSeconds (1f);
		//Destroy (gameObject);
	}
}
