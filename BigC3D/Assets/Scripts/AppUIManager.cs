using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppUIManager : MonoBehaviour
{

    #region Variables

    public GameObject signInSplash;
    public GameObject offersPanel;
    public GameObject menuPanel;
    public GameObject menuScroll;
    public GameObject cateringScroll;
    public GameObject accountPanel;
    public GameObject settingsPanel;
    public GameObject bigCLogo;
    public GameObject MusicManager;

    //Account panel
    public Text email;
    public Text pNum;
    public Text sch;

    public Text offerHeader;

    //Audio Clip
    public AudioClip click;
    AudioSource audioSource;

    //Toggle switches
    public Toggle m_Toggle;
    public Text m_Text;

    float vol;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //Checks if music should be on or not
        if(PlayerPrefs.HasKey("vol"))
        {
            vol = PlayerPrefs.GetFloat("vol");
            MusicManager.GetComponent<AudioSource>().volume = vol;
            if(vol == 0.00f)
            {
                m_Toggle.isOn = false;
            }
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetFloat("vol"));
        //Uncomment this later to update the account page with the users info.
        //email.text = PlayerPrefs.GetString("tempMail");
        //pNum.text = PlayerPrefs.GetString("tempNum");
        //sch.text = PlayerPrefs.GetString("tempSchool");

        //eventually add the playerpref of name or email ( if we decide to ask for first name)
        offerHeader.text = "Welcome, thisemail@gmail.com";

        //Toggle Music for settings
        if(m_Toggle.isOn)
        {
            m_Text.text = "On";
            vol = 0.75f;
            MusicManager.GetComponent<AudioSource>().volume = vol;
            PlayerPrefs.SetFloat("vol", vol);
        }
        if (!m_Toggle.isOn)
        {
            m_Text.text = "Off"; 
            vol = 0.00f;
            MusicManager.GetComponent<AudioSource>().volume = vol;
            PlayerPrefs.SetFloat("vol", vol);
        }

    }

    //Button actions
    #region Button actions
    public void Offers()
    {
        bigCLogo.SetActive(false);
        offersPanel.SetActive(true);
        menuPanel.SetActive(false);
        accountPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void Menu_food()
    {
        bigCLogo.SetActive(false);
        offersPanel.SetActive(false);
        menuPanel.SetActive(true);
        menuScroll.SetActive(true);
        cateringScroll.SetActive(false);
        accountPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void openMenu()
    {
        menuScroll.SetActive(true);
        cateringScroll.SetActive(false);
    }
    public void openCatering()
    {
        menuScroll.SetActive(false);
        cateringScroll.SetActive(true);
    }
    public void Account()
    {
        bigCLogo.SetActive(false);
        offersPanel.SetActive(false);
        menuPanel.SetActive(false);
        accountPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void Home()
    {
        offersPanel.SetActive(false);
        menuPanel.SetActive(false);
        accountPanel.SetActive(false);
        settingsPanel.SetActive(false);
        bigCLogo.SetActive(true);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        offersPanel.SetActive(false);
        menuPanel.SetActive(false);
        accountPanel.SetActive(false);
        bigCLogo.SetActive(false);
    }
    #endregion

    public void StartGame() // loads the waffle walkers game
    {
        audioSource.PlayOneShot(click, .6F);
        SceneManager.LoadScene("Main (Rework)");
    }

    #region URL Links
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

    public void PrivacyPolicy()
    {
        audioSource.PlayOneShot(click, .6F);
        Application.OpenURL("https://app.termly.io/document/privacy-policy/e1d78c43-5c95-44f2-bafa-31e455871ee5");
    }
    #endregion

    #region Settings Functionality
    //reserved

    #endregion
}
