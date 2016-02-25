using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

    public void Kill()
    {
        GameObject.Destroy(gameObject);

        int levelLoaded = Application.loadedLevel;
        Application.LoadLevel(levelLoaded);
    }
}