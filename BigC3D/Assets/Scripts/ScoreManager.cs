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
		lives = 3;
		score = 0;
		PlayerPrefs.SetInt ("Score", 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(lives <= 0)
		{
			//Time.timeScale = 0;
			//GameObject.Find("EnemyTest").GetComponent<EnemyController>().StopEnemyMovement();
			GameObject.Find ("EnemySpawner").GetComponent<EnemySpawner> ().StopSpawning ();
			GameObject.Find ("Player").GetComponent<TouchTest> ().enabled = false;
			shootButton.GetComponent<Button> ().interactable = false;
			PlayerPrefs.SetInt ("Score", score);
			UIManager.instance.GameOver ();
			lives = 0;
		}
	}

	public void EnemyKill()
	{
		score = score + 20;
	}
	public void LoseLife()
	{
		lives = lives - 1;
	}
}
