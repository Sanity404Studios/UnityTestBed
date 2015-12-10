using UnityEngine;
using System.Collections;

public class HookThrow : MonoBehaviour {

    public LayerMask allowedObjects;

    private LineRenderer lineR;
    private DistanceJoint2D joint;
    private Vector3 targetPos;
    private RaycastHit2D hitInfo;
    private float hookRange = 10.0f;
    private bool isGrappling = false;

    //Use for grabbing script and component references
    void Awake()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        lineR = GetComponent<LineRenderer>();
    }
	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () {

        Vector2 currPlayerPos = gameObject.transform.position;

	    if(Input.GetMouseButtonDown(0) && false == isGrappling)
        {
            
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            Vector2 relativeEndPoint;
            relativeEndPoint.x = targetPos.x - currPlayerPos.x;
            relativeEndPoint.y = targetPos.y - currPlayerPos.y;

            hitInfo = Physics2D.Raycast(currPlayerPos, relativeEndPoint, hookRange, allowedObjects);
            Debug.DrawRay(currPlayerPos, relativeEndPoint, Color.red, .8f);

            if(null != hitInfo.collider)
            {
                joint.enabled = true;
                lineR.enabled = true;
                isGrappling = true;
                joint.connectedAnchor = hitInfo.point;
                joint.distance = Vector2.Distance(currPlayerPos, hitInfo.point);
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
	}
}