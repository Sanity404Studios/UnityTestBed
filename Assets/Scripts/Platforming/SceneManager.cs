using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    int loadedLevel;

    void Start()
    {
        loadedLevel = Application.loadedLevel;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ("Player" == other.gameObject.tag)
        {
            loadedLevel++;
            Application.LoadLevel(loadedLevel);
        }
    }
}