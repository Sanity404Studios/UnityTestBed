using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static int deathCounter = 0;
    private static int currLevel_;
    private static Vector3 playerStartingPosition;
    private static GameObject playerObject;
    private static bool shouldDisplayWin = false;
    private static HookThrow hookThrow;

    void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerStartingPosition = playerObject.transform.position;
        hookThrow = playerObject.GetComponent<HookThrow>();
        Debug.Log(currLevel_);
        currLevel_ = SceneManager.GetActiveScene().buildIndex;
        

        if(true == shouldDisplayWin)
        {
            //code here for displaying the win text.
        }
    }

    public static void SwitchLevelDeletion(int sceneBuildIndexToLoad)
    {
        SceneManager.LoadScene(sceneBuildIndexToLoad);
    }

    public static void OnPlayerDeath()
    {
        Debug.Log("It!");
        hookThrow.EmergencyDisconectFromHook();
        playerObject.transform.position = playerStartingPosition;
        IncrementDeathCounter();
    }

    public static void LoadNextLevel()
    {
        Debug.Log("Start of method");
        if(currLevel_ >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Incrementing would put the current scene index outside of the build index!");
            shouldDisplayWin = true;
            SwitchLevelDeletion(0);
            //Code here for return to main menu with text saying you win
        }
        Debug.Log(currLevel_);
        SwitchLevelDeletion(++currLevel_);
    }

    public static int GetDeathCounter()
    {
        return deathCounter;
    }

    public static void IncrementDeathCounter()
    {
        deathCounter++;
    }
}