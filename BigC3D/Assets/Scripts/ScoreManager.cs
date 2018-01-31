using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;

	public int score;
	public int lives;
	public Button shootButton;
	//public int mpAmt;
	public int points = 20;
	public GameObject mpText;
	public Text mp;
	public Slider mpBar;
	public float timedLivesReturn;
	public Button resumeButton;
	public bool livesGone = false;
	public bool startLives = true;

	void Awake()
	{
		
		if(instance == null)
		{
			instance = this;
		}
	}

	// Use this for initialization
	void Start () 
	{
		//PlayerPrefs.SetInt ("lives", lives);
		PlayerPrefs.GetInt("lives");
		score = 0;
		PlayerPrefs.SetInt ("Score", 0);
		points = 20;

		//mpAmt = 0;
		/*if(timedLivesReturn == 0)
		{
			lives = 3;
			PlayerPrefs.SetInt ("lives", lives);
		}*/

		//PlayerPrefs.DeleteKey ("FirstTime");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log (PlayerPrefs.GetInt ("FirstTime"));

		if (livesGone == false && PlayerPrefs.GetInt("FirstTime") == 0) 
		{
			PlayerPrefs.SetInt ("lives", 3);
			PlayerPrefs.SetInt ("FirstTime", 1);
			SetFirstTimeLives();
			UIManager.instance.livesTimerOB.SetActive (false);
			UIManager.instance.livesTimerOB.GetComponent<PersistentTimer> ().enabled = false;
		}
		if( PlayerPrefs.GetInt("FirstTime") == 1)
		{
			if(PlayerPrefs.GetInt("lives") <= 0 )
			{
				PlayerPrefs.SetInt ("Score", score);
				PlayerPrefs.SetInt ("lives", 0);
				GameObject.Find ("EnemySpawner").GetComponent<EnemySpawner> ().StopSpawning ();
				GameObject.Find ("Player").GetComponent<TouchTest> ().enabled = false;
				shootButton.GetComponent<Button> ().interactable = false;
				resumeButton.GetComponent<Button> ().interactable = false;
				UIManager.instance.GameOver ();
				UIManager.instance.livesTimerOB.SetActive (true);
				UIManager.instance.livesTimerOB.GetComponent<PersistentTimer> ().enabled = true;
				//lives = 0;

				/*if(livesGone == false)
			{
				livesGone = true;
				//timedLivesReturn = 20000f;
				UIManager.instance.livesTimerOB.SetActive (true);
				UIManager.instance.livesTimerOB.GetComponent<PersistentTimer> ().enabled = true;
			}*/

			}
			else
			{
				resumeButton.GetComponent<Button> ().interactable = true;
				UIManager.instance.livesTimerOB.SetActive (false);
			}
		}

	
		/*if(livesGone == true)
		{
			//timedLivesReturn = 18000.0f;
			//TimerStart ();
			if(timedLivesReturn <= 0)
			{
				livesGone = false;
				//lives = 3;
				PlayerPrefs.SetInt ("lives", 3);
			}
		}*/

		SubmitSliderSetting ();

		//TimerStart ();

		if(UIManager.instance.mpCnt == 0)
		{
			mp.text = " ";
			mpBar.minValue = 0;
			mpBar.maxValue = 10;
		}
		else if(UIManager.instance.mpCnt >= 10 && UIManager.instance.mpCnt < 30)
		{

			mp.text = "x2";
			mpBar.minValue = 10;
			mpBar.maxValue = 30;
		}
		else if ( UIManager.instance.mpCnt >= 30)
		{
			
			mp.text = "x3";
		}
	

		UIManager.instance.highScoreText.text ="High Score: " + PlayerPrefs.GetInt ("HighScore").ToString ();
	}

	public void SetPlayerScores()
	{
		if(PlayerPrefs.HasKey("HighScore"))
		{
			if(score > PlayerPrefs.GetInt("HighScore"))
			{
				PlayerPrefs.SetInt ("HighScore", score);
			}
		}
		else
		{
			PlayerPrefs.SetInt ("HighScore", score);
		}
	}
	public void EnemyKill()
	{
		
		if(UIManager.instance.mpCnt >= 10 && UIManager.instance.mpCnt < 30)
		{
			score += (points * 2);
		}
		else if ( UIManager.instance.mpCnt >= 30)
		{
			score += (points * 3);
		}
		else
		{
			score += points;
		}


	}
	public void LoseLife()
	{
		//lives = lives - 1;
		PlayerPrefs.SetInt ("lives", PlayerPrefs.GetInt ("lives") - 1);
		//PlayerPrefs.SetInt ("lives", lives);

	}

	public void SubmitSliderSetting()
	{
		Debug.Log (mpBar.value);
		mpBar.value = UIManager.instance.mpCnt;
	}

	void TimerStart()
	{
		//timedLivesReturn -= Time.deltaTime;

	}

	void SetFirstTimeLives()
	{
		if(PlayerPrefs.HasKey("FirstTime"))
		{
			if(PlayerPrefs.GetInt("FirstTime") == 1)
			{
				
			}
		}
		else
		{
			PlayerPrefs.SetInt ("FirstTime", 1);
		}
	}

}
