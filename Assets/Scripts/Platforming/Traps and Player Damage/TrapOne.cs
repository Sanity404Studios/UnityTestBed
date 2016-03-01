using UnityEngine;
using System.Collections;

public class TrapOne : MonoBehaviour {

    private KillPlayer kP;


    void OnCollisionEnter2D(Collision2D other)
    {
        if("Player" == other.gameObject.name)
        {
            kP = other.gameObject.GetComponent<KillPlayer>();
            kP.Kill();
        }
    }
}