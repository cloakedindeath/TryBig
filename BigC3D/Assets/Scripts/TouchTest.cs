using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchTest : MonoBehaviour 
{
    /* This script handles player movement with animations,
     * projectile shooting,
     * Shield logic, and
     * the bomb blast
     * 
     */
	
	public static TouchTest instance;
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
	public GameObject model;
	public GameObject shield;
	public GameObject bombButton;
	public GameObject shieldText;
	public Slider movement;



	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		ammoType = 0;
		ammoOnScreen = 0;
		//ammoSwitchButton.image.sprite = ammoColor [0];
		//InvokeRepeating("Idle",0.1f,0.1f);
	}

	void Start()
	{
		audioC = GetComponent<AudioSource>();
		destination = transform.position;
	}

	void Update() 
	{
		//SubmitSliderSetting ();
		/*if(Application.platform == RuntimePlatform.Android)
		{
			
		}
		else
		{
			
		}*/
		//Debug.Log (ammoType);
		if(UIManager.instance.gameOver == false)
		{
			////////////////////////////////////////////////////
			//Check that player position is within the screen//
			//////////////////////////////////////////////////

		}
		/*if(goleft == false && goright == false)
		{
			//model.GetComponent<Animator> ().Play ("ANIM_Player_Idle_01");
		}*/
		if (player.transform.position.x >= -4f && player.transform.position.x <= 3.70f) 
		{
			if(goright == true)
			{ 	model.GetComponent<Animator> ().Play ("ANIM_Player_Run_Right");
				transform.Translate(-Vector3.right * speed * Time.deltaTime); }
			if(goleft == true)
			{ 
				model.GetComponent<Animator> ().Play ("ANIM_Player_Run_Left");
				transform.Translate(Vector3.right * speed * Time.deltaTime); }
			else{
				//model.GetComponent<Animator> ().Play ("ANIM_Player_Idle_01");
			}
			
			Vector2 p = player.transform.position;
			player.transform.position = new Vector2( Mathf.Clamp( p.x, -4f, 3.7f ), p.y);
		}

        #region PC controls for testing purposes

        if(Input.GetKeyDown(KeyCode.A))
        {
            ondownMoveLeft();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            onupMoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ondownMoveRight();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            onupMoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Shoot2();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Shoot3();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ShootKool();
        }
        #endregion
    }

    public void Idle()
	{
		model.GetComponent<Animator> ().Play ("ANIM_Player_Idle_01");
	}
    
 
    #region Movement for player (attached to buttons)

    public void ondownMoveLeft()
    { goleft = true; }
    public void ondownMoveRight()
    { goright = true; }
    public void onupMoveLeft()
    {
        goleft = false;
        model.GetComponent<Animator>().Play("ANIM_Player_Idle_01");
    }
    public void onupMoveRight()
    {
        goright = false;
        model.GetComponent<Animator>().Play("ANIM_Player_Idle_01");
    }

    #endregion

    #region SHOOTING MECHANICS
    public void Shoot2()  // shoots waffle projectile
	{
		if(ammoOnScreen <=2 && UIManager.instance.startWaveCountdown == true)
		{
			model.GetComponent<Animator> ().Play ("ANIM_Player_Fire_01");
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
    public void Shoot3()  //shoots chicken projectile
    {
        if (ammoOnScreen <= 2 && UIManager.instance.startWaveCountdown == true)
        {
            model.GetComponent<Animator>().Play("ANIM_Player_Fire_01");
            Debug.Log("Shooting");
            audioC.PlayOneShot(shoot);
            Rigidbody instantiatedProjectile = Instantiate(projectile2,
                player.transform.position,
                Quaternion.identity)
                as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, -bulletSpeed));
            ammoOnScreen++;
        }
        else
        {

        }
    }
    public void ShootKool()  // shoots kool aid projectile
    {
        if (ammoOnScreen <= 2 && UIManager.instance.startWaveCountdown == true)
        {
            model.GetComponent<Animator>().Play("ANIM_Player_Fire_01");
            Debug.Log("Shooting kool");
            audioC.PlayOneShot(shoot);
            Rigidbody instantiatedProjectile = Instantiate(projectile3,
                player.transform.position,
                Quaternion.identity)
                as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, -bulletSpeed));
            ammoOnScreen++;
        }
        else
        {

        }
    }
    #endregion

    #region Shield logic and Bomb usage logic

    //Handles the shield logic
    void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Enemy_Waffle" ||
			col.gameObject.tag == "Enemy_Chicken" || col.gameObject.tag == "Enemy_KoolAid")
		{
			if(shield.activeSelf)
			{
				Destroy (col.gameObject);
				UIManager.instance.shieldCnt = 0;
                ScoreManager.instance.shieldActive = false;
            }
			else
			{
				audioC.PlayOneShot (playerHurt);
				EnemySpawner.instance.count--;
				Destroy (col.gameObject);
				ScoreManager.instance.LoseLife ();
				//UIManager.instance.LifeAway();   // this is where the player loses HP on being hit by the enemy
			}
		}
	}

    public void BombBlast()
	{
		//EnemySpawner.instance.bomb = false;
		ScoreManager.instance.score += 100;
		UIManager.instance.bombCnt = 0;
		bombButton.SetActive (false);
        ScoreManager.instance.bombActive = false;
        //EnemyController.instance.BombEvent();  //work on this later
		UIManager.instance.DestroyAllEnemies ();
		//EnemySpawner.instance.bombCnt = 0;
	}

    #endregion

    #region Additional code not used atm

    /*public void SubmitSliderSetting()
	{
		if (movement.GetComponent<Slider> ().value < 0) 
		{
			goleft = true;
		}
		if (movement.GetComponent<Slider> ().value > 0) 
		{
			goright = true;
		}
	}*/

    /*private bool shouldDiscardSwipe(Vector2 touchPos)
	{
		PointerEventData touch = new    PointerEventData(EventSystem.current);
		touch.position = touchPos;
		List<RaycastResult> hits = new List<RaycastResult>();
		EventSystem.current.RaycastAll(touch, hits);
		//Debug.Log (hits.Count);
		return (hits.Count > 0); // discard swipe if an UI element is beneath

	}*/

    #endregion
}
