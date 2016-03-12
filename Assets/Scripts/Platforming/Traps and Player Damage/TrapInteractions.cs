using UnityEngine;
using System.Collections;

public class TrapInteractions : MonoBehaviour {

    void OnCollisionEnter2d(Collision2D other)
    {
        if("Trap1" == gameObject.name && "Player" == other.gameObject.name)
        {
            GameManager.OnPlayerDeath();
        }
    }
}