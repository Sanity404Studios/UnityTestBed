using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour {

    static void ChangeLevel(int levelToChangeTo)
    {
        SceneManager.LoadScene(levelToChangeTo, LoadSceneMode.Single);
    }

    static Scene GetLevel()
    {
        return SceneManager.GetActiveScene();
    }
}