﻿using UnityEngine;
using System.Collections;

public class CursorSetting : MonoBehaviour {

    public Texture2D cursorTexture;
    
    static CursorSetting Instance;
    private Vector2 cursorHotspot;

    void Awake()
    {
        if(null != Instance)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }



    //// initialize mouse with a new texture with the
    //// hotspot set to the middle of the texture
    //// (don't forget to set the texture in the inspector
    //// in the editor)
    //void Start()
    //{
    //    cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
    //    Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    //}

    //// To check where your mouse is really pointing
    //// track the mouse position in you update function
    //void Update()
    //{
    //    Vector3 currentMouse = Input.mousePosition;
    //    Ray ray = Camera.main.ScreenPointToRay(currentMouse);
    //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
    //    Debug.DrawLine(ray.origin, hit.point);
    //}
}