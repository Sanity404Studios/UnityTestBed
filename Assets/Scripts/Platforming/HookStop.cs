using UnityEngine;
using System.Collections;

public class HookStop : MonoBehaviour {

    Rigidbody2D rb;

	// Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D()
    {
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}