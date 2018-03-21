using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

	public static GameManager instance;

	void Awake()
	{
		//DontDestroyOnLoad(this.gameObject);

		if(instance == null)
		{
			instance = this;
		}
		else
		{
			//Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () 
	{
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void StartGame()
	{
		if(ScoreManager.instance.lives >= 1)
		{
			ScoreManager.instance.hp = 10;
			UIManager.instance.GameStart ();
			ScoreManager.instance.startLives = false;

		}

	}
}
