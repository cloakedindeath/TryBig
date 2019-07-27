using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdLivesReset : MonoBehaviour
{
    public Text txt;
    public GameObject ad;

    public static AdLivesReset instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (ScoreManager.instance.lives <= 0)
        {
            txt.GetComponent<GameObject>().SetActive(true);
            ad.GetComponent<GameObject>().SetActive(true);
        }
        else
        {
            txt.GetComponent<GameObject>().SetActive(false);
            ad.GetComponent<GameObject>().SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
      if (ScoreManager.instance.lives > 0)
        {
            txt.GetComponent<GameObject>().SetActive(false);
            ad.GetComponent<GameObject>().SetActive(false);
        }
    }

    public void RestoreLives()
    {
        UnityAdManager.instance.rewardAd();
        PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") + 3);
        ad.SetActive(false);
    }
    public void Deathcheck()
    {
        if (GameManager.instance.overallLives == 0 /*&& RewardButton.instance.freeLife == false*/)
        {

           
            //txt.GetComponent<GameObject>().SetActive(true);
            ad.GetComponent<GameObject>().SetActive(true);
            if (GameManager.instance.overallLives < 0)
            {
                GameManager.instance.overallLives = 0;
            }
        }


    }
}
