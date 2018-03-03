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
	public Button chestButton;
	private ulong lastChestOpen;
	public GameObject waitPanel;
	AudioSource audioT;
	public AudioClip click;


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
		/*if (PlayerPrefs.GetInt ("lives") <= 0) {
			ScoreManager.instance.waitPanel.SetActive (true);
		}
		else if (PlayerPrefs.GetInt ("lives") >= 1) {
			ScoreManager.instance.waitPanel.SetActive (false);
		}*/
	
		if(chestButton.interactable == false && UIManager.instance.gameOver == true)
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
		PlayerPrefs.SetInt ("lives", PlayerPrefs.GetInt("lives" + 3));
		ScoreManager.instance.waitPanel.GetComponent<Animator> ().Play ("waitPanelAway");
		//waitPanel.SetActive(false);
		UIManager.instance.GoBackToMenu();

	}

	public void Deathcheck()
	{
		if (PlayerPrefs.GetInt ("lives") <= 0) {
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
		if(secondsLeft < 0 )
		{
			ScoreManager.instance.resumeRewardButton.SetActive (false);
			//ScoreManager.instance.resumeButton.gameObject.SetActive (true);
			//timer.text = "";
			ScoreManager.instance.hp = 10;
			//ChestClick ();
			PlayerPrefs.SetInt ("lives", 3);
			//ScoreManager.instance.waitPanel.GetComponent<Animator> ().Play ("waitPanelAway");
			//waitPanel.SetActive(false);
			UIManager.instance.GoBackToMenu();
			return true;
		}
		else
		{
			return false;
		}

	}
}
