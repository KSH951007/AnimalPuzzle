
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
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime;



public class AuthManager : Managers<AuthManager>
{

    private readonly string apiUrl = @"https://localhost:7004/api/{0}/{1}/";
    private string path;
    private string jwtToken;
    private string requestHeaderValue;

    public string UID { get; private set; }
    public bool isLogin { get; private set; }
    public bool hasToken { get; private set; }
    public event Action<UnityWebRequest> onLoginResult;

    private void Awake()
    {
        if (!base.Init())
            return;
        path = Application.persistentDataPath + "/UserToken.txt";
        Debug.Log(path);
        if (hasLoginToken())
        {
            jwtToken = File.ReadAllText(path);
        }

    }
    public bool hasLoginToken()
    {
        if (File.Exists(path))
        {
            hasToken = true;
        }

        return hasToken;
    }

    public IEnumerator VaildateTokenRequest()
    {
        // UnityWebRequest를 사용하여 GET 요청 생성
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        // Authorization 헤더에 JWT 토큰 추가
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        // 서버로 요청 전송 및 응답 대기
        yield return request.SendWebRequest();



        // 응답 처리
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
    public IEnumerator SignupToken(Action<UnityWebRequest> onResult)
    {
        UnityWebRequest signupRequest = new UnityWebRequest(string.Format(apiUrl, "Auth", "signup"), "POST")
        {
            downloadHandler = new DownloadHandlerBuffer()
        };

        yield return signupRequest.SendWebRequest();



        if (signupRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(signupRequest.downloadHandler.text);
            ResgisterResponse resgisterResponse = JsonConvert.DeserializeObject<ResgisterResponse>(signupRequest.downloadHandler.text);
            jwtToken = resgisterResponse.Token;
            UID = resgisterResponse.UID;
            File.WriteAllText(path, jwtToken);
            isLogin = true;
            hasToken = true;
        }
        else
        {
            Debug.LogError(signupRequest.error);
        }
        onResult?.Invoke(signupRequest);
    }
    public IEnumerator TokenLogin()
    {

        UnityWebRequest requestBody = new UnityWebRequest(string.Format(apiUrl, "Auth", "login"), "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jwtToken)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        requestBody.SetRequestHeader("Content-Type", "text/plain");
        //requestBody.SetRequestHeader("Content-Type", "application/json");
        yield return requestBody.SendWebRequest();

        if (requestBody.result == UnityWebRequest.Result.Success)
        {
            isLogin = true;
            Debug.Log(requestBody.downloadHandler.text);
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(requestBody.downloadHandler.text);

            File.WriteAllText(path, loginResponse.Token);
            UID = loginResponse.UID;
        }
        else
        {
            Debug.LogError(requestBody.error);
        }
            
        onLoginResult?.Invoke(requestBody);

    }
    //public IEnumerator Logout()
    //{

    //}
}
