using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
	public GameObject socialMediaPanel;
	public GameObject optionsPanel;
	public GameObject startButton;
	//public AudioClip click;
	//AudioSource audioSource;

	// Use this for initialization
	void Start () 
	{
		Invoke ("OptionsButtonPulse", 1f);
		//audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void StartGame()
	{
		//audioSource.PlayOneShot(click, 1F);
		SceneManager.LoadScene ("Main");
	}
		
	public void OptionsButtonPulse()
	{
		optionsPanel.GetComponent<Animator> ().Play ("MenuBtnPulse");
		//startButton.GetComponent<Animator> ().Play ("StartBtnPusle");
	}

	#region Social Media Functions

	public void SocialMediaPanelClose()
	{
		//audioSource.PlayOneShot(click, 1F);
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
		//audioSource.PlayOneShot(click, 1F);
		UnityAdManager.instance.ShowAd ();
		socialMediaPanel.SetActive (true);
		Invoke ("SocialButtonPulse", .5f);
	}
		
	public void LoadInstagram()
	{
		//audioSource.PlayOneShot(click, 1F);
		Application.OpenURL("https://www.instagram.com/bigcwaffles/?hl=en");
	}
	public void LoadHomeSite()
	{
		//audioSource.PlayOneShot(click, 1F);
		Application.OpenURL("https://www.bigcwaffles.com");
	}
	public void LoadFacebook()
	{
		//audioSource.PlayOneShot(click, 1F);
		Application.OpenURL("https://www.facebook.com/Big-C-Waffles-743714709005652/");
	}
	public void LoadTwitter()
	{
		//audioSource.PlayOneShot(click, 1F);
		Application.OpenURL("https://twitter.com/bigcwaffles?lang=en");
	}

	public void OrderApparel()
	{
		//audioSource.PlayOneShot(click, 1F);
		Application.OpenURL("http://www.macflyfresh.com/index.php/shop/category/19-big-c-waffles.html");
	}

	public void OrderFood()
	{
		//audioSource.PlayOneShot(click, 1F);
		Application.OpenURL("https://www.grubhub.com/restaurant/big-c-waffles-2110-allendown-dr-durham/629246");
	}

	#endregion
}
