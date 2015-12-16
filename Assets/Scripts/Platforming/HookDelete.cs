using UnityEngine;
using System.Collections;

public class HookDelete : MonoBehaviour {
	
    void Start() {
        Destroy(gameObject, 5.0f);
    }
}