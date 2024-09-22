using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stateMessageTMP;
    [SerializeField] private Button loginBtn;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        AuthManager auth = AuthManager.Instance;

        if (auth.hasLoginToken())
        {
            StartCoroutine(auth.TokenLogin(ShowStateMessage, LoginResultHandler));
        }
        else
        {
            stateMessageTMP.gameObject.SetActive(false);
            loginBtn.gameObject.SetActive(true);

        }
    }

    public void ShowStateMessage(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            stateMessageTMP.text = "";
            stateMessageTMP.gameObject.SetActive(false);
            return;
        }
        else
        {
            stateMessageTMP.gameObject.SetActive(true);
            stateMessageTMP.text = message;
        }

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
    public void PressOnLogin()
    {
        StartCoroutine(AuthManager.Instance.SignupToken());
    }
}
