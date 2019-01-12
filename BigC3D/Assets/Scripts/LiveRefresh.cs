﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class LiveRefresh : MonoBehaviour {

	//3600000 = 1 hr
	//
	public float msToWait = 15000f;

	public Text timer2;
	public Text message;
	public Button chestButton;
	private ulong lastChestOpen;
	public bool freeLife;

	public static LiveRefresh instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		chestButton = GetComponent<Button> ();
		lastChestOpen =  ulong.Parse(PlayerPrefs.GetString ("LivesRefresh"));
		timer2 = GetComponentInChildren<Text> ();

		if(!isChestReady())
		{
			chestButton.interactable = false;
		}
	}

	private void Update()
	{
		if (PlayerPrefs.GetInt ("Lives") > 0)
		{
			UIManager.instance.livesLostMessage.SetActive (false);
			UIManager.instance.livesLostTimer.SetActive (false);
		}
		if(!chestButton.IsInteractable())
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
			timer2.text = r;
			message.text = "Wait for timer to refresh lives";
		}
	}

	public void ChestClick()
	{
		//Debug.Log( DateTime.Now.Ticks.ToString());
		freeLife = true;
		UnityAdManager.instance.rewardAd();
		ScoreManager.instance.hp = 10;
		lastChestOpen = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString ("LivesRefresh", DateTime.Now.Ticks.ToString ());
		chestButton.interactable = false;
		UIManager.instance.gameOver = true;
		// Gives lives back or reward the player
		//ScoreManager.instance.resumeRewardButton.SetActive (true);
		//ScoreManager.instance.resumeButton.SetActive (false);
		//ScoreManager.instance.lives = ScoreManager.instance.lives + 1;
		PlayerPrefs.SetInt ("Lives_Reward", PlayerPrefs.GetInt ("Lives_Reward") + 1);
		//UIManager.instance.livesLostMessage.SetActive (false);
		//UIManager.instance.livesLostTimer.SetActive (false);
		//PlayerPrefs.SetInt ("lives", PlayerPrefs.GetInt ("lives") + 1);
		//ScoreManager.instance.waitPanel.GetComponent<Animator> ().Play ("waitPanelAway");
	}

	public void Deathcheck()
	{
		if (ScoreManager.instance.lives <= 0 /*&& RewardButton.instance.freeLife == false*/) {

			//timerActive = true;
			PlayerPrefsX.SetBool("Timer1",true);
			lastChestOpen = (ulong)DateTime.Now.Ticks;
			PlayerPrefs.SetString ("LivesRefresh", DateTime.Now.Ticks.ToString ());
			chestButton.interactable = false;
			PlayerPrefs.SetInt ("Lives", 0);
		}


	}


	private bool isChestReady()
	{
		ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
		ulong m = diff / TimeSpan.TicksPerMillisecond;

		float secondsLeft = ((float)msToWait - m) / 1000f;
		if(secondsLeft < 0)
		{
			message.text = "Click to restore lives.";
			timer2.text = "Restore Lives";
			return true;
		}
		else
		{

			return false;
		}

	}

}
