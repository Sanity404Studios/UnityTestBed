using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static int deathCounter = 0;
    private static int currLevel_;
    private static Transform playerStartingPosition;
    private static GameObject playerObject;
    private static bool shouldDisplayWin = false;
    private static HookThrow hookThrow;

    void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerStartingPosition = playerObject.transform;
        hookThrow = playerObject.GetComponent<HookThrow>();
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
        hookThrow.EmergencyDisconectFromHook();
        playerObject.transform.position = playerStartingPosition.position;
        IncrementDeathCounter();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if("Player" == other.collider.name)
        {
            LoadNextLevel();
        }
    }

    public static void LoadNextLevel()
    {
        if(currLevel_ >= SceneManager.sceneCountInBuildSettings)
        {
            shouldDisplayWin = true;
            SwitchLevelDeletion(0);
            //Return to main menu with text saying you win
        }
        
        SwitchLevelDeletion(currLevel_++);
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