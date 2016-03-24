using UnityEngine;
using System.Collections;

public class TrapInteractions : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision!");
        if("Trap1" == gameObject.name && "Player" == other.gameObject.name)
        {
            Debug.Log("Called!");
            GameManager.OnPlayerDeath();
        }
    }
}