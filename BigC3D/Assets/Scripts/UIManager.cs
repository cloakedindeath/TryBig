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
	public float gameStartCountdown;
	public GameObject enemyDestroyer;

	public Text scoreText;
	public Text livesText;
	public Text waveTimerText;
	public Text startCountdownTimerText;

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
				timeCountDown = 30.5f;
			}
		}
		if(startWaveCountdown == true && gameOver == false)
		{
			enemyDestroyer.SetActive (false);
			timeCountDown -= Time.deltaTime;
			if(timeCountDown <= 0)
			{
				DestroyAllEnemies ();
			}
		}
		//Start actual wave countdown



		//Update text elements
		scoreText.text = "Score: " + ScoreManager.instance.score.ToString();
		livesText.text = "Lives: " + ScoreManager.instance.lives.ToString();
		waveTimerText.text = "Wave Timer: " + timeCountDown.ToString("f0");
		startCountdownTimerText.text = gameStartCountdown.ToString ("f0");

	}

	public void GameStart ()
	{
		gameOver = false;
		startGamePanel.GetComponent<Animator> ().Play ("GameStartPanelDropDown");
		startCountdown = true;

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
}
