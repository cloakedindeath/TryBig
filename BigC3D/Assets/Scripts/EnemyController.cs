using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public float speed;
	public Rigidbody rb;
	AudioSource audioE;
	public AudioClip yesHit;
	public AudioClip noHit;
	public AudioClip enHit;
	public bool walk;
	public GameObject model;
	public GameObject particle1;

	// Use this for initialization
	void Start () 
	{
		rb.GetComponent<Rigidbody> ();
		audioE = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//MoveEnemyToPlayer ();
		if(walk == true)
		{
			MoveEnemyToPlayer ();
		}
		if(walk == false)
		{
			//StopEnemyMovement();
		}
		if(rb.isKinematic == true)
		{
			StartCoroutine (DestroyEnemy ());
		}
	}

	void MoveEnemyToPlayer()
	{
		/*if(walk == true)
		{
			speed = 4;
		}
		else
		{
			speed = 0;
		}*/

		rb.velocity = new Vector3 (0,0,speed);
	}

	public void StopEnemyMovement()
	{
		rb.isKinematic = true;
		rb.angularVelocity = Vector3.zero;
		rb.velocity = Vector3.zero;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "WaffleAmmo" && this.gameObject.tag == "Enemy_Waffle")
		{
			//Instantiate (particle1, col.transform.position, Quaternion.identity) as GameObject;
			//GameObject e = (GameObject)Instantiate(particle1, transform.position,Quaternion.identity);
			/*Instantiate (particle1, new Vector3 (transform.position.x,
				transform.position.y, transform.position.z), Quaternion.identity);*/
			audioE.PlayOneShot (yesHit, .5f);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Eating_01");
			rb.isKinematic = true;
			rb.angularVelocity = Vector3.zero;
			rb.velocity = Vector3.zero;
			this.GetComponent<CapsuleCollider> ().enabled = false;
			//StopEnemyMovement ();
			walk = false;
			StartCoroutine (DestroyEnemy ());
		}
	
		if (col.gameObject.tag == "WaffleAmmo" &&  this.gameObject.tag != "Enemy_Waffle")
		{
			audioE.PlayOneShot (noHit,.5f);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Yuck");
			walk = false;
		}
		if (col.gameObject.tag == "ChickenAmmo" && this.gameObject.tag == "Enemy_Chicken")
		{
			//Instantiate (particle1, col.transform.position, Quaternion.identity) as GameObject;
			//GameObject e = (GameObject)Instantiate(particle1, transform.position,Quaternion.identity);
			/*Instantiate (particle1, new Vector3 (transform.position.x,
				transform.position.y, transform.position.z), Quaternion.identity);*/
			audioE.PlayOneShot (yesHit, .5f);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Eating_01");
			rb.isKinematic = true;
			rb.angularVelocity = Vector3.zero;
			rb.velocity = Vector3.zero;
			this.GetComponent<CapsuleCollider> ().enabled = false;
			//StopEnemyMovement ();
			walk = false;
			StartCoroutine (DestroyEnemy ());
		}

		if (col.gameObject.tag == "ChickenAmmo" && this.gameObject.tag != "Enemy_Chicken")
		{
			audioE.PlayOneShot (noHit,.5f);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Yuck");
			walk = false;
		}
		if (col.gameObject.tag == "KoolAidAmmo" &&this.gameObject.tag == "Enemy_KoolAid")
		{
			//Instantiate (particle1, transform.position, Quaternion.identity) as GameObject;
			//GameObject e = (GameObject)Instantiate(particle1, transform.position,Quaternion.identity);
			/*Instantiate (particle1, new Vector3 (transform.position.x,
				transform.position.y, transform.position.z), Quaternion.identity);*/
			audioE.PlayOneShot (yesHit, .5f);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Eating_01");
			rb.isKinematic = true;
			rb.angularVelocity = Vector3.zero;
			rb.velocity = Vector3.zero;
			this.GetComponent<CapsuleCollider> ().enabled = false;
			//StopEnemyMovement ();
			walk = false;
			StartCoroutine (DestroyEnemy ());
		}
	
		if (col.gameObject.tag == "KoolAidAmmo" && this.gameObject.tag != "Enemy_KoolAid")
		{
			audioE.PlayOneShot (noHit,.5f);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Yuck");
			walk = false;
		}
		if (col.gameObject.tag == "EnemyDestroyer" && UIManager.instance.gameOver == false)
		{
			audioE.PlayOneShot (enHit, .5f);
			//Destroy (gameObject);
			StartCoroutine (DestroyEnemy ());
			EnemySpawner.instance.count--;
			ScoreManager.instance.LoseLife ();
			UIManager.instance.mpCnt = 0;


		}
	
		if(col.gameObject.tag == "EnemyDestroyer" && this.gameObject.tag == "Shield")
		{
			EnemySpawner.instance.shield = false;
		}
		if(col.gameObject.tag == "EnemyDestroyer" && this.gameObject.tag == "Bomb")
		{
			EnemySpawner.instance.bomb = false;
		}
		else
		{
			walk = true;

		}
	}

	IEnumerator DestroyEnemy()
	{
		yield return new WaitForSeconds (.8f);
		model.transform.position = new Vector3 (-20, -0.1f, 0);
		//rb.isKinematic = false;
		Destroy (gameObject);
		//Destroy (particle1);
	}
}
