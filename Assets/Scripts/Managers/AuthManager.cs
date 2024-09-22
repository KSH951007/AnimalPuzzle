
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Linq;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.PackageManager.Requests;


[Serializable]
public class TokenRequest
{
    public string Token { get; set; }
}
public class AuthManager : Managers<AuthManager>
{

    [SerializeField] private readonly string apiUrl = "https://localhost:7004/api/Auth/";
    private string path;
    private string jwtToken;


    private void Awake()
    {
        if (!base.Init())
            return;

        // StartCoroutine(VaildateTokenRequest());
        path = Application.persistentDataPath + "/UserToken.txt";
        Debug.Log(path);
        if (hasLoginToken())
        {
            jwtToken = JsonConvert.DeserializeObject<TokenRequest>(File.ReadAllText(path)).Token;

        }
        else
        {

        }
    }
    public bool hasLoginToken()
    {
        if (File.Exists(path))
            return true;

        return false;
    }

    public IEnumerator VaildateTokenRequest()
    {
        // UnityWebRequest�� ����Ͽ� GET ��û ����
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        // Authorization ����� JWT ��ū �߰�
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        // ������ ��û ���� �� ���� ���
        yield return request.SendWebRequest();



        // ���� ó��
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
    public IEnumerator SignupToken()
    {
        UnityWebRequest requestBody = new UnityWebRequest(apiUrl + "signup", "POST")
        {
            downloadHandler = new DownloadHandlerBuffer()
        };

        yield return requestBody.SendWebRequest();


        if (requestBody.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(requestBody.downloadHandler.text);
            File.WriteAllText(path, requestBody.downloadHandler.text);
        }
        else
        {
            Debug.LogError(requestBody.error);
        }
    }
    public IEnumerator TokenLogin(Action<string> message, Action<UnityWebRequest> onRequest = null)
    {

        var tokenData = new
        {
            token = jwtToken
        };

        message?.Invoke("�α��� ������ Ȯ���ϰ��ֽ��ϴ�.");

        Debug.Log(tokenData.token);

        string json = JsonConvert.SerializeObject(tokenData);

        UnityWebRequest requestBody = new UnityWebRequest(apiUrl + "login", "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        requestBody.SetRequestHeader("Content-Type", "application/json");

        yield return requestBody.SendWebRequest();


        onRequest?.Invoke(requestBody);

        if (requestBody.result == UnityWebRequest.Result.Success)
        {
            message?.Invoke("�α��� ����");
            Debug.Log(requestBody.downloadHandler.text);
            File.WriteAllText(path, requestBody.downloadHandler.text);
        }
        else
        {
            Debug.LogError(requestBody.error);
        }
    }

}
