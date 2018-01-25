using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

	public Text scoreText;
	public Text livesText;
	public Text waveTimerText;
	public Text startCountdownTimerText;
	public Text waveOverScoreText;
	public Text waveOverText;
	public Text waveIndicatorText;

	public GameObject gameOverPanel;
	public Text gameOverScore;
	GameObject[] enemies;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		gameOver = true;

	}
	// Use this for initialization
	void Start () 
	{
		//enemyDestroyer = GameObject.Find ("EnemyDestroyer");
		//enemySpawner.GetComponent<EnemySpawner> ().enabled = false;
		gameStartCountdown = 3.5f;
		timeCountDown = 0;
		startCountdown = false;
		startWaveCountdown = false;
		waveCount = 1;
	}

	// Update is called once per frame
	void Update () 
	{
		//Start pre Wave countdown
		if(startCountdown == true && gameOver == false)
		{
			
			gameStartCountdown -= Time.deltaTime;
			if(gameStartCountdown <= 0)
			{
				startCountdown = false;
				startWaveCountdown = true;
				waveStartPanel.GetComponent<Animator> ().Play ("StartWaveCountdownRemover");
				timeCountDown = 25.5f;
				inbetweenTimer = 3.5f;
			}
		}
		//Start actual wave countdown
		else if(startWaveCountdown == true && gameOver == false)
		{
			//StartCoroutine (SpawnEnemies ());
			EnemySpawner.instance.PickEnemyType();
			timeCountDown -= Time.deltaTime;

			if(timeCountDown <= 0)
			{
				DestroyAllEnemies ();
				timeCountDown = 0;
				waveEndPanel.SetActive(true);
				inbetweenTimer -= Time.deltaTime;
			}
			if (inbetweenTimer <= 0)
			{
				waveStartPanel.GetComponent<Animator> ().Play ("StartWaveCountPopUp");
				waveEndPanel.GetComponent<Animator> ().Play ("WaveEndAway");
				waveEndPanel.SetActive (false);
				startWaveCountdown = false;
				startCountdown = true;
				gameStartCountdown = 3.5f;
				waveCount = waveCount + 1;
			}

		}
			
		//Update text elements
		scoreText.text = "Score: " + ScoreManager.instance.score.ToString();
		livesText.text = "Lives: " + ScoreManager.instance.lives.ToString();
		waveTimerText.text = "Wave " + waveCount + " Timer: " + timeCountDown.ToString("f0");
		startCountdownTimerText.text = gameStartCountdown.ToString ("f0");
		waveOverScoreText.text = "Score: " + ScoreManager.instance.score.ToString ();
		waveOverText.text = "Wave " + waveCount;
		waveIndicatorText.text = "Wave " + waveCount;

		if(waveCount == 10)
		{
			gameOver = true;
			GameOver ();
		}

	}

	public void GameStart ()
	{
		gameOver = false;
		startGamePanel.GetComponent<Animator> ().Play ("GameStartPanelDropDown");
		startCountdown = true;
		EnemySpawner.instance.spawnTime = 3.5f;

	}

	void DestroyAllEnemies()
	{
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		for(int i = 0; i < enemies.Length; i++) 
		{
			Destroy(enemies[i]);
		}

	}

	/*public void StartEnemies()
	{
		enemySpawner.GetComponent<EnemySpawner> ().Start ();
	}*/

	public void goToMenu()
	{
		gameOver = true;
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
		DestroyAllEnemies ();
		gameOverPanel.SetActive (true);
		gameOverScore.text = "Score: " + ScoreManager.instance.score.ToString();
	}

	public void RestartGame()
	{
		SceneManager.LoadScene ("Main");
	}
	public void GoBackToMenu()
	{
		SceneManager.LoadScene ("Main");
	}

	IEnumerator SpawnEnemies()
	{
		yield return new WaitForSeconds(1f);
		EnemySpawner.instance.PickEnemyType ();
	}
}
