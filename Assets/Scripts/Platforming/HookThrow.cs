using UnityEngine;
using System.Collections;

public class HookThrow : MonoBehaviour {

    public LayerMask allowedObjects;
    public GameObject hookSprite;

    private LineRenderer lineR;
    private DistanceJoint2D joint;
    private Vector3 targetPos;
    private RaycastHit2D hitInfo;
    private Rigidbody2D hooksRB;
    private float hookRange = 11.0f;
    private float reelStep = 2f;
    private float stopDistance = 1.0f;
    private bool isGrappling = false;
    private Vector2 currPlayerPos;
    private HookThrow hThrow;

    //Use for grabbing script and component references
    void Awake()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        lineR = GetComponent<LineRenderer>();
        hThrow = GetComponent<HookThrow>();
        
    }
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () {

        currPlayerPos = gameObject.transform.position;

	    if(Input.GetMouseButtonDown(0) && false == isGrappling)
        {
            
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            Vector2 relativeEndPoint;
            relativeEndPoint.x = targetPos.x - currPlayerPos.x;
            relativeEndPoint.y = targetPos.y - currPlayerPos.y;

            hitInfo = Physics2D.Raycast(currPlayerPos, relativeEndPoint, hookRange, allowedObjects);


            if(null != hitInfo.collider)
            {
                joint.enabled = true;
                lineR.enabled = true;
                isGrappling = true;

                //Instantiate(hookSprite, transform.position, Quaternion.identity) as GameObject;
                hThrow.AttachPlayer();
                lineR.SetPosition(0, currPlayerPos);
                lineR.SetPosition(1, hitInfo.point);
            }
        }
        if(true == isGrappling && Input.GetKeyDown(KeyCode.E))
        {
            joint.enabled = false;
            isGrappling = false;
            lineR.enabled = false;
        }

        if(true == isGrappling)
        {
            lineR.SetPosition(0, currPlayerPos);
            lineR.SetPosition(1, hitInfo.point);
        }

        if(hookRange < joint.distance)
        {
            joint.distance = hookRange;
        }
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
            Debug.Log("Lowering player");
            joint.distance += reelStep * Time.deltaTime;
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