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
    public LayerMask ceilingClimb;

    [Header("CeilingClimb")]
    private bool climbing;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("ClimbJumping")]
    public KeyCode dismountKey = KeyCode.LeftControl;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    private RaycastHit ceilingHit;
    private bool ceiling;

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

        if (climbing && !exitingCeiling) ClimbingMovement();
    }


    private void StateMachine()
    {
        // State 0 - LedgeGrabbing
        if (lg.holding)
        {
            if (climbing) StopClimbing();
        }
        // State 1 -Climbing
        else if (ceiling && !exitingCeiling)
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

        if (ceiling && Input.GetKeyDown(dismountKey)) ClimbDismount();
    }

    private void CeilingCheck()
    {
        ceiling = Physics.SphereCast(transform.position, sphereCastRadius, orientation.up, out ceilingHit, detectionLength, ceilingClimb);
    }

    private void StartClimbing()
    {
        climbing = true;
        pm.ceilingClimb = true;
    }

    private void ClimbingMovement()
    {
        Vector3 ceilingNormal = ceilingHit.normal;

        Vector3 ceilingForward = Vector3.Cross(ceilingNormal, transform.up);
        Vector3 ceilingRight = Vector3.Cross(ceilingNormal, ceilingHit.transform.forward);

        if ((orientation.forward - ceilingForward).magnitude > (orientation.forward - -ceilingForward).magnitude)
        {
            ceilingForward = -ceilingForward;
        }

        // weaken gravity
        if (useGravity)
        {
            if ((orientation.forward - ceilingForward).magnitude < (orientation.forward - -ceilingForward).magnitude)
            {
                ceiling = false;
            }
            else
            {
                rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
            }
        }
    }

    private void StopClimbing()
    {
        climbing = false;
        pm.ceilingClimb = false;
    }

    private void ClimbDismount()
    {
        if (pm.grounded) return;
        if (lg.holding || lg.exitingLedge) return;

        exitingCeiling = true;
        exitCeilingTimer = exitCeilingTime;
        Vector3 forceToApply = transform.forward;
    }
}
