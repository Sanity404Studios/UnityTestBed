using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwoDPlatformingCharacterController : MonoBehaviour
{
    public float localCharacterSpeed = 12.0f;
    public string axisName = "Horizontal";
    public Animator anim;
    public GameObject groundCheck;
    public GameObject hookSprite;

    private float jumpPower = 750.0f;
    private float minJumpDelay = .65f;
    private float jumpTime = 0.0f;
    private float jumpingMovementReduction = 2f;
    private float grapplingForceMultiplier = 10;
    private Rigidbody2D rb2d;
    private bool onGround = true;
    private bool jumping = false;
    private bool falling = false;
    private bool hasMoved = false;
    private bool controlsLocked = false;
    private bool currentlyGrappling;
    private Quaternion startRotation;
    private Transform currPlatform = null;
    private Vector3 newScale;
    private Vector3 lastPlatformPosition = Vector3.zero;
    private Vector3 currPlatformDelta = Vector3.zero;
    private HookThrow hookTh;

    // Use this for initialization
    void Start()
    {
        startRotation = gameObject.transform.rotation;
        //Gets animator component
        anim = gameObject.GetComponent<Animator>();
        //Gets 2d rigidbody
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        //Gets reference to hook throw script
        hookTh = gameObject.GetComponent<HookThrow>();

        if(Application.loadedLevel > 0)
        {
            InvokeRepeating("LerpRotatePlayer", Time.deltaTime, Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Switch Character Direction
        //Sets the float "speed" in the animator component for certain animations
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis(axisName))));

        //If there is positive movement othe horizontal axis, move the character in that direction and change scale so that the sprite is facing that way too.
        if (Input.GetAxis(axisName) < 0)
        {
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
        if (hasMoved && false == controlsLocked)
        {
            transform.position += transform.right * Input.GetAxisRaw(axisName) * localCharacterSpeed * Time.deltaTime;
        }

        if (true == jumping && hasMoved)
        {
            transform.position += transform.right * Input.GetAxisRaw(axisName) * localCharacterSpeed * Time.deltaTime / jumpingMovementReduction;
        }
        #endregion Move Character

        #region GrapplingMovement

        if (true == hasMoved && true == currentlyGrappling)
        {
            rb2d.AddForce(transform.right * Input.GetAxisRaw(axisName) * localCharacterSpeed * grapplingForceMultiplier  * Time.deltaTime, ForceMode2D.Force);
        }

        #endregion

        #region Jumping
        //If the desired key is down, and the player character is on the ground, set set some bools, set the animator paramiter "In Air From Jump" to true for animations to play, add force for thhe jump, and start the jump delay to prevent double jump
        if (true == onGround && Input.GetKeyDown(KeyCode.Space) && controlsLocked == false)
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
        #endregion Jumping

        #region Grappling Hook Up/down
        if(Input.GetKey(KeyCode.W))
        {
            hookTh.GetComponent<HookThrow>().RaiseHook();
        }

        if(Input.GetKey(KeyCode.S))
        {
            hookTh.GetComponent<HookThrow>().LowerHook();
        }

        #endregion Grappling Hook Up/down

        #region check Hasmoved
        if(0.0f != Input.GetAxis(axisName))
        {
            hasMoved = true;
        }
        else
        {
            hasMoved = false;
        }
        #endregion

        #region Check isGrappling
        if(true == hookTh.GetIsGrappling())
        {
            currentlyGrappling = true;
        }else 
        {
            currentlyGrappling = false;
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region Ground Detection

        //checks if Player Character is on the ground, for disallowing infanite jumping
        onGround = Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Ground"));

        #endregion Ground Detection

        #region Platform Detection
        jumpTime -= Time.deltaTime;

        //Checks for the Player Character being on the ground, and the minimum jump delay being passed. If true, onGround becomes true, stops jump animation, sets jumping to falls
        if (onGround && jumpTime < 0 && false == false)
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
        if (currPlatform != hit.transform)
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

        #region Lock Controls
        if(false == onGround && false == controlsLocked)
        {
            controlsLocked = true;
        }

        if (true == onGround)
        {
            controlsLocked = false;
        }
        #endregion
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

    void LerpRotatePlayer()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 10);
    }
}