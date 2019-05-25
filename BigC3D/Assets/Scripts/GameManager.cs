using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

	public static GameManager instance;
	public int overallLives;


	void Awake()
	{
		//DontDestroyOnLoad(this.gameObject);

		if(instance == null)
		{
			instance = this;
		}
		else
		{
			//Destroy(this.gameObject);
		}
		/*if(PlayerPrefs.HasKey("Lives"))
		{
			ScoreManager.instance.lives = PlayerPrefs.GetInt ("Lives");
		}*/
		//mainLivesReset = PlayerPrefs.GetInt ("Lives");
	}

	// Use this for initialization
	void Start () 
	{
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
	}

	// Update is called once per frame
	void Update () 
	{
		overallLives = (PlayerPrefs.GetInt("Lives") + PlayerPrefs.GetInt("Lives_Paid") + PlayerPrefs.GetInt("Lives_Reward"));  //Adds stock lives to paid lives for the game menu


		if (ScoreManager.instance.lives <= 0) 
		{
			UIManager.instance.livesLostMessage.SetActive (true);
			UIManager.instance.livesLostTimer.SetActive (true);
			TouchTest.instance.bombButton.SetActive (false);    // this ensures that the bomb button dissappears after losing a life
			TouchTest.instance.shield.SetActive (false);		// make sure no shield when losing a life
			TouchTest.instance.shieldText.SetActive (false);
		}
        if (ScoreManager.instance.hp <= 0)
        {
            TouchTest.instance.bombButton.SetActive(false);    // this ensures that the bomb button dissappears after losing a life
            TouchTest.instance.shield.SetActive(false);     // make sure no shield when losing a life
            TouchTest.instance.shieldText.SetActive(false);
        }
	}


	//Attached to the Play game button on the GameStartPanel
	public void StartGame()
	{
		//lives check
		if(ScoreManager.instance.lives >= 1)
		{
			ScoreManager.instance.hp = 3;				//Make healthbar full
			UIManager.instance.GameStart ();			//Trigger GameStart function in UIManager
			ScoreManager.instance.startLives = false;	//ensures the player never gets the initial starting 3 lives again

		}
	

	}
}
