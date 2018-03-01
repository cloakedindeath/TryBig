using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class PersistentTimer : MonoBehaviour 
{
	public static PersistentTimer instance;
	System.DateTime expiryTime;
	public Text timer;
	public int minutes = 1;
	int seconds = 60;
	float milliseconds = 0;

	public int curHealth;
	public int maxHealth = 3;

	//[Range(1,59)]
	public int defaultStartMinutes = 1;
	public bool allowTimerRestart = false;

	public int savedSeconds = 0;
	private bool resetTimer = false;

	void Start()
	{
		curHealth = maxHealth;
		if(PlayerPrefs.GetFloat("TimeDiff") >= 0 && PlayerPrefs.GetFloat("TimeOnExit") >= 0)
		{
			float diff = (PlayerPrefs.GetFloat ("TimeOnExit") - PlayerPrefs.GetFloat ("TimeDiff"));
			//PlayerPrefs.SetFloat ("TimeOnExit", diff);
			PlayerPrefs.SetFloat("TimeDiff", 0);
			//PlayerPrefs.DeleteKey ("TimeOnExit");
			seconds = (int)diff;
		}

		if ( !this.ReadTimestamp ("timer") ) {
			this.ScheduleTimer ();
		}
		/*else{
			PlayerPrefs.SetFloat ("TimeOnExit", seconds);
			//PlayerPrefs.DeleteKey ("TimeOnExit");
		}*/
	}

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		//DontDestroyOnLoad(this);
		minutes = defaultStartMinutes;

		if(PlayerPrefs.HasKey("TimeOnExit"))
		{
			milliseconds = PlayerPrefs.GetFloat ("TimeOnExit");

			minutes = (int)milliseconds / 60;
			milliseconds -= (minutes * 60);

			seconds = (int)milliseconds;
			milliseconds -= seconds;

			//PlayerPrefs.DeleteKey ("TimeOnExit");
		}
	}

	public void Update()
	{
		Debug.Log (expiryTime.ToString());
		if ( System.DateTime.Now > expiryTime ) {
			//OnTimer ();
			this.ScheduleTimer ();
		}
		Debug.Log(PlayerPrefs.GetFloat("TimeOnExit"));
		//Debug.Log(savedSeconds);
		Debug.Log (seconds);
		//PlayerPrefs.SetInt ("TimeOnExit", savedSeconds);
		//Debug.Log(milliseconds);
		if(PlayerPrefs.GetInt("lives") <= 0 )
		{
			//count down in seconds
			//PlayerPrefs.SetFloat("TimeOnExit",60);
			milliseconds += Time.deltaTime;

			if(resetTimer)
			{
				ResetTimer ();
			}

			if(milliseconds >= 1.0f)
			{
				milliseconds -= 1.0f;

				if((seconds > 0) || (minutes > 0))
				{
					seconds--;

					if(seconds < 0)
					{
						seconds = 59;
						minutes--;
					}

				}
				else
				{
					//add code to flag and stop endless loop
					//resetTimer = true;
					resetTimer = allowTimerRestart;

				}
			}
			if(PlayerPrefs.GetInt("lives") == 3)
			{
				//allowTimerRestart = true;
				ScoreManager.instance.hp = 10;
				allowTimerRestart = true;
				minutes = defaultStartMinutes;
				//
			}
			else
			{
				allowTimerRestart = false;
			}

			if(seconds != savedSeconds)
			{
				//Show Current Time
				timer.text = string.Format("Time:{0}:{1:D2}", minutes,seconds);

				savedSeconds = seconds;
			}

			if(seconds <= 0 && minutes <= 0)
			{
				PlayerPrefs.SetFloat ("TimeOnExit", 0);
				PlayerPrefs.SetInt("lives", 3);
				ScoreManager.instance.hp = 10;
				savedSeconds = 60;
				//minutes = 1;
				//seconds = 60;
			}
		}

	}

	void ResetTimer()
	{
		minutes = defaultStartMinutes;
		seconds = 0;
		savedSeconds = 0;
		milliseconds = 1.0f - Time.deltaTime;
		resetTimer = false;
		//PlayerPrefs.DeleteKey ("TimeOnExit");
		//PlayerPrefs.SetFloat ("TimeOnExit", defaultStartMinutes);
	}

	private void OnApplicationQuit()
	{
		int numSeconds = ((minutes * 60) + seconds);

		if(numSeconds > 0)
		{
			milliseconds += numSeconds;
			//PlayerPrefs.SetFloat ("TimeOnExit", milliseconds);
			PlayerPrefs.SetFloat ("TimeOnExit", seconds);
		}
	}

	void ScheduleTimer () {
		expiryTime = System.DateTime.Now.AddDays (1.0);
		this.WriteTimestamp ("timer");
	}
	private bool ReadTimestamp (string key) {
		long tmp = System.Convert.ToInt64 (PlayerPrefs.GetString(key, "0"));
		if ( tmp == 0 ) {
			return false;
		}
		expiryTime = System.DateTime.FromBinary (tmp);
		return true;
	}

	private void WriteTimestamp (string key) {
		PlayerPrefs.SetString (key, expiryTime.ToBinary().ToString());
	}
}
/*DateTime expiryTime;
void Start () {
	if ( !this.ReadTimestamp ("timer") ) {
		this.ScheduleTimer ();
	}
}
void Update () {
	if ( DateTime.Now > expiryTime ) {
		OnTimer ();
		this.ScheduleTimer ();
	}
}
void ScheduleTimer () {
	expiryTime = DateTime.Now.AddDays (1.0);
	this.WriteTimestamp ("timer");
}
private bool ReadTimestamp (string key) {
	long tmp = Convert.ToInt64 (PlayerPrefs.GetString(key, "0"));
	if ( tmp == 0 ) {
		return false;
	}
	expiryTime = DateTime.FromBinary (tmp);
	return true;
}

private void WriteTimestamp (string key) {
	PlayerPrefs.SetString (key, expiryTime.ToBinary().ToString());
}*/