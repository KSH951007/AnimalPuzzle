using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DataManager : Managers<DataManager>
{
    private readonly string apiUrl = "https://localhost:7004/api/";

    Dictionary<ResponseDataType, ResponseData> responseDatas = new Dictionary<ResponseDataType, ResponseData>();
    public int responsDataCount;


    private void Awake()
    {
        if (!base.Init())
            return;
        responsDataCount = 0;


    }

    public void AddData<T>(ResponseDataType type, T data) where T : ResponseData
    {
        if (FindData(type))
            return;

        responseDatas.Add(type, data);
    }

    public bool FindData(ResponseDataType type)
    {
        if (!responseDatas.ContainsKey(type))
            return false;

        return true;
    }
    public ResponseData GetData(ResponseDataType type)
    {
        if (responseDatas.TryGetValue(type, out ResponseData data))
        {
            responseDatas.Remove(type);
            responsDataCount--;
            return data;
        }

        return null;
    }


    public IEnumerator GetWebRequestData<T>(ResponseDataType type, string url, Action<UnityWebRequest> onRequest = null) where T : ResponseData
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(apiUrl);
        stringBuilder.Append(url);

        Debug.Log(stringBuilder.ToString());
        UnityWebRequest request = UnityWebRequest.Get(stringBuilder.ToString());

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
            Debug.Log(typeof(T).ToString());
            T data = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);

            responseDatas.Add(type, data);
            Debug.Log("추가된 데이터 :" + type.ToString());
            responsDataCount++;
        }
        else
        {
            Debug.LogError(request.error);

        }
        onRequest?.Invoke(request);
    }


}
