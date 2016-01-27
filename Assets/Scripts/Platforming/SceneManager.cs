using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    int loadedLevel;

    void Start()
    {
        loadedLevel = Application.loadedLevel;
        Debug.LogWarning("Loaded Level: " + Application.loadedLevel + " variable for loaded level: " + loadedLevel);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ("Player" == other.gameObject.tag)
        {
            Debug.LogWarning("If has been entered, loaded level value " + loadedLevel);
            loadedLevel++;
            Debug.LogWarning("Value after addition" + loadedLevel);
            Application.LoadLevel(loadedLevel);
        }
    }
}