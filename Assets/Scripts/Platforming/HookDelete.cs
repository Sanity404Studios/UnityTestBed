using UnityEngine;
using System.Collections;

public class HookDelete : MonoBehaviour {

    float timeAlive = 0.0f;
	
    void Start() {
        Destroy(gameObject, 5.0f);
    }
}
