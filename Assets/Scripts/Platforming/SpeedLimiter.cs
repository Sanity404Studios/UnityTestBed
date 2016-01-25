using UnityEngine;
using System.Collections;

public class SpeedLimiter : MonoBehaviour {


    private Rigidbody2D rb2d;

    private float maxSpeed = 20.0f;

	void Awake () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	

	void FixedUpdate () {

        Vector2 currVel = rb2d.velocity;
        float forceToApply;
        Debug.Log(rb2d.velocity);

        if(currVel.x > 20.0f)
        {
            forceToApply = rb2d.velocity.x - maxSpeed;
            rb2d.AddForce(-currVel * forceToApply, ForceMode2D.Impulse);
            Debug.LogWarning("Reducing player velocity by " + forceToApply + "Speed after change " + currVel);
        }

        if (currVel.y > 20.0f)
        {
            forceToApply = rb2d.velocity.y - maxSpeed;
            rb2d.AddForce(-currVel * forceToApply, ForceMode2D.Impulse);
            Debug.LogWarning("Reducing player velocity by " + forceToApply + " Speed after change " + currVel);
        }
	}
}