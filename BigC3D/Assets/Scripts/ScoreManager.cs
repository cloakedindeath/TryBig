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
	public Slider hpBar;
	public float timedLivesReturn;
	public GameObject resumeButton;
	public bool livesGone = false;
	public bool startLives = true;
	public int hp;
	public AudioSource audioControl;
	public AudioClip mpDing;
	public bool ding;
	public int dingCnt = 1;
	public GameObject waitPanel;
	//public GameObject resumeRewardButton;


	void Awake()
	{
		
		if(instance == null)
		{
			instance = this;
		}
		if(lives > 0)
		{
			hp = 10;
		}
	}

	// Use this for initialization
	void Start () 
	{
		ding = false;
		dingCnt = 1;
		audioControl = GetComponent<AudioSource>();
		//PlayerPrefs.SetInt ("lives", lives);
		//PlayerPrefs.GetInt("lives");
		score = 0;
		//PlayerPrefs.SetInt ("Score", 0);
		points = 20;
		if(lives >= 3)
		{
			hp = 10;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		SubmitSliderSetting ();

		#region Multiplier UI and Sound Control
		//Play sound when multiplier reached
		if(UIManager.instance.mpCnt == 10 && !audioControl.isPlaying)
		{
			if(!audioControl.isPlaying)
			{
				UIManager.instance.mpCnt = 10.5f;
				audioControl.pitch = 1.2f;
				audioControl.PlayOneShot (mpDing, 1f);

			}
		}
		if(UIManager.instance.mpCnt >= 10)
		{
			ding = true;
		}
		if(UIManager.instance.mpCnt == 30 && ding == true)
		{
			if(!audioControl.isPlaying)
			{
				UIManager.instance.mpCnt = 30.5f;
				audioControl.pitch = 1.4f;
				audioControl.PlayOneShot (mpDing,1f);
				//ding = false;
			}
		}

		if(UIManager.instance.mpCnt == 0 && UIManager.instance.gameOver == false )
		{

			if(!audioControl.isPlaying && dingCnt == 0)
			{
				audioControl.pitch = 0.1f;
				audioControl.PlayOneShot (mpDing, 1f);
				dingCnt++;
			}

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
		else if ( UIManager.instance.mpCnt >= 30 && ding == true)//may need to fix this later
		{
			mp.text = "x3";
			if(!audioControl.isPlaying && dingCnt == 1)
			{
				audioControl.pitch = 1.4f;
				audioControl.PlayOneShot (mpDing, 1f);
				dingCnt++;
			}
			//audioControl.PlayOneShot (mpDing);

		}
		#endregion

		UIManager.instance.highScoreText.text ="High Score: " + PlayerPrefs.GetInt ("HighScore").ToString ();

		Debug.Log (PlayerPrefs.GetInt ("FirstTime"));

		if (livesGone == false && PlayerPrefs.GetInt("FirstTime") == 0) 
		{
			//PlayerPrefs.SetInt ("lives", 3);
			lives = 3;
			PlayerPrefs.SetInt ("FirstTime", 1);
			SetFirstTimeLives();
			//UIManager.instance.livesTimerOB.SetActive (false);
			//UIManager.instance.livesTimerOB.GetComponent<PersistentTimer> ().enabled = false;
		}
		if( PlayerPrefs.GetInt("FirstTime") == 1)
		{
			if(lives <= 0 )
			{
				//PlayerPrefs.SetInt ("Score", score);
				//PlayerPrefs.SetInt ("lives", 0);
				lives = 0;
				GameObject.Find ("EnemySpawner").GetComponent<EnemySpawner> ().StopSpawning ();
				GameObject.Find ("Player").GetComponent<TouchTest> ().enabled = false;
				shootButton.GetComponent<Button> ().interactable = false;
				resumeButton.GetComponent<Button> ().interactable = false;
				resumeButton.gameObject.SetActive (false);
				UIManager.instance.GameOver ();
				//waitPanel.SetActive (true);
				//UIManager.instance.livesTimerOB.SetActive (true);
				//UIManager.instance.livesTimerOB.GetComponent<PersistentTimer> ().enabled = true;
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
				resumeButton.gameObject.SetActive (true);
				//UIManager.instance.livesTimerOB.SetActive (false);
				ding = false;
				//waitPanel.SetActive (false);
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
		hp = (hp - 1);
		TouchTest.instance.model.GetComponent<Animator> ().Play ("ANIM_Player_TakeDamage_01");
		if(hp <= 0)
		{
			
			//PlayerPrefs.SetInt ("lives", PlayerPrefs.GetInt ("lives") - 1);
			lives = lives - 1;
			PlayerPrefs.SetInt ("Lives", PlayerPrefs.GetInt ("Lives") - 1);
			hp = 10;
			UIManager.instance.GameOver ();
			//StartCoroutine (ANIMpause ());
			//UIManager.instance.waveEndPanel.SetActive (false);
			//UIManager.instance.touchCnt = 0;
		}
	

	}

	public void SubmitSliderSetting()
	{
		//Debug.Log (mpBar.value);
		mpBar.value = UIManager.instance.mpCnt;
		hpBar.value = hp;
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

	public void paidAddLife()
	{
		UnityAdManager.instance.ShowAd();
		RewardButton.instance.freeLife = true;
		//resumeRewardButton.SetActive (true);
		//PersistentTimer.instance.savedSeconds = 60;
		//waitPanel.GetComponent<Animator> ().Play ("waitPanelAway");
		PlayerPrefs.SetInt ("Lives", PlayerPrefs.GetInt ("Lives") + 1);
		lives = lives + 1;
	}

	IEnumerator ANIMpause()
	{
		yield return new WaitForSeconds (3f);
		UIManager.instance.GameOver ();
	}
}
