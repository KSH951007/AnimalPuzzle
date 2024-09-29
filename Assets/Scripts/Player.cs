using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HeartCount { get; private set; }
    public double RemainHeartChargeTime { get; private set; }
    public int CoinCount { get; private set; }
    public int heartChageTimeLimit { get; private set; }
    private int heartMaxCount;
    public event Action<int> onChangeHeartCount;
    public event Action<int> onChangeCoinCount;


    public event Action onHeartTimer;
    private void Awake()
    {
        heartChageTimeLimit = 30 * 60;
        heartMaxCount = 5;
        DataManager dataManager = DataManager.Instance;
        Debug.Log(dataManager.FindData(ResponseDataType.Asset));
        if (dataManager.FindData(ResponseDataType.Asset))
        {
            UserAssetResponse userAsset = (UserAssetResponse)dataManager.GetData(ResponseDataType.Asset);
            TimeSpan timeSpan = DateTime.UtcNow - userAsset.LastHeartChargeDate;

            HeartCount = Mathf.Min(heartMaxCount, (int)(userAsset.HeartCount + (timeSpan.TotalSeconds / heartChageTimeLimit)));

            RemainHeartChargeTime = timeSpan.TotalSeconds % heartChageTimeLimit;
            CoinCount = userAsset.CoinCount;
            Debug.Log(HeartCount);
        }

        DontDestroyOnLoad(gameObject);
        onChangeHeartCount?.Invoke(HeartCount);
        onChangeCoinCount?.Invoke(CoinCount);
    }
    private void Start()
    {
        Debug.Log("Player Start");
    }
    // Update is called once per frame
    void Update()
    {
        //RemainHeartChargeTime += Time.deltaTime;
        //if (RemainHeartChargeTime / heartChageTimeLimit >= 1)
        //{

        //}
    }

}
