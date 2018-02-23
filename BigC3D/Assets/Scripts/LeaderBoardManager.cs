using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class LeaderBoardManager : MonoBehaviour 
{
	
	public static LeaderBoardManager instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		//PlayGamesPlatform.Activate ();

		#if UNITY_ANDROID
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			.RequestEmail()
			.Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		#endif
		//Login();
	}

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Login()
	{
		Social.localUser.Authenticate ((bool success) => {
			if(!success)
			{
				Debug.Log("Not Successful");
			}
			else
			{
				Debug.Log("Successful");
			}
		});
	}

	public void AddScoreToLeaderboard()
	{
		Social.ReportScore (PlayerPrefs.GetInt ("HighScore"), "CgkIsvTzoaYHEAIQAA", (bool success) => {
			if(success)
			{
				Debug.Log("Added Score");
			}
		});
	}

	public void _ShowLeaderboard()
	{
		
		//PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIsvTzoaYHEAIQAA");
		//Social.ShowLeaderboardUI ();
		if(Social.localUser.authenticated)
		{
			Debug.Log ("Made it");
			//PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderBoard.leaderboard_highest_score);
			PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIsvTzoaYHEAIQAA");
			//PlayGamesPlatf

		}
		else
		{
			Login ();
		}

	}
}
