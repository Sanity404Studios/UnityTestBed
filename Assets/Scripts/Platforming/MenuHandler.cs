﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    public Canvas quitCanvas;
    public Canvas SettingsCanvas;
    public Canvas LevelSelectCanvas;
    public Button playButton;
    public Button exitButton;
    public Button settingsButton;
    public Button levelSelectButton;
    
    private int currLevel;
    private MenuHandler menHand;
    //bool inMenu;

    void Awake()
    {
        currLevel = Application.loadedLevel;

        menHand = gameObject.GetComponent<MenuHandler>();
        quitCanvas = quitCanvas.GetComponent<Canvas>();
        playButton = playButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
    }

	// Use this for initialization
	void Start () {
        quitCanvas.enabled = false;
        SettingsCanvas.enabled = false;
        LevelSelectCanvas.enabled = false;
        menHand.CheckLevel(currLevel);
	}
    void CheckLevel(int loadedLevel)
    {
        if(0 == loadedLevel)
        {
            //inMenu = true;
        }
    }

    public void ExitPress()
    {
        quitCanvas.enabled = true;
        playButton.enabled = false;
        exitButton.enabled = false;
        settingsButton.enabled = false;
        LevelSelectCanvas.enabled = false;
    }

    public void BackToMainMenu()
    {
        quitCanvas.enabled = false;
        SettingsCanvas.enabled = false;
        LevelSelectCanvas.enabled = false;
        playButton.enabled = true;
        exitButton.enabled = true;
        settingsButton.enabled = true;
        LevelSelectCanvas.enabled = false;
    }
    public void SettingsMenu()
    {
        settingsButton.enabled = false;
        SettingsCanvas.enabled = true;
        playButton.enabled = false;
        exitButton.enabled = false;
        quitCanvas.enabled = false;
        LevelSelectCanvas.enabled = false;

        
    }
    public void LevelSelectMenu()
    {
        LevelSelectCanvas.enabled = true;
        settingsButton.enabled = false;
        playButton.enabled = false;
        exitButton.enabled = false;
        quitCanvas.enabled = false;
    }
    public void StartLevel()
    {
        Application.LoadLevel(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}