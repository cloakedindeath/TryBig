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
    public GameObject accountPanel;
    public GameObject bigCLogo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Button actions
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
        accountPanel.SetActive(false);
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

}
