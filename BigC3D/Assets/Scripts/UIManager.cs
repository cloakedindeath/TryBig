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
	public GameObject quitButton;
	public GameObject player;

	public Rigidbody projectile;
	public float speed = 20;

	public Text scoreText;
	public Text livesText;

	public GameObject gameOverPanel;
	public Text gameOverScore;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		gameOver = false;
	}
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		scoreText.text = "Score: " + ScoreManager.instance.score.ToString();
		gameOverScore.text = "Score: " + ScoreManager.instance.score.ToString();
		livesText.text = "Lives: " + ScoreManager.instance.lives.ToString();
	}

	/*public void StartGame()
	{
		titlePanel.GetComponent<Animator> ().Play ("TitlePanelUp");
		quitButton.SetActive (true);
		gameOver = false;
	}*/

	public void goToMenu()
	{
		//titlePanel.GetComponent<Animator> ().Play ("TitlePanelDown");
		//quitButton.SetActive (false);
		//gameOver = true;
		SceneManager.LoadScene("Menu");
	}

	public void Shoot()
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
	}

	public void GameOver()
	{
		gameOverPanel.SetActive (true);
	}

	public void RestartGame()
	{
		SceneManager.LoadScene ("Main");
	}
}
