using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class ConstrainMouseToWindow : MonoBehaviour {

    //Creates new screen rect who's boundries are the width and height of the screen
    Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePos = Input.mousePosition;

        if(false == screenRect.Contains(mousePos))
        {
            return;
        }
	}
}