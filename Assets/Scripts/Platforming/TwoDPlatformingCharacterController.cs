using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwoDPlatformingCharacterController : MonoBehaviour
{
    public float localCharacterSpeed = 10.0f;
    public string axisName = "Horizontal";
    public Animator anim;
    public GameObject groundCheck;

    private float jumpPower = 375.0f;
    private float minJumpDelay = .65f;
    private float jumpTime = 0.0f;
    private Rigidbody2D rb2d;
    private Collision2D coll;
    private bool onGround = true;
    private bool jumping = false;
    private bool falling = false;
    private Transform currPlatform = null;
    private Vector3 newScale;
    private Vector3 lastPlatformPosition = Vector3.zero;
    private Vector3 currPlatformDelta = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
        //Gets animator component
        anim.gameObject.GetComponent<Animator>();
        //Gets 2d rigidbody
        rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
   {
        #region Switch Character Direction
        //Sets the float "speed" in the animator component for certain animations
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis(axisName))));

        //If there is positive movement othe horizontal axis, move the character in that direction and change scale so that the sprite is facing that way too.
        if(Input.GetAxis(axisName) < 0) {
            newScale = transform.localScale;
            newScale.x = -1.0f;
            transform.localScale = newScale;
        }//same as above, but the other direction
        else if (Input.GetAxis(axisName) > 0)
        {
            newScale = transform.localScale;
            newScale.x = 1.0f;
            transform.localScale = newScale;
        }
        #endregion Switch Character Direction

        #region Move Character
        transform.position += transform.right*Input.GetAxis(axisName) * localCharacterSpeed * Time.deltaTime;
        #endregion Move Character

        #region Jumping
        if (Input.GetKeyDown(KeyCode.LeftControl) && onGround == true)
        {
            onGround = false;
            jumping = true;
            anim.SetBool("In Air From Jump", true);
            rb2d.AddForce(transform.up * jumpPower);
            jumpTime = minJumpDelay;
        }

        if (false == onGround && false == jumping)
        {
            falling = true;
            anim.SetBool("Falling", true);
        }

        if (true == onGround && true == falling)
        {
            falling = false;
        }
    }
        #endregion Jumping


    void FixedUpdate()
    {
        #region Platform Detection
        jumpTime -= Time.deltaTime;
        //checks if PC is on the ground, for disallowing doublejump
        onGround = Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Ground"));
       

        //Checks for the PC being on the ground, and the minimum jump delay being passed. If true, onGround becomes true, stops jump animation, sets jumping to falls
        if (onGround && jumpTime < 0) //&& jumping == true
        {
            onGround = true;
            jumping = false;
            falling = false;
            anim.SetBool("In Air From Jump", false);
            anim.SetBool("Falling", false);
        }


        #endregion Platform Detection

        #region Stick To Moving Platform

        List<Transform> platforms = new List<Transform>();

        //Gets the transform of the object hit, for riding platforms
        RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Ground"));
        platforms.Add(hit.transform);

        if(currPlatform != hit.transform)
        {
            currPlatform = hit.transform;
            currPlatformDelta = Vector3.zero;
            if (null != currPlatform)
            {
                lastPlatformPosition = currPlatform.position;
            }
        }
        if (null != currPlatform)
        {
            currPlatformDelta = currPlatform.position - lastPlatformPosition;

            transform.position += currPlatformDelta;

            lastPlatformPosition = currPlatform.position;
        }
        #endregion Stick To Moving Platform
    }
}