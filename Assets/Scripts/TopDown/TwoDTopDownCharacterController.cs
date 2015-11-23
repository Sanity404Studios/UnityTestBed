using UnityEngine;
using System.Collections;

public class TwoDTopDownCharacterController : MonoBehaviour {


    public Animator anim;
    public string axisLeftRight = "Horizontal";
    public string axisUpDown = "Vertical";
    public AudioSource bGM;

    private Vector3 newScale;
    private float idleTimer;
    private float localCharacterSpeed = 5;

	// Use this for initialization
	void Start () {
        anim.GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {

        //Increases idle timer for playing the animation for a long idle
        idleTimer += Time.deltaTime;
        anim.SetFloat("idleTime", idleTimer);


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            MoveLeftRight();
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            MoveUpDown();
        }

        //sets false to movLeftRight when there is no movement
        if (Input.GetAxis(axisLeftRight) == 0)
        {
            anim.SetBool("movLeftRight", false);
        }
        //sets false to movUp and movDown when there is no movement
        if (Input.GetAxis(axisUpDown) == 0)
        {
            anim.SetBool("movUp", false);
            anim.SetBool("movDown", false);
        }
        //Resets idleTimer
        if (idleTimer > 12f)
        {
            ResetIdle();
        }

	}

    private void MoveLeftRight()
    {
        //moves right
        if (Input.GetAxis(axisLeftRight) > 0)
        {
            newScale = transform.localScale;
            newScale.x = 1.0f;
            transform.localScale = newScale;
            anim.SetBool("movLeftRight", true);
            transform.position += transform.right * Input.GetAxis(axisLeftRight) * localCharacterSpeed * Time.deltaTime;
        }
        //moves left
        if (Input.GetAxis(axisLeftRight) < 0)
        {
            newScale = transform.localScale;
            newScale.x = -1.0f;
            transform.localScale = newScale;
            anim.SetBool("movLeftRight", true);
            transform.position += transform.right * Input.GetAxis(axisLeftRight) * localCharacterSpeed * Time.deltaTime;
        }
    }

    private void MoveUpDown()
    {
        //moves down
        if (Input.GetAxis(axisUpDown) < 0)
        {
            anim.SetBool("movUp", false);
            anim.SetBool("movDown", true);
            transform.position += transform.up * Input.GetAxis(axisUpDown) * localCharacterSpeed * Time.deltaTime;
        }
        //moves Up
        if (Input.GetAxis(axisUpDown) > 0)
        {
            anim.SetBool("movDown", false);
            anim.SetBool("movUp", true);
            transform.position += transform.up * Input.GetAxis(axisUpDown) * localCharacterSpeed * Time.deltaTime;
        }
    }

    private void ResetIdle()
    {
        idleTimer = -1f;
    }
}