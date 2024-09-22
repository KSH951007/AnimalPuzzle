using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers<T> : MonoBehaviour where T : Managers<T>
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if (instance == null)
                {
                    GameObject manager = new GameObject();
                    manager.AddComponent<T>();
                    instance = manager.GetComponent<T>();
                }
                DontDestroyOnLoad(instance.gameObject);
            }


            return instance;
        }
    }
    protected virtual bool Init()
    {
        if (instance != null)
        {
            Debug.Log(instance.gameObject.name);
            Destroy(this.gameObject);
            Debug.Log("ªË¡¶");
            return false;
        }

        instance = (T)this;
        DontDestroyOnLoad(gameObject);

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
