using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

            default: Debug.LogError("No Such canvas exists! Are you sure you spelled it right or are converting it to a string?");
                break;
        }
    }

    private void OperateMenu(string menuToActivate)
    {
        Debug.Log("halp");
        for(int i = 0; i < canvasArray.Length; i++)
        {
            if(canvasArray[i].name == menuToActivate)
            {
                canvasArray[i].enabled = true;
                Debug.Log("Activated: " + canvasArray[i].name);
            }
            else
            {
                canvasArray[i].enabled = false;
                Debug.Log("Deactivated: " + canvasArray[i].name);
            }
        }
    }

    private void HandleButton(Button button)
    {
        switch(button.name)
        {
            case "Play":
                break;

            case "Settings":
                break;

            case "Exit":
                break;

            case "LevelSellect":
                break;

            case "SettingsBackButton":
                break;
        }
    }
}