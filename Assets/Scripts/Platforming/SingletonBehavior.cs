using UnityEngine;
using System.Collections;

public class SingletonBehavior : MonoBehaviour {

    public static SingletonBehavior instance;

    void Awake()
    {

        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}