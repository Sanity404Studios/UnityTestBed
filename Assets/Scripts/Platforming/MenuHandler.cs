using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

    private int currLevel;
    private TwoDPlatformingCharacterController platCharCont;
    private MenuHandler mHand;
    bool inMenu;

    void Awake()
    {
        currLevel = Application.loadedLevel;
        platCharCont = GameObject.FindGameObjectWithTag("Player").GetComponent<TwoDPlatformingCharacterController>();
        mHand = gameObject.GetComponent<MenuHandler>();
    }

	// Use this for initialization
	void Start () {
        mHand.CheckLevel(currLevel);
	}
    void CheckLevel(int loadedLevel)
    {
        if(0 == loadedLevel)
        {
            inMenu = true;
            platCharCont.IsInMenu(inMenu);
            platCharCont.EditGravity(0.0f);
        }
    }
}