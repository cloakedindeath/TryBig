using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RewardButton : MonoBehaviour {

	//3600000 = 1 hr
	//
	public float msToWait = 15000f;

	private Text timer2;
	private Button chestButton;
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
		lastChestOpen = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString ("RewardGiven", DateTime.Now.Ticks.ToString ());
		chestButton.interactable = false;

		// Gives lives back or reward the player

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
			timer2.text = "Watch Ad for 1 life";
			return true;
		}
		else
		{
			return false;
		}

	}

}
