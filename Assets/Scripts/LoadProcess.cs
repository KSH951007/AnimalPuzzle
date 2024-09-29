using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadProcess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
    }

    public IEnumerator Load()
    {


        SceneLoader sceneLoader = SceneLoader.Instance;
        yield return StartCoroutine(sceneLoader.ExcuteLoading());

    }

}
