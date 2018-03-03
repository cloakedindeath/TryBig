using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdManager : MonoBehaviour 
{
	public static UnityAdManager instance;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);

		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy (this.gameObject);
		}

		Advertisement.Initialize ("1675995", false);
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log (PlayerPrefs.GetInt ("Adcount"));
	}

	/*public void ShowAd_timed()
	{
		if(PlayerPrefs.HasKey("Adcount"))
		{
			if(PlayerPrefs.GetInt("Adcount") == 3)
			{
				if(Advertisement.IsReady("video"))
				{
					Advertisement.Show ("video");
				}

				PlayerPrefs.SetInt ("Adcount", 0);
			}
			else
			{
				PlayerPrefs.SetInt ("Adcount", (PlayerPrefs.GetInt ("Adcount") + 1));
			}
		}
		else
		{
			PlayerPrefs.SetInt ("Adcount", 0);
		}
	}*/

	public void ShowAd()
	{
		if(Advertisement.IsReady("video"))
		{
			Advertisement.Show ("video");
		}
	}
	public void rewardAd()
	{
		if(Advertisement.IsReady("rewardedVideo"))
		{
			Advertisement.Show ("rewardedVideo");
		}
	}

	public void ShowAd2()
	{
		if(PlayerPrefs.HasKey("Adcount"))
		{
			if(PlayerPrefs.GetInt("Adcount") == 2)
			{
				if(Advertisement.IsReady("video"))
				{
					Advertisement.Show ("video");
				}

				PlayerPrefs.SetInt ("Adcount", 0);
			}
			else
			{
				PlayerPrefs.SetInt ("Adcount", (PlayerPrefs.GetInt ("Adcount") + 1));
			}
		}
		else
		{
			PlayerPrefs.SetInt ("Adcount", 0);
		}
	}

}
