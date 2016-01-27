using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

    static DontDestroyOnLoad Instance;
    void Start()
    {
        if(Instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        Debug.LogWarning(Instance);
    }
}