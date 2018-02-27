using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

//using GoogleMobileAds.Api;


public class LeaderBoardManager : MonoBehaviour 
{
	
	public static LeaderBoardManager instance;

	void Awake()
	{
		PlayGamesPlatform.Activate();
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()

			.Build();

		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
	
		if (! PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.Authenticate ((bool success) => {
				if (success) {
					/// Signed in! Hooray!
				} else {
					/// Not signed in. We'll want to show a sign in button
				}
			}, true);   /// <--- That "true" is very important!
		} else {
			Debug.Log("We're already signed in");
		}
		
		if(instance == null)
		{
			instance = this;
		}

	}

	#region DEFAULT_UNITY_CALLBACKS
	void Start ()
	{
		// recommended for debugging:
		//PlayGamesPlatform.DebugLogEnabled = true;

		// Activate the Google Play Games platform
		//PlayGamesPlatform.Activate ();
	}
	#endregion
	#region BUTTON_CALLBACKS
	/// <summary>
	/// Login In Into Your Google+ Account
	/// </summary>
	public void LogIn ()
	{
		Social.localUser.Authenticate ((bool success) =>
			{
				if (success) {
					Debug.Log ("Login Sucess");
				} else {
					Debug.Log ("Login failed");
				}
			});
	}
	/// <summary>
	/// Shows All Available Leaderborad
	/// </summary>
	public void OnShowLeaderBoard ()
	{
		//        Social.ShowLeaderboardUI (); // Show all leaderboard
		((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (LeaderBoard.leaderboard_highest_score); // Show current (Active) leaderboard
	}
	/// <summary>
	/// Adds Score To leader board
	/// </summary>
	public void OnAddScoreToLeaderBoard ()
	{
		if (Social.localUser.authenticated) {
			Social.ReportScore (PlayerPrefs.GetInt ("HighScore"), LeaderBoard.leaderboard_highest_score, (bool success) =>
				{
					if (success) {
						Debug.Log ("Update Score Success");

					} else {
						Debug.Log ("Update Score Fail");
					}
				});
		}
	}
	/// <summary>
	/// On Logout of your Google+ Account
	/// </summary>
	public void OnLogOut ()
	{
		((PlayGamesPlatform)Social.Active).SignOut ();
	}
	#endregion
	/*void OnStart()
	{
		Social.localUser.Authenticate((bool success) => {
			return;
		});
	}
	// Use this for initialization
	void Start () 
	{
		// PASTE THESE LINES AT THE END OF Start()
		// Try silent sign-in (second parameter is isSilent)
		PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);
	}*/
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	/*public void Login()
	{
		 if (!PlayGamesPlatform.Instance.localUser.authenticated) {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        } else {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();
		}
	}*/

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
			LogIn ();
		}

	}

	public void SignInCallback(bool success) {
		if (success) {
			Debug.Log("(Lollygagger) Signed in!");

			// Change sign-in button text
			//signInButtonText.text = "Sign out";

			// Show the user's name
			//authStatus.text = "Signed in as: " + Social.localUser.userName;
		} else {
			Debug.Log("(Lollygagger) Sign-in failed...");

			// Show failure message
			//signInButtonText.text = "Sign in";
			//authStatus.text = "Sign-in failed";
		}
	}
}
//PlayGamesPlatform.Activate ();

		/*#if UNITY_ANDROID*/
		//  ADD THIS CODE BETWEEN THESE COMMENTS

		// Create client configuration
		//PlayGamesClientConfiguration config = new 
			//PlayGamesClientConfiguration.Builder()
			//.Build();

		// Enable debugging output (recommended)
		//PlayGamesPlatform.DebugLogEnabled = true;

		// Initialize and activate the platform
		//PlayGamesPlatform.InitializeInstance(config);*/
		/*PlayGamesPlatform.Activate();
		// END THE CODE TO PASTE INTO START
		#endif*/
		//Login();