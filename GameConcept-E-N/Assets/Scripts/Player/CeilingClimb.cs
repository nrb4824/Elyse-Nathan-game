using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingClimb : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public PlayerMovement pm;
    public LedgeGrabbing lg;
    public LayerMask Climb;

    [Header("CeilingClimb")]
    public float climbSpeed;

    private bool climbing;

    [Header("ClimbJumping")]
    /*public float climbJumpUpForce;
    public float climbJumpBackForce;*/

    public KeyCode dismountKey = KeyCode.LeftControl;
    /*public int climbJumps;
    private int climbJumpsLeft;*/

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;

    private RaycastHit CeilingHit;
    private bool ceiling;

    private Transform lastceiling;
    private Vector3 lastCeilingNormal;
    public float minCeilingNormalAngleChange;

    [Header("Exiting")]
    public bool exitingCeiling;
    public float exitCeilingTime;
    private float exitCeilingTimer;

    private void Start()
    {
        lg = GetComponent<LedgeGrabbing>();
    }
    private void Update()
    {
        CeilingCheck();
        StateMachine();

        if (climbing && !exitingWall) ClimbingMovement();
    }

    private void StateMachine()
    {
        // State 0 - LedgeGrabbing
        if (lg.holding)
        {
            if (climbing) StopClimbing();
        }
        // State 1 -Climbing
        else if (ceiling && Input.GetKey(KeyCode.W) && !exitingCeiling)
        {
            if (!climbing) StartClimbing();

        }

        // State 2 - Exiting
        else if (exitingCeiling)
        {
            if (climbing) StopClimbing();

            if (exitCeilingTimer > 0) exitCeilingTimer -= Time.deltaTime;
            if (exitCeilingTimer < 0) exitingCeiling = false;
        }

        // State 3 - None
        else
        {
            if (climbing) StopClimbing();
        }

        if (Ceiling && Input.GetKeyDown(dismountKey)) ClimbJump();
    }

    private void CeilingCheck()
    {
        ceiling = Physics.SphereCast(transform.position, sphereCastRadius, orientation.up, out ceilingHit, detectionLength, ceilingClimb);
        //wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        //bool newWall = frontWallHit.transform != lastWall || Mathf.Abs(Vector3.Angle(lastWallNormal, frontWallHit.normal)) > minWallNormalAngleChange;

        /*if ((wallFront && newWall) || pm.grounded)
        {
            climbJumpsLeft = climbJumps;
        }*/
    }

    private void StartClimbing()
    {
        climbing = true;
        pm.climbing = true;

        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
        rb.freezeRotation = true;

        //lastWall = frontWallHit.transform;
        lastCeilingNormal = CeilingHit.normal;
    }

    private void ClimbingMovement()
    {
        rb.velocity = new Vector3(climbSpeed, rb.velocity.y, rb.velocity.z);
        rb.freezeRotation = true;
    }

    private void StopClimbing()
    {
        climbing = false;
        pm.climbing = false;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionY;
        rb.freezeRotation = true;
    }

    private void ClimbJump()
    {
        if (pm.grounded) return;
        if (lg.holding || lg.exitingLedge) return;

        exitingCeiling = true;
        exitCeilingTimer = exitCeilingTime;
        Vector3 forceToApply = transform.forward + frontWallHit.normal;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        //climbJumpsLeft--;
    }


}
