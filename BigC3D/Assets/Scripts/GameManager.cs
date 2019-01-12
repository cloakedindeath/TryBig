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
		}
	}


	//Attached to the Play game button on the GameStartPanel
	public void StartGame()
	{
		//lives check
		if(ScoreManager.instance.lives >= 1)
		{
			ScoreManager.instance.hp = 10;				//Make healthbar full
			UIManager.instance.GameStart ();			//Trigger GameStart function in UIManager
			ScoreManager.instance.startLives = false;	//NOT SURE WHAT THIS IS DOING ATM

		}
	

	}
}
