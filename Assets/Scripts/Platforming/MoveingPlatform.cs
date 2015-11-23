using UnityEngine;
using System.Collections;

public class MoveingPlatform : MonoBehaviour {

    private GameObject leftBounds;
    private GameObject rightBounds;

    private float speed = .3f;

    private bool movingRight = true;

	// Use this for initialization
	void Start () 
    {
        leftBounds = GameObject.Find("leftBounds");
        rightBounds = GameObject.Find("rightBounds");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (movingRight)
        {
            transform.Translate(rightBounds.transform.position * speed * Time.deltaTime);
        }
        if (!movingRight)
        {
            transform.Translate(leftBounds.transform.position * speed * Time.deltaTime);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == leftBounds)
        {
            movingRight = false;
        }
        if (other.gameObject == rightBounds)
        {
            movingRight = true;
        }
    }
}