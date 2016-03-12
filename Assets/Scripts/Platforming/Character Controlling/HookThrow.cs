using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HookThrow : MonoBehaviour {

    public LayerMask allowedObjects;
    public GameObject hookSprite;
    public GameObject myParticleSystem;
    public Text InAutoState;
    public Text maxDistanceOnlyState;

    private LineRenderer lineRend;
    private DistanceJoint2D joint;
    private Vector3 targetPos;
    private RaycastHit2D hitInfo;
    private float hookRange = 22.0f;
    private float reelStep = 12f;
    private float stopDistance = 1.0f;
    private bool isGrappling = false;
    private bool isInAutoMode = false;
    private Vector2 currPlayerPos;
    private HookThrow hThrow;
    private GameObject currHit;
    private GameObject LastHit;

    private string autoModeText = "Automatic Mode: ";
    private string lengthStateText = "Variable Distance Mode: ";

    //Use for grabbing script and component references
    void Awake()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        lineRend = GetComponent<LineRenderer>();
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

        //Keeps currJointDistance up to date.
        //if(true == isGrappling) 
        //{
        //}
        
        //if the player is left clicking and the player is not currently grappling:
        if (Input.GetMouseButtonDown(0))
        {

            hThrow.PreformRaycast();

        }
        #region Keybinds
        // if bool is true and the Key E is pressed, get rid of everything with the grapple so the player can fall / continue on
        if (true == isGrappling && Input.GetMouseButtonDown(1))
        {
            hThrow.DetachPlayer();
        }
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
            lineRend.SetPosition(0, currPlayerPos);
        }

        if(hookRange < joint.distance)
        {
            joint.distance = hookRange;
        }
	}

    void FixedUpdate()
    {
        // if the player is currently grappling and the hook is in automatic mode, continuslly invoke RaiseHook().
        if (true == isGrappling && isInAutoMode)
        {
            Invoke("RaiseHook", Time.deltaTime);
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
            //Debug.Log("Raising player");
            joint.distance -= reelStep * Time.deltaTime;
        }
        else
        {
            //Debug.Log("Player is not grappling(???) or player is too close to object");
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

    void PreformRaycast()
    {
        #region math for calculating relative end points for raycasts
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;

        Vector2 relativeEndPoint;
        relativeEndPoint.x = targetPos.x - currPlayerPos.x;
        relativeEndPoint.y = targetPos.y - currPlayerPos.y;
        #endregion

        hitInfo = Physics2D.Raycast(currPlayerPos, relativeEndPoint, hookRange, allowedObjects);


        if (null != hitInfo.collider)
        {
            hThrow.SetupVisualsForHook();
        }
        //if(null == hitInfo.collider)
        //{
        //    hThrow.DetachPlayer();
        //}
    }

    void AttachPlayer()
    {
        joint.connectedAnchor = hitInfo.point;
        joint.distance = Vector2.Distance(currPlayerPos, hitInfo.point);
    }

    //ONLY CALL IF YOU HAVE HIT SOMETHING WITH A RAYCAST. 
    void SetupVisualsForHook()
    {
        if(null != hitInfo.collider)
        {

            joint.enabled = true;
            lineRend.enabled = true;
            isGrappling = true;

            //calls method to attach player to the part of the level that was hit
            hThrow.AttachPlayer();

            //sets up line renderer points
            lineRend.SetPosition(0, currPlayerPos);
            lineRend.SetPosition(1, hitInfo.point);
            //Debug.Log("hitInfo point: " + hitInfo.point);

            //Makes a particle system at the point where the hook hit along the same rotation as the object that was hit
            Instantiate(myParticleSystem, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        }

        #region debuging else
        else
        {
            Debug.LogError("YA SCRUB. WHY ARE YE CALLING THIS FUNCTON IF YOU HAVEN'T HIT SOMETHING WITH A RAYCAST?");
        }
        #endregion
    }

    void DetachPlayer()
    {
        joint.enabled = false;
        isGrappling = false;
        lineRend.enabled = false;
    }

    //Only call this when the player dies!
    public void EmergencyDisconectFromHook()
    {
        hThrow.DetachPlayer();
    }
}