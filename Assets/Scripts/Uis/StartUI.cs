using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI progressTMP;
    [SerializeField] private GameObject progressObj;
    [SerializeField] private Button loginBtn;


    private readonly string progressFormat = @"( {0} / {1} )";
    private int maxProgressCount;
    private void Start()
    {
    }

    private void OnDisable()
    {
        // ApplicationController.Instance.messageHandler.onMessage -= ShowStateMessage;

    }

    public void Init(int maxCount)
    {
        maxProgressCount = maxCount;
    }

    public void OnProgress(int currentCount)
    {
        if (progressObj.activeSelf == false)
            progressObj.SetActive(true);

        progressImage.fillAmount = (float)currentCount / maxProgressCount;
        progressTMP.text = string.Format(progressFormat, currentCount, maxProgressCount);
    }
    public void LoginResultHandler(UnityWebRequest request)
    {
        if (request.result == UnityWebRequest.Result.Success)
        {

        }
        else
        {

        }
    }
    public void SignupResultHandler(UnityWebRequest request)
    {
        if (request.result == UnityWebRequest.Result.Success)
        {

        }
        else
        {

        }
    }
    public void OpenLoginButton()
    {
        progressObj.gameObject.SetActive(false);
        loginBtn.gameObject.SetActive(true);
    }

    public void PressOnLogin()
    {
        loginBtn.interactable = false;
        StartCoroutine(AuthManager.Instance.SignupToken(OnSignupResult));

     

    }
    public void OnSignupResult(UnityWebRequest request)
    {
        if (request.result == UnityWebRequest.Result.Success)
        {

            DataManager dataManager = DataManager.Instance;
            //StartCoroutine(SceneLoader.Instance.NextSceneLoadAsync("LobbyScene"));

        }
        else
        {

        }


    }
}
