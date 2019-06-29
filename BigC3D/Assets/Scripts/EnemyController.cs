using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	public static EnemyController instance;
	public float speed;
	public float speed2 = 5;
	public Rigidbody rb;
	AudioSource audioE;
	public AudioClip yesHit;
	public AudioClip noHit;
	public AudioClip enHit;
	public bool walk;
	public GameObject model;
	public GameObject particleChick;
	public GameObject particleKool;
	public GameObject particleWaffle;
	public Vector3 impactLoc1;

    public GameObject[] enemiesW;
    public GameObject[] enemiesC;
    public GameObject[] enemiesK;
    public GameObject[] projectiles;

    void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

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
			particleWaffle = Instantiate (particleWaffle, transform.position, Quaternion.FromToRotation (Vector3.up, impactLoc1)) as GameObject;
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
			this.rb.velocity = new Vector3 (0,0,speed2);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Yuck");
			walk = false;
		}
		if (col.gameObject.tag == "ChickenAmmo" && this.gameObject.tag == "Enemy_Chicken")
		{
			particleChick = Instantiate (particleChick, transform.position, Quaternion.FromToRotation (Vector3.up, impactLoc1)) as GameObject;
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
			this.rb.velocity = new Vector3 (0,0,speed2);
			model.GetComponent<Animator> ().Play ("ANIM_Monster_Yuck");
			walk = false;
		}
		if (col.gameObject.tag == "KoolAidAmmo" &&this.gameObject.tag == "Enemy_KoolAid")
		{
			particleKool = Instantiate (particleKool, transform.position, Quaternion.FromToRotation (Vector3.up, impactLoc1)) as GameObject;
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
			this.rb.velocity = new Vector3 (0,0,speed2);
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
			//UIManager.instance.LifeAway();   // this is where the player loses HP on being hit by the enemy
			UIManager.instance.mpCnt = 0;  //reset mp
            UIManager.instance.shieldCnt = 0;  //reset shield count
            UIManager.instance.bombCnt = 0;  // reset bomb count


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


    /* Shitty fuck
     public void BombEvent()
     {
          if (this.gameObject.tag == "Enemy_KoolAid" || this.gameObject.tag == "Enemy_Chicken" || this.gameObject.tag == "Enemy_Waffle") {

              model.GetComponent<Animator> ().Play ("ANIM_Monster_Eating_01");
              StartCoroutine (DestroyEnemy ());
          }
         StartCoroutine(DestroyWEnemy());
         StartCoroutine(DestroyCEnemy());
         StartCoroutine(DestroyKEnemy());

         projectiles = GameObject.FindGameObjectsWithTag("Projectile");

         for (int i = 0; i < projectiles.Length; i++)
         {
            Destroy(projectiles[i]);
         }
     }


     IEnumerator DestroyWEnemy()
     {
         yield return new WaitForSeconds(.8f);
         model.transform.position = new Vector3(-20, -0.1f, 0);
         for (int i = 0; i < EnemySpawner.instance.activeWEnemies.Length; i++)
         {
             //Destroy(enemiesW[i]);
             model.GetComponent<Animator>().Play("ANIM_Monster_Eating_01");

             // Destroy(EnemySpawner.instance.activeWEnemies[i]);
         }
         //rb.isKinematic = false;
         //Destroy(gameObject);
     }
     IEnumerator DestroyCEnemy()
     {
         yield return new WaitForSeconds(.8f);
         model.transform.position = new Vector3(-20, -0.1f, 0);
         for (int i = 0; i < EnemySpawner.instance.activeCEnemies.Length; i++)
         {
             //Destroy(enemiesW[i]);
             model.GetComponent<Animator>().Play("ANIM_Monster_Eating_01");

             Destroy(EnemySpawner.instance.activeCEnemies[i]);
         }
         //rb.isKinematic = false;
         //Destroy(gameObject);
     }
     IEnumerator DestroyKEnemy()
     {
         yield return new WaitForSeconds(.8f);
         model.transform.position = new Vector3(-20, -0.1f, 0);
         for (int i = 0; i < EnemySpawner.instance.activeKEnemies.Length; i++)
         {
             //Destroy(enemiesW[i]);
             model.GetComponent<Animator>().Play("ANIM_Monster_Eating_01");

             Destroy(EnemySpawner.instance.activeKEnemies[i]);

         }
         //rb.isKinematic = false;
         //Destroy(gameObject);
     }
     */

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(.8f);
        model.transform.position = new Vector3(-20, -0.1f, 0);
        //rb.isKinematic = false;
        Destroy(gameObject);
    }
}
