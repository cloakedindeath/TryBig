using System.Collections;
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
	public GameObject pCardPanel;
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
	//public GameObject livesTimerOB;

	public GameObject gameOverPanel;
	public Text gameOverScore;
	GameObject[] enemiesW;
	GameObject[] enemiesC;
	GameObject[] enemiesK;
	GameObject[] projectiles;
	GameObject[] pickUpA;
	GameObject[] pickUpB;
	public int touchCnt = 0;
	public float mpCnt;
	public float shieldCnt;
	public float bombCnt;
	public AudioSource audioU;
	public AudioClip deathSound;
	public AudioClip countdown;
	public AudioClip click;
	public bool death;
	public int deathCnt;
	public GameObject gun;
	public GameObject livesLostMessage;
	public GameObject livesLostTimer;
	public GameObject playButton;


	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		gameOver = true;	//starts gameOver as true at game menu
		mpCnt = 0;			// also resets the multiplier
	}

	// Use this for initialization
	void Start () 
	{
		
		//LeaderBoardManager.instance.Login();
		audioU = GetComponent<AudioSource>();						//Plays music (music not in)
		death = false;												//Set if player is dead to false
		//DontDestroyOnLoad (livesTimerOB);
		//enemyDestroyer = GameObject.Find ("EnemyDestroyer");
		//enemySpawner.GetComponent<EnemySpawner> ().enabled = false;
		startCountdownTimerText.gameObject.transform.localScale = new Vector3 (0,0,0); //Makes sure countdown timer for game is not visable
		//gameStartCountdown = -1f;
		timeCountDown = 0;											//Resets the timer for the game to 0
		startCountdown = false;										//
		startWaveCountdown = false;									//Bool to start the wave timer
		waveCount = 1;												//Sets the starting wave count back to 1
		GameObject.Find ("Player").GetComponent<TouchTest> ().enabled = false;		//Make sure TouchTest script in disabled
		shootButton.GetComponent<Button> ().interactable = false;	//Make sure the shoot buttons cannot be pressed before the wave starts
		deathCnt = 5;												//Looks like a variable made to stop something weird from happening
		//audio.volume = 0.3f;

	}
	// Update is called once per frame
	void Update () 
	{
		ScoreManager.instance.lives = GameManager.instance.overallLives;    //Get lives from playerprefs
        Debug.Log(ScoreManager.instance.lives);
        if (ScoreManager.instance.lives == 0)
        {
            livesLostTimer.SetActive(true);
        }
   
        #region GAME LOOP CODE

        /////////////////////////////////
        //Start pre Wave countdown
        if (startCountdown == true && gameOver == false)
		{
			foreach (GameObject buttons in controls)
			{
				buttons.GetComponent<Button>().interactable = false; 		//makes the movement and fire buttons unable to be pressed
			}

	
			//Tap to start round
			/*if( Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				touchCnt = touchCnt + 1;
			}*/
			if(touchCnt == 1)
			{
				audioU.PlayOneShot (countdown, .5f);
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

        #endregion

        SetHighestWave();

		/////////////////////////
		//Update text elements
		scoreText.text = "Score: " + ScoreManager.instance.score.ToString();
		//livesText.text = "Lives: " + PlayerPrefs.GetInt("lives").ToString();
		//menuLivesText.text = "Lives: " + PlayerPrefs.GetInt("lives").ToString();
		livesText.text = "Lives: " + ScoreManager.instance.lives.ToString();
		menuLivesText.text = "Lives: " + ScoreManager.instance.lives.ToString();
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
		if(ScoreManager.instance.lives <= 0 )
		{
			death = true;
		}

	}

    //Starts the next wave
	public void nextWaveStart()
	{
		touchCnt = 1;																		//Keeps Touch count at 1 to automatically start next wave 
		audioU.PlayOneShot (countdown);														//Plays the countdown audio
		startCountdownTimerText.gameObject.transform.localScale = new Vector3 (1,1,1);		//Makes the wave start timer text visable
		waveStartPanel.GetComponent<Animator> ().Play ("StartWaveCountPopUp");				//Makes the wave start panel pop up for countdown
		waveEndPanel.GetComponent<Animator> ().Play ("WaveEndAway");						//Makes the wave end panel animate away
		waveEndPanel.SetActive (false);														//Makes wave end panel dissappear
		startWaveCountdown = false;															//Keep in wave timer false
		startCountdown = true;																//Enables the start game timer
		gameStartCountdown = 3.5f;															//Resets the start game timer
		waveCount = waveCount + 1;															//Increments the wave count
		ScoreManager.instance.SetPlayerScores ();											//Sets player score for leaderboards
	}

	//Function called to start the game when play button is pressed - Called from GameManager Script
	public void GameStart ()
	{
		TouchTest.instance.model.GetComponent<Animator> ().Play ("ANIM_Player_Idle_01");	//Plays idle animation on the player character
		gun.SetActive (true);																//Makes the gun visable
		audioU.PlayOneShot (click, 1f);														//Plays click audio
		waveEndPanel.SetActive (false);														//Makes sure the wave end pop up is not visable
		deathCnt = 0;																		
		//PlayerPrefs.SetInt ("Score", 0);
		ScoreManager.instance.score = 0;													//Resets the game score to 0
		gameOver = false;																	//Game is not over
		startGamePanel.GetComponent<Animator> ().Play ("GameStartPanelDropDown");			//Makes the GameStartPanel animate off the screen
		startCountdown = true;																//Enables the start game timer
		EnemySpawner.instance.spawnTime = 3.5f;												//Set time it takes between enemies spawning
		//mpCnt = 0;																			//Set multiplier count to 0  now set when life is lost
		gameOverPanel.SetActive (false);													//Makes sure the Game Over Panel is not visable
		touchCnt = 1;																		//This has been changed to make the countdown start immediately after hitting the play button

	}

	//Destroys enemies
	public void DestroyAllEnemies()
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
		/*pickUpA = GameObject.FindGameObjectsWithTag ("Shield");

		for(int i = 0; i < pickUpA.Length; i++) 
		{
			Destroy(pickUpA[i]);
		}
		pickUpB = GameObject.FindGameObjectsWithTag ("Bomb");

		for(int i = 0; i < pickUpB.Length; i++) 
		{
			Destroy(pickUpB[i]);
		}*/
	}

	/*public void StartEnemies()
	{
		enemySpawner.GetComponent<EnemySpawner> ().Start ();
	}*/

	public void goToMenu()
	{
		PlayerPrefsX.SetBool ("Timer1", TimerTest.instance.timerActive);
		audioU.PlayOneShot (click, 1f);
		gameOver = true;
		//PlayerPrefs.SetFloat ("TimeOnExit", PersistentTimer.instance.savedSeconds);
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
        UIManager.instance.shieldCnt = 0;
        UIManager.instance.bombCnt = 0;

        foreach (GameObject buttons in controls)
		{
			buttons.GetComponent<Button>().interactable = false;
		}
		TouchTest.instance.model.GetComponent<Animator> ().Play ("ANIM_Player_Death");
		gun.SetActive (false);
		waveEndPanel.SetActive (false);
		//UnityAdManager.instance.ShowAd2 ();
		gameOver = true;
		if(!audioU.isPlaying && deathCnt == 0 /*&& gameOver == true*/)
		{
			audioU.PlayOneShot (deathSound);
			deathCnt = 5;
			death = false;
			//gameOver = false;
		}

		timeCountDown = 0;
		gameOverPanel.SetActive (true);

		DestroyAllEnemies ();
		gameOverPanel.GetComponent<Animator> ().Play ("GameOverPopUp");
		gameOverScore.text = "Score: " + ScoreManager.instance.score.ToString();

		ScoreManager.instance.SetPlayerScores ();
		LeaderBoardManager.instance.AddScoreToLeaderboard();
		//LeaderBoardManager.instance._ShowLeaderboard ();


	}

	public void RestartGame()
	{
		TouchTest.instance.model.GetComponent<Animator> ().Play ("ANIM_Player_Idle_01");
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
		if (!audioU.isPlaying) {
			audioU.PlayOneShot (click, 1f);
		}
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
		//PlayerPrefs.SetInt ("Score", 0);
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

	public void GoBackToMenuSetLives()
	{
		if (!audioU.isPlaying) 
		{
			audioU.PlayOneShot (click, 1f);
		}
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
		//PlayerPrefs.SetInt ("Score", 0);
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

	public void GrantBomb()
	{
		bombCnt = 30;
	}

	#region Button Click Events
	public void OpenHowTo()
	{
		audioU.PlayOneShot (click, .6f);
		howToPanel.SetActive (true);
	}
	public void CloseHowTo()
	{
		audioU.PlayOneShot (click, .6f);
		howToPanel.GetComponent<Animator> ().Play ("HowToPopDown");
		StartCoroutine (HowToDisable ());
	}
	public void Pause()
	{
		audioU.PlayOneShot (click, .6f);
		pausePanel.SetActive (true);
		StartCoroutine (PauseTime ());
	}
	public void PauseResume()
	{
		if (!audioU.isPlaying) {
			audioU.PlayOneShot (click, .6f);
		}
		//audioU.PlayOneShot (click, 1f);
		Time.timeScale = 1;
		pausePanel.GetComponent<Animator> ().Play ("PauseAway");
		StartCoroutine (PauseDisable ());
	}
	public void PauseQuit()
	{
		audioU.PlayOneShot (click, .6f);
		Time.timeScale = 1;
		SceneManager.LoadScene ("Main");
	}

    #region Shop buttons // currently unused

    
    public void OpenShop()
	{
		//UnityAdManager.instance.ShowAd ();
		audioU.PlayOneShot (click, .6f);
		shopPanel.GetComponent<Animator> ().Play ("ShopPop");
	}
	public void pauseOpenShop()
	{
		audioU.PlayOneShot (click, .6f);
		Time.timeScale = 1;
		shopPanelPause.GetComponent<Animator> ().Play ("ShopPop");
		StartCoroutine (openPauseShop ());
	}
	public void pauseCloseShop()
	{
		audioU.PlayOneShot (click, .6f);
		Time.timeScale = 1;
		shopPanelPause.GetComponent<Animator> ().Play ("ShopRight");
		StartCoroutine (openPauseShop ());
	}
	public void CloseShop()
	{
		audioU.PlayOneShot (click, .6f);
		shopPanel.GetComponent<Animator> ().Play ("ShopRight");
	}
    /*Commenting out-Currently on the chopping block
	public void pCardOpen()
	{
		audioU.PlayOneShot (click, .6f);
		pCardPanel.GetComponent<Animator> ().Play ("PlayerCardANIM");
	}
	public void pCardClose()
	{
		audioU.PlayOneShot (click, .6f);
		pCardPanel.GetComponent<Animator> ().Play ("PlayerCardAway");
	}
	*/
    #endregion

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
	/*IEnumerator WaitPanelDown()
	{
		yield return new WaitForSeconds(0.5f);
		ScoreManager.instance.waitPanel.SetActive (false);
		//PlayerPrefs.SetInt ("lives", 3);
	}
	IEnumerator WaitPanelUp()
	{
		yield return new WaitForSeconds(0.5f);
		ScoreManager.instance.waitPanel.SetActive (true);
	}*/

	//Removes lives if HP hits 0
	public void LifeAway()
	{
		//ScoreManager.instance.lives = ScoreManager.instance.lives - 1;

		mpCnt = 0;
		if (PlayerPrefs.GetInt ("Lives") <= 0) 
		{
			PlayerPrefs.SetInt ("Lives_Paid", PlayerPrefs.GetInt ("Lives_Paid") - 1);		//Removes paid lives if there are no stock lives left
			if(PlayerPrefs.GetInt("Lives_Paid") <= 0)
				{
					PlayerPrefs.SetInt ("Lives_Reward", PlayerPrefs.GetInt ("Lives_Reward") - 1);
				}

		}
		else 
		{
			PlayerPrefs.SetInt ("Lives", PlayerPrefs.GetInt ("Lives") - 1);					//Removes stock lives
		}
	}
	public void waveCountIncrease()
	{
		waveCount += 10;;
	}

	public void ShowLeaderboard()
	{
		audioU.PlayOneShot (click, .6f);
		LeaderBoardManager.instance._ShowLeaderboard ();
	}

	/*public void paidAddLife()
	{
		UnityAdManager.instance.ShowAd();
		//PersistentTimer.instance.savedSeconds = 60;
		//ScoreManager.instance.waitPanel.GetComponent<Animator> ().Play ("waitPanelAway");
		//PlayerPrefs.SetInt ("lives", PlayerPrefs.GetInt ("lives") + 1);
		//ScoreManager.instance.lives = ScoreManager.instance.lives+1;
	}*/

	#endregion
}
