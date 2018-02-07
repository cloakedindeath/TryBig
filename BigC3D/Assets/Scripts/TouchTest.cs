using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchTest : MonoBehaviour 
{

	public static TouchTest instance;
	// Update is called once per frame
	public float speed = 0.1F;
	public GameObject player; 

	public Rigidbody projectile;
	public Rigidbody[] projectiles;
	public Sprite[] ammoColor;
	public float bulletSpeed = 20;
	public int ammoType;
	public int ammoOnScreen;
	public Button ammoSwitchButton;
	AudioSource audioC;
	public AudioClip shoot;
	public AudioClip ammoSwap;
	public AudioClip playerHurt;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		ammoType = 0;
		ammoOnScreen = 0;
		ammoSwitchButton.image.sprite = ammoColor [0];
	}

	void Start()
	{
		audioC = GetComponent<AudioSource>();
	}

	void Update() 
	{
		//Debug.Log (ammoType);
		if(UIManager.instance.gameOver == false)
		{
			////////////////////////////////////////////////////
			//Check that player position is within the screen//
			//////////////////////////////////////////////////
			if (player.transform.position.x >= -4f && player.transform.position.x <= 3.70f) 
			{
				//////////////////////////////////////////
				//Touch movement with Y-axis restricted//
				////////////////////////////////////////
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) 
				{
					Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
					transform.Translate (-touchDeltaPosition.x * speed, 0 * speed, 0);
				}
				//////////////////////////////////////////////////
				//Guarantees that the player wont go off screen//
				////////////////////////////////////////////////
				Vector2 p = player.transform.position;
				player.transform.position = new Vector2( Mathf.Clamp( p.x, -4f, 3.7f ), p.y);
			}
		}
	}

	//Ammo Switch
	public void PickAmmoType()
	{
		
		if(ammoType == 0)
		{
			audioC.PlayOneShot (ammoSwap);
			projectile = projectiles [0];
			ammoSwitchButton.image.sprite = ammoColor [1];
			ammoType++;
		}
		else if(ammoType == 1)
		{
			audioC.PlayOneShot (ammoSwap);
			projectile = projectiles [1];
			ammoSwitchButton.image.sprite = ammoColor [2];
			ammoType++;
		}
		else if(ammoType == 2)
		{
			audioC.PlayOneShot (ammoSwap);
			projectile = projectiles [2];
			ammoSwitchButton.image.sprite = ammoColor [0];
			ammoType = 0;
		}
	}

	public void Shoot2()
	{
		if(ammoOnScreen <=2)
		{
			
			Debug.Log ("Shooting");
			audioC.PlayOneShot (shoot);
			Rigidbody instantiatedProjectile = Instantiate (projectile,
				player.transform.position, 
				Quaternion.identity)
				as Rigidbody;
			instantiatedProjectile.velocity = transform.TransformDirection (new Vector3 (0, 0, -bulletSpeed));
			ammoOnScreen++;
		}
		else
		{
			
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy_Waffle" ||
			col.gameObject.tag == "Enemy_Chicken" || col.gameObject.tag == "Enemy_KoolAid")
		{
			audioC.PlayOneShot (playerHurt);
			EnemySpawner.instance.count--;
			Destroy (col.gameObject);
			ScoreManager.instance.LoseLife ();
		}
	}
}
