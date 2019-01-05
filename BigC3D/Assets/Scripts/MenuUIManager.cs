using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
	public GameObject socialMediaPanel;
	public GameObject optionsPanel;
	public GameObject startButton;
	public AudioClip click;
	AudioSource audioSource;
	public float timer;

	// Use this for initialization
	void Start () 
	{
		Invoke ("OptionsButtonPulse", 1f);
		audioSource = GetComponent<AudioSource>();
		//audioSource.Play();
		//timer = 0f;
		//PlayerPrefs.SetFloat ("TimeDiff", timer);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (PlayerPrefs.GetFloat("TimeDiff"));
		/*if(PlayerPrefs.HasKey("TimeOnExit"))
		{
			//timer += Time.deltaTime;
			//PlayerPrefs.SetFloat ("TimeDiff", timer);
		}*/
		//Debug.Log (timer);
	}

	public void StartGame()
	{
		audioSource.PlayOneShot(click, .6F);
		PlayerPrefs.SetFloat("TimeDiff",timer);
		SceneManager.LoadScene ("Main (Rework)");
	}
		
	public void OptionsButtonPulse()
	{
		optionsPanel.GetComponent<Animator> ().Play ("MenuBtnPulse");
		//startButton.GetComponent<Animator> ().Play ("StartBtnPusle");
	}

	#region Social Media Functions

	public void SocialMediaPanelClose()
	{
		audioSource.PlayOneShot(click, .6F);
		socialMediaPanel.GetComponent<Animator> ().Play ("SocialMediaMenuDisappear");
		Invoke ("DisableSocialMediaPanel", 0.5f);
	}

	public void DisableSocialMediaPanel()
	{
		socialMediaPanel.SetActive (false);
	}

	public void SocialButtonPulse()
	{
		socialMediaPanel.GetComponent<Animator> ().Play ("SocialButtonPusle");
	}

	public void SocialMediaPopUp()
	{
		audioSource.PlayOneShot(click, .6F);
		UnityAdManager.instance.ShowAd ();
		socialMediaPanel.SetActive (true);
		Invoke ("SocialButtonPulse", .5f);
	}
		
	public void LoadInstagram()
	{
		audioSource.PlayOneShot(click, .6F);
		Application.OpenURL("https://www.instagram.com/bigcwaffles/?hl=en");
	}
	public void LoadHomeSite()
	{
		audioSource.PlayOneShot(click, .6F);
		Application.OpenURL("https://www.bigcwaffles.com");
	}
	public void LoadFacebook()
	{
		audioSource.PlayOneShot(click, .6F);
		Application.OpenURL("https://www.facebook.com/Big-C-Waffles-743714709005652/");
	}
	public void LoadTwitter()
	{
		audioSource.PlayOneShot(click, .6F);
		Application.OpenURL("https://twitter.com/bigcwaffles?lang=en");
	}

	public void OrderApparel()
	{
		audioSource.PlayOneShot(click, .6F);
		Application.OpenURL("http://www.macflyfresh.com/index.php?option=com_hikashop&ctrl=category&task=listing&cid=19&name=big-c-waffles&Itemid=278");
	}

	public void OrderFood()
	{
		audioSource.PlayOneShot(click, .6F);
		Application.OpenURL("https://www.grubhub.com/restaurant/big-c-waffles-2110-allendown-dr-durham/629246");
	}

	IEnumerator ButtonClick()
	{
		yield return new WaitForSeconds(.5f);

	}

	#endregion
}
