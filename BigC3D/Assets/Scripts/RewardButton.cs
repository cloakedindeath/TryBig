using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RewardButton : MonoBehaviour {

	//3600000 = 1 hr
	//
	public float msToWait = 15000f;

	public Text timer2;
	public Button chestButton;
	private ulong lastChestOpen;

	private void Start()
	{
		chestButton = GetComponent<Button> ();
		lastChestOpen =  ulong.Parse(PlayerPrefs.GetString ("RewardGiven"));
		timer2 = GetComponentInChildren<Text> ();

		if(!isChestReady())
		{
			chestButton.interactable = false;
		}
	}

	private void Update()
	{
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
		}
	}

	public void ChestClick()
	{
		//Debug.Log( DateTime.Now.Ticks.ToString());
		UnityAdManager.instance.rewardAd();
		ScoreManager.instance.hp = 10;
		lastChestOpen = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString ("RewardGiven", DateTime.Now.Ticks.ToString ());
		chestButton.interactable = false;
		UIManager.instance.gameOver = true;
		// Gives lives back or reward the player
		ScoreManager.instance.resumeRewardButton.SetActive (true);
		ScoreManager.instance.resumeButton.gameObject.SetActive (false);
		PlayerPrefs.SetInt ("lives", PlayerPrefs.GetInt ("lives") + 1);
		ScoreManager.instance.waitPanel.GetComponent<Animator> ().Play ("waitPanelAway");
	}

	private bool isChestReady()
	{
		ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
		ulong m = diff / TimeSpan.TicksPerMillisecond;

		float secondsLeft = ((float)msToWait - m) / 1000f;
		if(secondsLeft < 0)
		{
			timer2.text = "Watch Ad";
			return true;
		}
		else
		{
			return false;
		}

	}

}
