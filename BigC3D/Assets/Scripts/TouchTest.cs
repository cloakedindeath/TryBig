﻿using System.Collections;
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
	public Rigidbody projectile1;
	public Rigidbody projectile2;
	public Rigidbody projectile3;
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
	public Vector3 destination;
	public bool goleft = false;
	public bool goright = false;



	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		ammoType = 0;
		ammoOnScreen = 0;
		//ammoSwitchButton.image.sprite = ammoColor [0];
	}

	void Start()
	{
		audioC = GetComponent<AudioSource>();
		destination = transform.position;
	}

	void Update() 
	{
		//Debug.Log (ammoType);
		if(UIManager.instance.gameOver == false)
		{
			////////////////////////////////////////////////////
			//Check that player position is within the screen//
			//////////////////////////////////////////////////

		}
		if (player.transform.position.x >= -4f && player.transform.position.x <= 3.70f) 
		{
			if(goright == true)
			{ transform.Translate(-Vector3.right * speed * Time.deltaTime); }
			if(goleft == true)
			{ transform.Translate(Vector3.right * speed * Time.deltaTime); }
			
			Vector2 p = player.transform.position;
			player.transform.position = new Vector2( Mathf.Clamp( p.x, -4f, 3.7f ), p.y);
		}

	}

	public void moveLeft()
	{
		if (player.transform.position.x >= -4f && player.transform.position.x <= 3.70f) 
		{
			//////////////////////////////////////////
			//Touch movement with Y-axis restricted//
			////////////////////////////////////////
			/*if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) 
			{
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				//shouldDiscardSwipe (touchDeltaPosition);
				transform.Translate (-touchDeltaPosition.x * speed, 0 * speed, 0);
			}*/

			player.transform.Translate(Vector3.right * speed * Time.deltaTime) ;
			//player.transform.position += Vector3.Lerp(speed * Time.deltaTime, destination, transform.position);
			//player.transform.position -= new Vector3 (transform.position.x * speed,0,0);
			//////////////////////////////////////////////////
			//Guarantees that the player wont go off screen//
			////////////////////////////////////////////////
			Vector2 p = player.transform.position;
			player.transform.position = new Vector2( Mathf.Clamp( p.x, -4f, 3.7f ), p.y);
		}

	}
	public void stopLeft()
	{
		if (player.transform.position.x >= -4f && player.transform.position.x <= 3.70f) 
		{
			//////////////////////////////////////////
			//Touch movement with Y-axis restricted//
			////////////////////////////////////////
			/*if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) 
			{
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				//shouldDiscardSwipe (touchDeltaPosition);
				transform.Translate (-touchDeltaPosition.x * speed, 0 * speed, 0);
			}*/

			player.transform.Translate(Vector3.right * 0) ;
			//player.transform.position = Vector3.Lerp(speed * Time.deltaTime, destination, transform.position);
			//////////////////////////////////////////////////
			//Guarantees that the player wont go off screen//
			////////////////////////////////////////////////
			Vector2 p = player.transform.position;
			player.transform.position = new Vector2( Mathf.Clamp( p.x, -4f, 3.7f ), p.y);
		}

	}
	public void moveRight()
	{
		if (player.transform.position.x >= -4f && player.transform.position.x <= 3.70f) 
		{
			//////////////////////////////////////////
			//Touch movement with Y-axis restricted//
			////////////////////////////////////////
			/*if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) 
			{
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				//shouldDiscardSwipe (touchDeltaPosition);
				transform.Translate (-touchDeltaPosition.x * speed, 0 * speed, 0);
			}*/

			player.transform.Translate(-Vector3.right * speed * Time.deltaTime);
			//////////////////////////////////////////////////
			//Guarantees that the player wont go off screen//
			////////////////////////////////////////////////
			Vector2 p = player.transform.position;
			player.transform.position = new Vector2( Mathf.Clamp( p.x, -4f, 3.7f ), p.y);
		}
	}
	public void stopRight()
	{
		if (player.transform.position.x >= -4f && player.transform.position.x <= 3.70f) 
		{
			//////////////////////////////////////////
			//Touch movement with Y-axis restricted//
			////////////////////////////////////////
			/*if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) 
			{
				Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
				//shouldDiscardSwipe (touchDeltaPosition);
				transform.Translate (-touchDeltaPosition.x * speed, 0 * speed, 0);
			}*/

			player.transform.Translate(-Vector3.right * 0);
			//////////////////////////////////////////////////
			//Guarantees that the player wont go off screen//
			////////////////////////////////////////////////
			Vector2 p = player.transform.position;
			player.transform.position = new Vector2( Mathf.Clamp( p.x, -4f, 3.7f ), p.y);
		}
	}
	//Ammo Switch
	/*public void PickAmmoType()
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
	}*/

	public void Shoot2()
	{
		if(ammoOnScreen <=2 && UIManager.instance.startWaveCountdown == true)
		{

			Debug.Log ("Shooting");
			audioC.PlayOneShot (shoot);
			Rigidbody instantiatedProjectile = Instantiate (projectile1,
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
	public void Shoot3()
	{
		if(ammoOnScreen <=2 && UIManager.instance.startWaveCountdown == true)
		{

			Debug.Log ("Shooting");
			audioC.PlayOneShot (shoot);
			Rigidbody instantiatedProjectile = Instantiate (projectile2,
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
	public void ShootKool()
	{
		if(ammoOnScreen <= 2 && UIManager.instance.startWaveCountdown == true)
		{

			Debug.Log ("Shooting kool");
			audioC.PlayOneShot (shoot);
			Rigidbody instantiatedProjectile = Instantiate (projectile3,
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

	private bool shouldDiscardSwipe(Vector2 touchPos)
	{
		PointerEventData touch = new    PointerEventData(EventSystem.current);
		touch.position = touchPos;
		List<RaycastResult> hits = new List<RaycastResult>();
		EventSystem.current.RaycastAll(touch, hits);
		//Debug.Log (hits.Count);
		return (hits.Count > 0); // discard swipe if an UI element is beneath

	}

	public void ondownMoveLeft()
	{ goleft = true; }
	public void ondownMoveRight()
	{ goright = true; }
	public void onupMoveLeft()
	{ goleft = false; }
	public void onupMoveRight()
	{ goright = false; }


}
