using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HookThrow : MonoBehaviour {

    public LayerMask allowedObjects;
    public GameObject hookSprite;
    public Text InAutoState;
    public Text maxDistanceOnlyState;

    private LineRenderer lineR;
    private DistanceJoint2D joint;
    private Vector3 targetPos;
    private RaycastHit2D hitInfo;
    //private Rigidbody2D hooksRB;
    private float hookRange = 20.0f;
    private float reelStep = 8f;
    private float stopDistance = 1.0f;
    private float currJointDistance;
    private bool isGrappling = false;
    private bool maxDistOnly = true;
    private bool isInAutoMode = false;
    private Vector2 currPlayerPos;
    private HookThrow hThrow;

    private string autoModeText = "Automatic Mode: ";
    private string lengthStateText = "Variable Distance Mode: ";

    //Use for grabbing script and component references
    void Awake()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        lineR = GetComponent<LineRenderer>();
        hThrow = GetComponent<HookThrow>();
        
    }

    void Start()
    {
        InAutoState.text = autoModeText + isInAutoMode.ToString();
        maxDistanceOnlyState.text = lengthStateText + joint.maxDistanceOnly.ToString();

    }
	
	// Update is called once per frame
	void Update () {

        currPlayerPos = gameObject.transform.position;

        // if the player is currently grappling and the hook is in automatic mode, continuslly invoke RaiseHook().
        if(true== isGrappling && isInAutoMode)
        {
            Invoke("RaiseHook", Time.deltaTime);
        }

        //Keeps currJointDistance up to date.
        if(true == isGrappling) 
        {
            currJointDistance = joint.distance;
        }
        
        //if the player is left clicking and the player is not currently grappling:
	    if(Input.GetMouseButtonDown(0) && false == isGrappling)
        {

            #region math for calculating relitive end points for raycasts
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            Vector2 relativeEndPoint;
            relativeEndPoint.x = targetPos.x - currPlayerPos.x;
            relativeEndPoint.y = targetPos.y - currPlayerPos.y;
            #endregion

            //actual raycast
            hitInfo = Physics2D.Raycast(currPlayerPos, relativeEndPoint, hookRange, allowedObjects);

            //if we hit something 
            if(null != hitInfo.collider)
            {
                //enabkle the joint
                joint.enabled = true;
                //enabkle the line renderer
                lineR.enabled = true;
                // set bool to true
                isGrappling = true;

                //calls method to attach player to the part of the level that was hit
                hThrow.AttachPlayer();

                //sets up line renderer
                lineR.SetPosition(0, currPlayerPos);
                lineR.SetPosition(1, hitInfo.point);
            }
        }
        #region Keybinds
        // if bool is true and the Key E is pressed, get rid of everything with the grapple so the player can fall / continue on
        if (true == isGrappling && Input.GetKeyDown(KeyCode.E))
        {
            joint.enabled = false;
            isGrappling = false;
            lineR.enabled = false;
        }
        //if(true == isGrappling && Input.GetKeyDown(KeyCode.R))
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(hThrow.GetMaxDistanceOnly() == true) 
            {
                hThrow.SetMaxDistanceOnly(false);
            }
            else
            {
                hThrow.SetMaxDistanceOnly(true);
            }
        }

        //Enables and disables automatic retraction of the grappling hook
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(false == isInAutoMode)
            {
                isInAutoMode = true;
                InAutoState.text = autoModeText + isInAutoMode.ToString();
            } else
            {
                isInAutoMode = false;
                InAutoState.text = autoModeText + isInAutoMode.ToString();
            }
            
        }
        #endregion

        if (true == isGrappling)
        {
            lineR.SetPosition(0, currPlayerPos);
            lineR.SetPosition(1, hitInfo.point);
        }

        if(hookRange < joint.distance)
        {
            joint.distance = hookRange;
        }
	}

    void SetMaxDistanceOnly(bool value)
    {
        joint.maxDistanceOnly = value;
        maxDistanceOnlyState.text = lengthStateText + joint.maxDistanceOnly.ToString();
    }

    
    bool GetMaxDistanceOnly()
    {
        return joint.maxDistanceOnly;
    }

    public void RaiseHook()
    {
        if(true == isGrappling && stopDistance < joint.distance)
        {
            Debug.Log("Raising player");
            joint.distance -= reelStep * Time.deltaTime;
        }
        else
        {
            Debug.Log("Player is not grappling(???) or player is too close to object");
        }
    }

    public void LowerHook()
    {
        if (true == isGrappling)
        {
            joint.distance += reelStep * Time.deltaTime;
        }
        else
        {
            Debug.Log("Player is not grappling(???) or player is too close to object");
        }
    }

    public bool GetIsGrappling()
    {
        return isGrappling;
    }

    void AttachPlayer()
    {
        joint.connectedAnchor = hitInfo.point;
        joint.distance = Vector2.Distance(currPlayerPos, hitInfo.point);
    }
}