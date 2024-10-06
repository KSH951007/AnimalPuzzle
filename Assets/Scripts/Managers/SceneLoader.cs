using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Managers<SceneLoader>
{
    private readonly string loadingSceneName = "LoadingScene";

    private Queue<IEnumerator> loadinJobQueue = new Queue<IEnumerator>();
    private int currentJobCount;
    private int maxJobCount;

    public string nextSceneName { get; private set; }
    public Scene prevScene { get; private set; }
    public event Action<int, int> onPrgressLoad;
    private void Awake()
    {
        if (!base.Init())
            return;


        currentJobCount = 0;
        maxJobCount = 0;
    }

    public void AddLoadJobQueue(IEnumerator load)
    {
        loadinJobQueue.Enqueue(load);
        maxJobCount++;
    }

    public IEnumerator ExcuteLoading()
    {
        onPrgressLoad?.Invoke(currentJobCount, maxJobCount);
        while (loadinJobQueue.Count > 0)
        {
            var load = loadinJobQueue.Dequeue();

            yield return StartCoroutine(load);
            currentJobCount++;
            onPrgressLoad?.Invoke(currentJobCount, maxJobCount);

            yield return new WaitForSeconds(2f);
        }

        maxJobCount = 0;
        currentJobCount = 0;

        yield return StartCoroutine(NextSceneLoadAsync(nextSceneName));

    }
    public IEnumerator NextSceneLoadAsync(string nextSceneName)
    {

        Scene prevScene = SceneManager.GetActiveScene();

        this.nextSceneName = nextSceneName;

        if (maxJobCount > 0)
        {
            SceneManager.LoadScene(loadingSceneName);
            yield break;
        }



        AsyncOperation nextSceneOper = SceneManager.LoadSceneAsync(this.nextSceneName, LoadSceneMode.Additive);
        nextSceneOper.allowSceneActivation = false;
        while (nextSceneOper.progress < 0.9f)
        {
            yield return null;
        }

        nextSceneOper.allowSceneActivation = true;
        while (!nextSceneOper.isDone)
        {
            yield return null;
        }


        if (SceneManager.GetSceneByName(prevScene.name).isLoaded)
        {
            AsyncOperation unloadSceneOper = SceneManager.UnloadSceneAsync(prevScene);

            Debug.Log(unloadSceneOper.isDone);
            while (!unloadSceneOper.isDone)
            {
                yield return null;
            }
        }

        if (SceneManager.GetSceneByName(loadingSceneName).isLoaded)
        {
            AsyncOperation unloadSceneOper = SceneManager.UnloadSceneAsync(loadingSceneName);

            Debug.Log(unloadSceneOper.isDone);
            while (!unloadSceneOper.isDone)
            {
                yield return null;
            }

        }






    }
}
