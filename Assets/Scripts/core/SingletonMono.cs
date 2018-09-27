using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T:SingletonMono<T>
{
    protected static T Instance = null;
    public static T _instance
    {
        get
        {
            if(Instance == null)
            {
                GameObject go = GameObject.Find("SingletonObj");
                if (go == null)
                {
                    go = new GameObject("SingletonObj");
                    DontDestroyOnLoad(go);
                }
                Instance = go.AddComponent<T>();
            }
            return Instance;
        }        
    }

    private void OnApplicationQuit()
    {
        Instance = null;
    }
}
