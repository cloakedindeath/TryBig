﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour 
{
	public static UIManager instance;

	//public GameObject titlePanel;
	public bool gameOver;
	public bool startCountdown;
	public bool startWaveCountdown;
	public GameObject quitButton;
	public GameObject player;
	public float timeCountDown;
	public GameObject startGamePanel;
	public GameObject waveStartPanel;
	public GameObject waveEndPanel;
	public float gameStartCountdown;
	public float inbetweenTimer;
	public int waveCount;
	public GameObject shootButton;
	public GameObject howToPanel;
	public GameObject pausePanel;
	public GameObject shopPanel;
	public GameObject shopPanelPause;
	public GameObject[] controls;
	public Slider mpBar;

	public Text scoreText;
	public Text livesText;
	public Text menuLivesText;
	public Text waveTimerText;
	public Text startCountdownTimerText;
	public Text waveOverScoreText;
	public Text waveOverText;
	public Text waveIndicatorText;
	public Text highScoreText;
	public Text furthestWaveText;
	//public Text livesTimer;
	public GameObject livesTimerOB;

	public GameObject gameOverPanel;
	public Text gameOverScore;
	GameObject[] enemiesW;
	GameObject[] enemiesC;
	GameObject[] enemiesK;
	GameObject[] projectiles;
	public int touchCnt = 0;
	public float mpCnt;
	AudioSource audioU;
	public AudioClip deathSound;
	public AudioClip countdown;
	public AudioClip click;
	public bool death;
	public int deathCnt;


	void Awake()
	{
		
		if(instance == null)
		{
			instance = this;
		}
		gameOver = true;
		mpCnt = 0;
	}
	// Use this for initialization
	void Start () 
	{
		audioU = GetComponent<AudioSource>();
		death = false;
		//DontDestroyOnLoad (livesTimerOB);
		//enemyDestroyer = GameObject.Find ("EnemyDestroyer");
		//enemySpawner.GetComponent<EnemySpawner> ().enabled = false;
		startCountdownTimerText.gameObject.transform.localScale = new Vector3 (0,0,0);
		gameStartCountdown = -1f;
		timeCountDown = 0;
		startCountdown = false;
		startWaveCountdown = false;
		waveCount = 1;
		GameObject.Find ("Player").GetComponent<TouchTest> ().enabled = false;
		shootButton.GetComponent<Button> ().interactable = false;
		deathCnt = 5;
		//audio.volume = 0.3f;

	}
	// Update is called once per frame
	void Update () 
	{
		

		/////////////////////////////////
		//Start pre Wave countdown
		if(startCountdown == true && gameOver == false)
		{
			foreach (GameObject buttons in controls)
			{
				buttons.GetComponent<Button>().interactable = false;
			}

	
			//Tap to start round
			if( Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				touchCnt = touchCnt + 1;
			}
			if(touchCnt == 1)
			{
				audioU.PlayOneShot (countdown);
				touchCnt = 2;
				startCountdownTimerText.gameObject.transform.localScale = new Vector3 (1,1,1);
				gameStartCountdown = 3.5f;
			}
			gameStartCountdown -= Time.deltaTime;

			if (gameStartCountdown <= 0.85f && gameStartCountdown > 0) 
			{
				
				startCountdownTimerText.gameObject.transform.localScale = new Vector3 (0,0,0);
				startCountdown = false;
				startWaveCountdown = true;
				waveStartPanel.GetComponent<Animator> ().Play ("StartWaveCountdownRemover");
				timeCountDown = 60.5f;
				inbetweenTimer = 3.5f;
				touchCnt = 1;
				EnemySpawner.instance.PickEnemyType();
			}
		}
		//////////////////////////////
		//Start actual wave countdown
		else if(startWaveCountdown == true && gameOver == false)
		{
			
			EnemySpawner.instance.PickEnemyType();
			GameObject.Find ("Player").GetComponent<TouchTest> ().enabled = true;
			foreach (GameObject buttons in controls)
			{
				buttons.GetComponent<Button>().interactable = true;
			}
			shootButton.GetComponent<Button> ().interactable = true;
			timeCountDown -= Time.deltaTime;
			///////////////////////
			//wave over
			if( timeCountDown <= 0 && gameOver == false && ScoreManager.instance.hp > 0)
			{
				startWaveCountdown = false;
				if(startWaveCountdown == false)
				{
					foreach (GameObject buttons in controls)
					{
						buttons.GetComponent<Button>().interactable = false;
					}
					GameObject.Find ("Player").GetComponent<TouchTest> ().enabled = false;
				}
				timeCountDown = 0;
				waveEndPanel.SetActive(true);
				//touchCnt = 0;
				shootButton.GetComponent<Button> ().interactable = false;
				DestroyAllEnemies ();
				//touch to continue after score highlights
				/*if( Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
				{
					touchCnt = touchCnt + 1;

				}
				if(touchCnt == 2 && gameOver == false)
				{
					touchCnt = 3;
					audioU.PlayOneShot (countdown);
					startCountdownTimerText.gameObject.transform.localScale = new Vector3 (1,1,1);
					waveStartPanel.GetComponent<Animator> ().Play ("StartWaveCountPopUp");
					waveEndPanel.GetComponent<Animator> ().Play ("WaveEndAway");
					waveEndPanel.SetActive (false);
					startWaveCountdown = false;
					startCountdown = true;
					gameStartCountdown = 3.5f;
					waveCount = waveCount + 1;
					ScoreManager.instance.SetPlayerScores ();
				}*/
				//inbetweenTimer -= Time.deltaTime;
			}
			/*if (inbetweenTimer <= 0)
			{
				waveStartPanel.GetComponent<Animator> ().Play ("StartWaveCountPopUp");
				waveEndPanel.GetComponent<Animator> ().Play ("WaveEndAway");
				waveEndPanel.SetActive (false);
				startWaveCountdown = false;
				startCountdown = true;
				gameStartCountdown = 3.5f;
				waveCount = waveCount + 1;
			}*/

		}

		SetHighestWave ();

		/////////////////////////
		//Update text elements
		scoreText.text = "Score: " + ScoreManager.instance.score.ToString();
		livesText.text = "Lives: " + PlayerPrefs.GetInt("lives").ToString();
		menuLivesText.text = "Lives: " + PlayerPrefs.GetInt("lives").ToString();
		//livesTimer.text = ScoreManager.instance.timedLivesReturn.ToString ();
		waveTimerText.text = "Wave " + waveCount + " Timer: " + timeCountDown.ToString("f0");
		startCountdownTimerText.text = gameStartCountdown.ToString ("f0");
		waveOverScoreText.text = "Score: " + ScoreManager.instance.score.ToString ();
		waveOverText.text = "Wave " + waveCount + " Complete";
		waveIndicatorText.text = "Wave " + waveCount;
		furthestWaveText.text = "Wave Level Reached: " + PlayerPrefs.GetInt ("MostWaves").ToString ();
		//highScoreText.text = "High Score: " + PlayerPrefs.GetInt ("HighScore");

		/*if(waveCount == 10)
		{
			gameOver = true;
			GameOver ();
		}*/
		if(PlayerPrefs.GetInt("lives") <= 0 )
		{
			death = true;
		}

	}

	public void nextWaveStart()
	{
		touchCnt = 1;
		audioU.PlayOneShot (countdown);
		startCountdownTimerText.gameObject.transform.localScale = new Vector3 (1,1,1);
		waveStartPanel.GetComponent<Animator> ().Play ("StartWaveCountPopUp");
		waveEndPanel.GetComponent<Animator> ().Play ("WaveEndAway");
		waveEndPanel.SetActive (false);
		startWaveCountdown = false;
		startCountdown = true;
		gameStartCountdown = 3.5f;
		waveCount = waveCount + 1;
		ScoreManager.instance.SetPlayerScores ();
	}

	public void GameStart ()
	{
		audioU.PlayOneShot (click, 1f);
		waveEndPanel.SetActive (false);
		deathCnt = 0;
		PlayerPrefs.SetInt ("Score", 0);
		gameOver = false;
		startGamePanel.GetComponent<Animator> ().Play ("GameStartPanelDropDown");
		startCountdown = true;
		EnemySpawner.instance.spawnTime = 3.5f;
		mpCnt = 0;
		gameOverPanel.SetActive (false);

	}

	void DestroyAllEnemies()
	{
		enemiesW = GameObject.FindGameObjectsWithTag ("Enemy_Waffle");

		for(int i = 0; i < enemiesW.Length; i++) 
		{
			Destroy(enemiesW[i]);
		}
		enemiesC = GameObject.FindGameObjectsWithTag ("Enemy_Chicken");

		for(int i = 0; i < enemiesC.Length; i++) 
		{
			Destroy(enemiesC[i]);
		}
		enemiesK = GameObject.FindGameObjectsWithTag ("Enemy_KoolAid");

		for(int i = 0; i < enemiesK.Length; i++) 
		{
			Destroy(enemiesK[i]);
		}
		projectiles = GameObject.FindGameObjectsWithTag ("Projectile");

		for(int i = 0; i < projectiles.Length; i++) 
		{
			Destroy(projectiles[i]);
		}

	}

	/*public void StartEnemies()
	{
		enemySpawner.GetComponent<EnemySpawner> ().Start ();
	}*/

	public void goToMenu()
	{
		audioU.PlayOneShot (click, 1f);
		gameOver = true;
		PlayerPrefs.SetFloat ("TimeOnExit", PersistentTimer.instance.savedSeconds);
		SceneManager.LoadScene("Menu");
	}
		
	/*public void Shoot()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Rigidbody instantiatedProjectile = Instantiate (projectile,
				transform.position, 
				transform.rotation)
				as Rigidbody;
			instantiatedProjectile.velocity = transform.TransformDirection (new Vector3 (0, 0, speed));
		}
	}

	public void Shoot2()
	{
		Debug.Log ("Shooting");
		Rigidbody instantiatedProjectile = Instantiate (projectile,
			player.transform.position, 
			Quaternion.identity)
			as Rigidbody;
		instantiatedProjectile.velocity = transform.TransformDirection (new Vector3 (0, 0, -speed));
	}*/

	public void GameOver()
	{
		waveEndPanel.SetActive (false);
		UnityAdManager.instance.ShowAd2 ();
		gameOver = true;
		if(!audioU.isPlaying && deathCnt == 0 && gameOver == true)
		{
			audioU.PlayOneShot (deathSound);
			deathCnt = 5;
			death = false;
			gameOver = false;
		}

		timeCountDown = 0;
		gameOverPanel.SetActive (true);

		DestroyAllEnemies ();
		gameOverPanel.GetComponent<Animator> ().Play ("GameOverPopUp");
		gameOverScore.text = "Score: " + ScoreManager.instance.score.ToString();
		ScoreManager.instance.SetPlayerScores ();


	}

	public void RestartGame()
	{
		waveEndPanel.SetActive (false);
		audioU.PlayOneShot (click, 1f);
		gameOver = false;
		audioU.Stop ();
		GameObject.Find ("Player").GetComponent<TouchTest> ().enabled = false;
		shootButton.GetComponent<Button> ().interactable = false;
		waveCount = 1;
		//ScoreManager.instance.lives = 3;
		ScoreManager.instance.score = 0;
		PlayerPrefs.SetInt ("Score", 0);
		gameOver = false;
		//gameOverPanel.SetActive (false);
		gameOverPanel.GetComponent<Animator> ().Play ("GOAway");
		waveStartPanel.GetComponent<Animator> ().Play ("ResumeGame");
		startCountdown = true;
		startWaveCountdown = false;
		mpCnt = 0;
	}
	public void GoBackToMenu()
	{
		audioU.PlayOneShot (click, 1f);
		PauseResume ();
		DestroyAllEnemies ();
		audioU.Stop ();
		gameOver = false;
		//touchCnt = 0;
		//gameStartCountdown = 3.5f;
		gameOverPanel.GetComponent<Animator> ().Play ("GOAway");
		startGamePanel.GetComponent<Animator> ().Play ("BeginAnim");
		waveStartPanel.GetComponent<Animator> ().Play ("StartWaveCountPopUp");
		GameObject.Find ("Player").GetComponent<TouchTest> ().enabled = false;
		shootButton.GetComponent<Button> ().interactable = false;
		waveCount = 1;
		//ScoreManager.instance.lives = 3;
		ScoreManager.instance.score = 0;
		PlayerPrefs.SetInt ("Score", 0);
		//gameOver = false;
		gameOverPanel.SetActive (false);
		//waveStartPanel.GetComponent<Animator> ().Play ("ResumeGame");
		startCountdown = false;
		startWaveCountdown = false;
		mpCnt = 0;
		//SceneManager.LoadScene ("Main");
		startCountdownTimerText.gameObject.transform.localScale = new Vector3 (0,0,0);
		gameStartCountdown = -1f;
		timeCountDown = 0;
		touchCnt = 0;

	}

	public void SetHighestWave()
	{
		if(PlayerPrefs.HasKey("MostWaves"))
		{
			if(waveCount > PlayerPrefs.GetInt("MostWaves"))
			{
				PlayerPrefs.SetInt ("MostWaves", waveCount);
			}
		}
		else
		{
			PlayerPrefs.SetInt ("MostWaves", waveCount);
		}
	}

	public void OpenHowTo()
	{
		audioU.PlayOneShot (click, 1f);
		howToPanel.SetActive (true);
	}
	public void CloseHowTo()
	{
		audioU.PlayOneShot (click, 1f);
		howToPanel.GetComponent<Animator> ().Play ("HowToPopDown");
		StartCoroutine (HowToDisable ());
	}
	public void Pause()
	{
		audioU.PlayOneShot (click, 1f);
		pausePanel.SetActive (true);
		StartCoroutine (PauseTime ());
	}
	public void PauseResume()
	{
		audioU.PlayOneShot (click, 1f);
		Time.timeScale = 1;
		pausePanel.GetComponent<Animator> ().Play ("PauseAway");
		StartCoroutine (PauseDisable ());
	}
	public void PauseQuit()
	{
		audioU.PlayOneShot (click, 1f);
		Time.timeScale = 1;
		SceneManager.LoadScene ("Main");
	}
	public void OpenShop()
	{
		audioU.PlayOneShot (click, 1f);
		shopPanel.GetComponent<Animator> ().Play ("ShopPop");
	}
	public void pauseOpenShop()
	{
		audioU.PlayOneShot (click, 1f);
		Time.timeScale = 1;
		shopPanelPause.GetComponent<Animator> ().Play ("ShopPop");
		StartCoroutine (openPauseShop ());
	}
	public void pauseCloseShop()
	{
		audioU.PlayOneShot (click, 1f);
		Time.timeScale = 1;
		shopPanelPause.GetComponent<Animator> ().Play ("ShopRight");
		StartCoroutine (openPauseShop ());
	}
	public void CloseShop()
	{
		audioU.PlayOneShot (click, 1f);
		shopPanel.GetComponent<Animator> ().Play ("ShopRight");
	}

	IEnumerator SpawnEnemies()
	{
		yield return new WaitForSeconds(1f);
		EnemySpawner.instance.PickEnemyType ();
	}
	IEnumerator HowToDisable()
	{
		yield return new WaitForSeconds(1f);
		howToPanel.SetActive (false);
	}
	IEnumerator PauseTime()
	{
		yield return new WaitForSeconds(0.5f);
		Time.timeScale = 0;
	}
	IEnumerator PauseDisable()
	{
		yield return new WaitForSeconds(0.5f);
		pausePanel.SetActive (false);
	}
	IEnumerator openPauseShop()
	{
		yield return new WaitForSeconds(0.5f);
		Time.timeScale = 0;
	}

	public void LifeAway()
	{
		PlayerPrefs.SetInt ("lives", PlayerPrefs.GetInt ("lives") - 1);
	}
	public void waveCountIncrease()
	{
		waveCount += 10;;
	}

}
