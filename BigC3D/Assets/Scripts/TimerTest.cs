using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerTest : MonoBehaviour 
{
	public static TimerTest instance;
	public float msToWait = 15000f;

	public Text timer;
	public Text t2;
	public Button chestButton;
	private ulong lastChestOpen;
	public GameObject waitPanel;
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
		chestButton = GetComponent<Button> ();
		lastChestOpen =  ulong.Parse(PlayerPrefs.GetString ("LastRewardGiven"));
		timer = GetComponentInChildren<Text> ();

		if(!isChestReady())
		{
			chestButton.interactable = false;
		}
	}

	private void Update()
	{
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
		}*/

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
		}
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
		//UIManager.instance.GoBackToMenu();
	}

	public void Deathcheck()
	{
		if (ScoreManager.instance.lives <= 0 && RewardButton.instance.freeLife == false) {
			timerActive = true;
			lastChestOpen = (ulong)DateTime.Now.Ticks;
			PlayerPrefs.SetString ("LastRewardGiven", DateTime.Now.Ticks.ToString ());
			chestButton.interactable = false;
		}


	}

	private bool isChestReady()
	{
		ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
		ulong m = diff / TimeSpan.TicksPerMillisecond;

		float secondsLeft = ((float)msToWait - m) / 1000f;
		//Debug.Log (secondsLeft);
		if(secondsLeft < 0 )
		{
			//ScoreManager.instance.resumeRewardButton.SetActive (false);
			//ScoreManager.instance.resumeButton.SetActive (true);
			//ScoreManager.instance.hp = 10;
			//ScoreManager.instance.lives = ScoreManager.instance.lives + 3;
			//ScoreManager.instance.lives += 3;
			//RewardButton.instance.freeLife = false;
			timer.text = "Restore Lives";
			t2.text = "CLick to restore lives.";
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
			t2.text = "All lives lost!\nWait for the timer or purchase more lives.";
			return false;
		}

	}
}
