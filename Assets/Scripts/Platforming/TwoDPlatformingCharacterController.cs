using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwoDPlatformingCharacterController : MonoBehaviour
{
    public float localCharacterSpeed = 10.0f;
    public string axisName = "Horizontal";
    public Animator anim;
    public GameObject groundCheck;
    public GameObject hookSprite;

    private float jumpPower = 500.0f;
    private float minJumpDelay = .65f;
    private float jumpTime = 0.0f;
    private float hookSpeed = 2.8f;
    private float timeBeforeAnotherHook;
    private byte hookRange = 12;
    private Rigidbody2D rb2d;
    private bool onGround = true;
    private bool jumping = false;
    private bool falling = false;
    private LineRenderer lineRend;
    private Transform currPlatform = null;
    private Vector3 newScale;
    private Vector3 lastPlatformPosition = Vector3.zero;
    private Vector3 currPlatformDelta = Vector3.zero;
    private Vector2 currPlayerPos;
    private Vector2 castDirection;
    private Vector2 relativeEndPoint;
    private Vector2 adjustedPlayerPos;
    private Rigidbody2D hookSpriteRB;

    // Use this for initialization
    void Start()
    {
        //Gets animator component
        anim.gameObject.GetComponent<Animator>();
        //Gets 2d rigidbody
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        //Get Line Renderer
        lineRend = gameObject.GetComponent<LineRenderer>();
        //Sets the width of the LineRenderer
        lineRend.SetWidth(.2f, .2f);
        //Gets the sprite renderer component
        lineRend.enabled = false;
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
        transform.position += transform.right * Input.GetAxis(axisName) * localCharacterSpeed * Time.deltaTime;
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
        #endregion Jumping

        #region Get Mouse Clicks
        if (Input.GetMouseButtonDown(0))
        {
            OperateGrapplingHook();
        }
        #endregion Get Mouse Clicks
    }
        


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

    void OperateGrapplingHook()
    {
        //Current play position
        currPlayerPos = gameObject.transform.position;
        //The direction to raycast in
        castDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Adjusts the endpoint into local space, and adjusts the start of the raycast and Instantiate to be out of the collider of the player
        relativeEndPoint.x = castDirection.x - currPlayerPos.x;
        relativeEndPoint.y = castDirection.y - currPlayerPos.y;
        adjustedPlayerPos.y = currPlayerPos.y;
        adjustedPlayerPos.x = currPlayerPos.x + .75f;

        //Performs the raycast
        RaycastHit2D hit2D = Physics2D.Raycast(adjustedPlayerPos, relativeEndPoint, 10.0f);
        Debug.DrawRay(adjustedPlayerPos, relativeEndPoint, Color.red, 4.0f);

        //if we hit a collider and the tag of the gameObject that was hit is equal to CanBeHooked
        if (null != hit2D.collider && "CanBeHooked" == hit2D.transform.gameObject.tag)
        {
                //Place the sprite for the hook at the adjusted player position with the rotation of 0.0
                Instantiate(hookSprite, adjustedPlayerPos, Quaternion.identity);

                //finds the object that was just made
                GameObject foundHookObject = GameObject.Find("hookSpritePrefab(Clone)");

                //if the hook sprite gameObject has a RigidBody2D component
                if (hookSpriteRB = foundHookObject.GetComponent<Rigidbody2D>())
                {
                    //set the line renderer component starting postion to the player position
                    lineRend.SetPosition(0, currPlayerPos);
                    lineRend.SetPosition(1, foundHookObject.transform.position);
                    lineRend.enabled = true;
                    InvokeRepeating("LineRenderUpdate", .01f, Time.deltaTime);

                    hookSprite.transform.position = Vector2.Lerp(adjustedPlayerPos, hit2D.point, Time.deltaTime);

                    //hookSpriteRB.AddForce(hit2D.point - currPlayerPos * 00f);
                    hookSpriteRB.velocity = (hit2D.point - currPlayerPos).normalized * 35.0f;
                }
                else
                {
                    Debug.LogError("No Rigidybody2D Found on hookSpritePrefab(Clone). The hell happend?");
                }

        }
    }

    void LineRenderUpdate()
    {
        Debug.Log(hookSprite.transform.position);
        lineRend.SetPosition(0, currPlayerPos);
        lineRend.SetPosition(1, hookSprite.transform.position);
    }
}