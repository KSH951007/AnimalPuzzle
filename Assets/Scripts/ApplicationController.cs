using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ApplicationController : MonoBehaviour
{

    Queue<Func<IEnumerator>> initTaskQueue = new Queue<Func<IEnumerator>>();


    [SerializeField] private GameObject playerPrefab;

    public bool isLastVersion { get; private set; }


    [SerializeField] private StartUI startUI;
    private void Awake()
    {
        isLastVersion = false;
    }


    private void Start()
    {

        // �α��� ���� Ȯ��
        AddInitTask(CheckAppVersion);
        AuthManager auth = AuthManager.Instance;


        if (auth.isLogin == false)
        {
            if (auth.hasToken == true)
            {
                AddInitTask(auth.TokenLogin);
            }
        }

        StartCoroutine(ExecuteTasks());
    }
    public void AddInitTask(Func<IEnumerator> task)
    {
        initTaskQueue.Enqueue(task);

    }
    public IEnumerator ExecuteTasks()
    {
        int maxCount = initTaskQueue.Count;
        int currentCount = 0;
        startUI.Init(maxCount);
        while (initTaskQueue.Count > 0)
        {

            Func<IEnumerator> work = initTaskQueue.Dequeue();
            currentCount++;
            startUI.OnProgress(currentCount);


            yield return StartCoroutine(work?.Invoke());
        }
        AuthManager auth = AuthManager.Instance;

        if (auth.isLogin == false)
        {
            Debug.Log("�α������� ����");
            startUI.OpenLoginButton();
        }
        else
        {
            //����ȯ ����!!
            //����ȯ ���� load��save�� ���� ����ϰ� 
            SceneLoader.Instance.AddLoadJobQueue(DataManager.Instance.GetWebRequestData<UserAssetResponse>(ResponseDataType.Asset, $"Asset/{AuthManager.Instance.UID}"));
            SceneLoader.Instance.AddLoadJobQueue(DataManager.Instance.GetWebRequestData<StageResponse>(ResponseDataType.Stage, $"Stage/{AuthManager.Instance.UID}"));
            SceneLoader.Instance.AddLoadJobQueue(CreatePlayer());
            StartCoroutine(SceneLoader.Instance.NextSceneLoadAsync("LobbyScene"));
        }
    }

    private IEnumerator CreatePlayer()
    {

        var task = InstantiateAsync(playerPrefab);

        yield return new WaitUntil(() => task.isDone);

    }
    private IEnumerator CheckAppVersion()
    {
        AsyncOperationHandle<List<string>> handle = Addressables.CheckForCatalogUpdates();
        yield return handle;

        if (handle.Result.Count > 0)
        {
            var updateHandle = Addressables.UpdateCatalogs(handle.Result);
            yield return updateHandle;


        }
        else
        {
            isLastVersion = true;
        }
    }
}
