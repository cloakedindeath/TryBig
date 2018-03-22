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
			StopEnemyMovement();
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
			audioE.PlayOneShot (yesHit);
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
			audioE.PlayOneShot (noHit,1);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Yuck");
			walk = false;
		}
		if (col.gameObject.tag == "ChickenAmmo" && this.gameObject.tag == "Enemy_Chicken")
		{
			audioE.PlayOneShot (yesHit);
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
			audioE.PlayOneShot (noHit,1);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Yuck");
			walk = false;
		}
		if (col.gameObject.tag == "KoolAidAmmo" &&this.gameObject.tag == "Enemy_KoolAid")
		{
			audioE.PlayOneShot (yesHit);
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
			audioE.PlayOneShot (noHit,1);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Yuck");
			walk = false;
		}
		if (col.gameObject.tag == "EnemyDestroyer" && UIManager.instance.gameOver == false)
		{
			audioE.PlayOneShot (enHit);
			//Destroy (gameObject);
			StartCoroutine (DestroyEnemy ());
			EnemySpawner.instance.count--;
			ScoreManager.instance.LoseLife ();
			UIManager.instance.mpCnt = 0;
			EnemySpawner.instance.shieldCnt = 0;
			EnemySpawner.instance.bombCnt = 0;
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
		Destroy (gameObject);
	}
}
