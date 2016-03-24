using UnityEngine;
using System.Collections;

public class DeathLine : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("omg");
        if ("Player" == other.gameObject.name)
        {
            GameManager.OnPlayerDeath();
        }
    }
}