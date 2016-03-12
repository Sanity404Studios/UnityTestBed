using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelChangeingManager : MonoBehaviour {

    static void SwitchLevelDeletion(int sceneBuildIndexToLoad)
    {
        SceneManager.LoadScene(sceneBuildIndexToLoad);
    }

    static void OnPlayerDeath()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if("Player" == other.collider.name)
        {
            LoadNextLevel();
        }
    }

    static void LoadNextLevel()
    {
        int currLevel = SceneManager.GetActiveScene().buildIndex;

        if(currLevel >= SceneManager.sceneCountInBuildSettings)
        {
            //Return to main menu with text saying you win
        }
        
        SwitchLevelDeletion(currLevel++);
    }
}