using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    int loadedLevel;
    public Text winText;
    

    void Start()
    {
        loadedLevel = Application.loadedLevel;
        winText.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ("Player" == other.gameObject.tag)
        {
            loadedLevel++;
            Application.LoadLevel(loadedLevel);
        }
        if (loadedLevel >= Application.levelCount) 
        {
            winText.enabled = true;
            winText.text = "You Win!";
        }
    }
}