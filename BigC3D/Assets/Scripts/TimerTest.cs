using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerTest : MonoBehaviour 
{
	public static TimerTest instance;
	public float msToWait = 15000f;  //10,800,000 for 3 hours - 600,000 for 10 minutes 

	public Text timer;
	public Text t2;
	public Button chestButton;
	private ulong lastChestOpen;
	//public GameObject waitPanel;
	AudioSource audioT;
	public AudioClip click;
	public bool timerActive;


	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	private void Start()
	{
		audioT= GetComponent<AudioSource>();
		//chestButton = GetComponent<Button> ();

		lastChestOpen =  ulong.Parse(PlayerPrefs.GetString ("LastRewardGiven"));
		timer = GetComponentInChildren<Text> ();
		//timerActive = true;
		if(!isChestReady())
		{
			chestButton.interactable = false;
		}
		timerActive = PlayerPrefsX.GetBool ("Timer1");
		//ScoreManager.instance.lives = PlayerPrefs.GetInt ("Lives");
	
	}

	private void Update()
	{
		if(ScoreManager.instance.lives > 0)
		{
			timerActive = false;
		}
		else{
			timerActive = true;
			//RewardButton.instance.freeLife = false;
		}
		Debug.Log (PlayerPrefsX.GetBool ("Timer1"));
		Debug.Log(PlayerPrefs.GetInt("Lives"));
		Debug.Log (RewardButton.instance.freeLife);
		if (ScoreManager.instance.lives <= 0) 
		{
			UIManager.instance.livesLostMessage.SetActive (true);
			UIManager.instance.livesLostTimer.SetActive (true);
		}
		//Set the Timer
		/*ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
		ulong m = diff / TimeSpan.TicksPerMillisecond;
		float secondsLeft = ((float)msToWait - m) / 1000f;

		string r = "";
		//Hours
		r += ((int)secondsLeft / 3600).ToString() + "h ";
		secondsLeft -= ((int)secondsLeft / 3600) * 3600;
		//Minutes
		r += ((int)secondsLeft / 60).ToString("00") + "m ";
		//Seconds
		r += (secondsLeft % 60).ToString("00") + "s";
		timer.text = r;
		if (secondsLeft < 0)
		{
			t2.text = "Click for lives.";
			timer.text = "Restore Lives";
			chestButton.interactable = true;
			//ScoreManager.instance.hp = 10;
			//ScoreManager.instance.lives = ScoreManager.instance.lives + 3;
			//RewardButton.instance.freeLife = false;
		}
		else{
			t2.text = "All lives lost!\nWait for the timer or purchase more lives.";
			chestButton.interactable = false;
		}

		if(chestButton.interactable == false )
		{
			if(isChestReady())
			{
				chestButton.interactable = true;

				return;
			}
			//Set the Timer

			ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
			ulong m = diff / TimeSpan.TicksPerMillisecond;
			float secondsLeft = ((float)msToWait - m) / 1000f;

			string r = "";
			//Hours
			r += ((int)secondsLeft / 3600).ToString() + "h ";
			secondsLeft -= ((int)secondsLeft / 3600) * 3600;
			//Minutes
			r += ((int)secondsLeft / 60).ToString("00") + "m ";
			//Seconds
			r += (secondsLeft % 60).ToString("00") + "s";
			timer.text = r;
		}*/
		ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
		ulong m = diff / TimeSpan.TicksPerMillisecond;
		float secondsLeft = ((float)msToWait - m) / 1000f;

		/*if (secondsLeft <= 0) {
			secondsLeft = 0;
			UIManager.instance.livesLostMessage.SetActive (false);					//Removes countdown message
			UIManager.instance.livesLostTimer.SetActive (false);	
		}*/

		string r = "";
		//Hours
		r += ((int)secondsLeft / 3600).ToString() + "h ";
		secondsLeft -= ((int)secondsLeft / 3600) * 3600;
		//Minutes
		r += ((int)secondsLeft / 60).ToString("00") + "m ";
		//Seconds
		r += (secondsLeft % 60).ToString("00") + "s";
		timer.text = r;
	}

	public void ChestClick()
	{
		//Debug.Log( DateTime.Now.Ticks.ToString());
		//lastChestOpen = (ulong)DateTime.Now.Ticks;
		//PlayerPrefs.SetString ("LastRewardGiven", DateTime.Now.Ticks.ToString ());
		//chestButton.interactable = false;
		//audioT.PlayOneShot(click,1f);
		// Gives lives back or reward the player
		ScoreManager.instance.hp = 10;
		ScoreManager.instance.lives = ScoreManager.instance.lives + 3;
		RewardButton.instance.freeLife = false;
		timerActive = false;
		PlayerPrefsX.SetBool("Timer1",false);
		PlayerPrefs.SetInt ("Lives", PlayerPrefs.GetInt("Lives") + 3);
		UIManager.instance.livesLostMessage.SetActive (false);					//Removes countdown message
		UIManager.instance.livesLostTimer.SetActive (false);					//Removes Lives Restore Button

		//UIManager.instance.GoBackToMenu();
	}

	public void Deathcheck()
	{
		if (ScoreManager.instance.lives <= 0 && RewardButton.instance.freeLife == false) {
			
			timerActive = true;
			PlayerPrefsX.SetBool("Timer1",true);
			lastChestOpen = (ulong)DateTime.Now.Ticks;
			PlayerPrefs.SetString ("LastRewardGiven", DateTime.Now.Ticks.ToString ());
			chestButton.interactable = false;
			PlayerPrefs.SetInt ("Lives", 0);
		}


	}

	private bool isChestReady()
	{
		ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
		ulong m = diff / TimeSpan.TicksPerMillisecond;

		float secondsLeft = ((float)msToWait - m) / 1000f;
		//Debug.Log (secondsLeft);
		if(secondsLeft <= 0 && timerActive == true )
		{
			secondsLeft = 0;
			//ScoreManager.instance.resumeRewardButton.SetActive (false);
			//ScoreManager.instance.resumeButton.SetActive (true);
			//ScoreManager.instance.hp = 10;
			//ScoreManager.instance.lives = ScoreManager.instance.lives + 3;
			//ScoreManager.instance.lives += 3;
			//RewardButton.instance.freeLife = false;
			RewardButton.instance.freeLife = false;
			timer.text = "Restore Lives";
			t2.text = "Click to restore lives.";
			//timer.text = "";
			//ScoreManager.instance.hp = 10;
			//ChestClick ();
			//PlayerPrefs.SetInt ("lives", 3);
			//ScoreManager.instance.waitPanel.GetComponent<Animator> ().Play ("waitPanelAway");
			//waitPanel.SetActive(false);
			//UIManager.instance.GoBackToMenuSetLives();
			return true;
		}
		else
		{
			t2.text = "Lives restore in 10 minutes.";
			return false;
		}

	}
}
