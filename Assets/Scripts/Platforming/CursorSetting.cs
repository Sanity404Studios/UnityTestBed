using UnityEngine;
using System.Collections;

public class CursorSetting : MonoBehaviour {

    public Texture2D newCursorTexture;
    static CursorSetting Instance;

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
        Cursor.SetCursor(newCursorTexture, Vector2.zero, CursorMode.Auto);
    }
}