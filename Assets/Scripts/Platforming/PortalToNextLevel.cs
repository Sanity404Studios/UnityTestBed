using UnityEngine;
using System.Collections;

public class PortalToNextLevel : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if ("Player" == other.collider.name)
        {
            GameManager.LoadNextLevel();
        }
    }
}