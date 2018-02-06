using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public float speed;
	public Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		rb.GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveEnemyToPlayer ();

		/*if(UIManager.instance.gameOver == true)
		{
			StopEnemyMovement ();
		}*/
	}

	void MoveEnemyToPlayer()
	{
		rb.velocity = new Vector3 (0,0,speed);
	}

	public void StopEnemyMovement()
	{
		rb.velocity = new Vector3 (0,0,speed) * 0;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "EarlyDestroyer")
		{
			Destroy (gameObject);
			EnemySpawner.instance.count--;
			//ScoreManager.instance.LoseLife ();
		}
		if (col.gameObject.tag == "EnemyDestroyer" && UIManager.instance.gameOver == false)
		{
			Destroy (gameObject);
			EnemySpawner.instance.count--;
			ScoreManager.instance.LoseLife ();
			UIManager.instance.mpCnt = 0;
		}
	}
		
}
