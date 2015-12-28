using UnityEngine;
using System.Collections;

public class MouseHoverToMoveSprite : MonoBehaviour {

    private GameObject playerSprite;

	// Use this for initialization
	void Start () {
        playerSprite = GameObject.Find("PlayerSprite");
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    //Have some other method that calls this method. This method will take two vector 2s, a start and an end point (the end point will be the transform of the empty game object used as the spawn point for the sprite), and a time to lerp between the two points. This method will then lerp over said period.
    public void UpdateCharacterPos()
    {
        if("Play" == gameObject.name)
        {
            playerSprite.transform.position = GameObject.Find("PlaySpawn").transform.position;
        }
        if ("Settings" == gameObject.name)
        {
            playerSprite.transform.position = GameObject.Find("SettingsSpawn").transform.position;
        }
        if ("Level Select" == gameObject.name)
        {
            playerSprite.transform.position = GameObject.Find("LevelSelectSpawn").transform.position; 
        }
        if ("Exit" == gameObject.name)
        {
            playerSprite.transform.position = GameObject.Find("ExitSpawnPos").transform.position;
        }
    }
}