using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwoDPlatformingCharacterController : MonoBehaviour
{
    public float localCharacterSpeed = 10.0f;
    public string axisName = "Horizontal";
    public Animator anim;
    public GameObject groundCheck;

    private float jumpPower = 500.0f;
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
        //If the desired key is down, and the player character is on the ground, set set some bools, set the animator paramiter "In Air From Jump" to true for animations to play, add force for thhe jump, and start the jump delay to prevent double jump
        if (true == onGround && Input.GetKeyDown(KeyCode.Space))
        {
            onGround = false;
            jumping = true;
            anim.SetBool("In Air From Jump", true);
            rb2d.AddForce(transform.up * jumpPower);
            jumpTime = minJumpDelay;
        }

        //If not on the ground, and not jumping, set animator paramiter "Falling" to true for falling animation to play.
        if (false == onGround && false == jumping)
        {
            falling = true;
            anim.SetBool("Falling", true);
        }

        //If on the ground and falling is true, set falling to false.
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
        //checks if Player Character is on the ground, for disallowing doublejump
        onGround = Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Ground"));
       

        //Checks for the Player Character being on the ground, and the minimum jump delay being passed. If true, onGround becomes true, stops jump animation, sets jumping to falls
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

        //When the Player Moves to a new platform, set the new platform as the current one, and set the delta to be zero so that the velocity of the Player Character is not preserved onto the new platform
        if(currPlatform != hit.transform)
        {
            currPlatform = hit.transform;
            currPlatformDelta = Vector3.zero;
            //If currPlatform is not null, set lastPlatformPosition to be equal to currentPlatform position, this keeps lastPlatform up to date.
            if (null != currPlatform)
            {
                lastPlatformPosition = currPlatform.position;
            }
        }
        //If there is a platforum under the Player Character, get the platform's Delta, move the Player Character, 
        if (null != currPlatform)
        {
            currPlatformDelta = currPlatform.position - lastPlatformPosition;

            transform.position += currPlatformDelta;

            lastPlatformPosition = currPlatform.position;
        }
        #endregion Stick To Moving Platform
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If the trigger the Player Character is colliding with is named "DeathLine", get the index of the loaded level and restart that level
        if ("DeathLine" == other.gameObject.name)
        {
            int loadedLevel = Application.loadedLevel;
            Application.LoadLevel(loadedLevel);
        }
    }
}