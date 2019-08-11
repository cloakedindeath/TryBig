using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    public GameObject email;
    public GameObject pNum;
    public GameObject loginSplash;

    private string mail;
    private string num;

    private void Awake()
    {
       if(PlayerPrefs.HasKey("tempMail") && PlayerPrefs.HasKey("tempNum"))
        {
            loginSplash.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InputField mailInput = email.GetComponent<InputField>();
        mailInput.onEndEdit.AddListener(SubmitMail);
        InputField numInput = pNum.GetComponent<InputField>();
        numInput.onEndEdit.AddListener(SubmitNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubmitInfo()
    {
        PlayerPrefs.SetString("tempMail", mail);
        PlayerPrefs.SetString("tempNum", num);
        loginSplash.SetActive(false);
    }

    private void SubmitMail(string arg0)
    {
        mail = arg0;
    }

    private void SubmitNum(string arg0)
    {
        num = arg0;
    }
}
