using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class MenuHandler : MonoBehaviour {

    public Canvas[] canvasArray;
    public Canvas StartCanvas;

    private MenuHandler menhand;

    void Awake()
    {
        menhand = GetComponent<MenuHandler>();
        menhand.HandleMenu(StartCanvas);
    }

    public void HandleMenu(Canvas canvas)
    {
        switch(canvas.name)
        {
                
            case "StartMenu":
                menhand.OperateMenu("StartMenu");
                break;

            case "QuitMenu":
                OperateMenu("QuitMenu");
                break;

            case "SettingsMenu":
                OperateMenu("SettingsMenu");
                break;

            case "LevelSelectMenu":
                OperateMenu("LevelSelectMenu");
                break;

            default: Debug.LogError("No Such Canvas exists! Are you sure you spelled it right, converting it to a string, or are useing the .name property?");
                break;
        }
    }

    private void OperateMenu(string menuToActivate)
    {
        for(int i = 0; i < canvasArray.Length; i++)
        {
            if(canvasArray[i].name == menuToActivate)
            {
                canvasArray[i].enabled = true;
            }
            else
            {
                canvasArray[i].enabled = false;
            }
        }
    }

    public void HandleButton(Button button)
    {
        switch(button.name)
        {
            case "Play":
                GameManager.LoadNextLevel();
                break;

            case "Settings":
                OperateMenu("SettingsMenu");
                break;

            case "Exit":
                OperateMenu("QuitMenu");

                break;

            case "LevelSelect":
                OperateMenu("LevelSelectMenu");
                break;

            case "SettingsBackButton":
                OperateMenu("StartMenu");
                break;

            case "LevelSelectBackButton":
                OperateMenu("StartMenu");
                break;

            case "QuitYes":
                Application.Quit();
                break;

            case "QuitNo":
                OperateMenu("StartMenu");
                break;

             default: Debug.LogError("No Such Button exists! Are you sure you spelled it right, converting it to a string, or are useing the .name property?");
                break;
        }
    }
}