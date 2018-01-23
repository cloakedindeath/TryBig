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
	}

	void MoveEnemyToPlayer()
	{
		rb.velocity = new Vector3 (0,0,speed);
	}

	public void StopEnemyMovement()
	{
		rb.velocity = new Vector3 (0,0,0);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "EnemyDestroyer")
		{
			Destroy (gameObject);
			ScoreManager.instance.LoseLife ();
		}
	}
		
}
