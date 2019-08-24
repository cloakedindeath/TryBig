using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppUIManager : MonoBehaviour
{
    public GameObject signInSplash;
    public GameObject offersPanel;
    public GameObject menuPanel;
    public GameObject menuScroll;
    public GameObject cateringScroll;
    public GameObject accountPanel;
    public GameObject bigCLogo;

    //Account panel
    public Text email;
    public Text pNum;
    public Text sch;

    public Text offerHeader;

    public AudioClip click;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Uncomment this later to update the account page with the users info.
        //email.text = PlayerPrefs.GetString("tempMail");
        //pNum.text = PlayerPrefs.GetString("tempNum");
        //sch.text = PlayerPrefs.GetString("tempSchool");

        //eventually add the playerpref of name or email ( if we decide to ask for first name)
        offerHeader.text = "Welcome, {insert name/email}...";
    }

    //Button actions
    #region Button actions
    public void Offers()
    {
        bigCLogo.SetActive(false);
        offersPanel.SetActive(true);
        menuPanel.SetActive(false);
        accountPanel.SetActive(false);
    }
    public void Menu_food()
    {
        bigCLogo.SetActive(false);
        offersPanel.SetActive(false);
        menuPanel.SetActive(true);
        menuScroll.SetActive(true);
        cateringScroll.SetActive(false);
        accountPanel.SetActive(false);
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
    }
    public void Home()
    {
        offersPanel.SetActive(false);
        menuPanel.SetActive(false);
        accountPanel.SetActive(false);
        bigCLogo.SetActive(true);
    }
    #endregion

    public void StartGame()
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

}
