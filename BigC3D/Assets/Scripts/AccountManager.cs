using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

public class AccountManager : MonoBehaviour
{
    public GameObject email;
    public GameObject pNum;
    public GameObject loginSplash;
    public Dropdown schDrop;

    private string mail;
    private string num;
    private string school;

    private void Awake()
    {
      /* if(PlayerPrefs.HasKey("tempMail") && PlayerPrefs.HasKey("tempNum") && PlayerPrefs.HasKey("tempSchool"))
        {
            loginSplash.SetActive(false);
        }*/
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
        school = schDrop.captionText.text;
        //Debug.Log(school);
    }

    public void SubmitInfo()
    {
        PlayerPrefs.SetString("tempMail", mail);
        PlayerPrefs.SetString("tempNum", num);
        PlayerPrefs.SetString("tempSchool", school);
        loginSplash.SetActive(false);
        SaveCustomer();
    }

    private void SubmitMail(string arg0)
    {
        mail = arg0;
    }

    private void SubmitNum(string arg0)
    {
        num = arg0;
    }

    private void SaveCustomer()
    {
        var httpClient = new HttpClient { BaseAddress = new url("https://dataintegrationspro.com") };
        httpClient.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var request = new HttpRequestMessage(HttpMethod.Post, "/v1/bigc/customers")
        {
            Content = new StringContent("{\"emailAddress\":\"" + mail + "\",\"phoneNumber\":\"" + num + "\"}",
                Encoding.UTF8, "application/json")
        };

        httpClient.SendAsync(request).Wait();
    }

}
