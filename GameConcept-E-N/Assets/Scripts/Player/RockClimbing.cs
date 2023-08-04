using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockClimbing : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public PlayerMovement pm;
    public LedgeGrabbing lg;
    public LayerMask Climb;

    [Header("Climbing")]
    private bool climbing;
    public float verticalClimbSpeed;
    public float horizontalClimbSpeed;
    public float diagonalClimbSpeed;
    private float horizontalInput;
    private float verticalInput;

    [Header("ClimbJumping")]
    public float climbJumpUpForce;
    public float climbJumpBackForce;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    [Header("Exiting")]
    public bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    private void Start()
    {
        lg = GetComponent<LedgeGrabbing>();
    }
    private void Update()
    {
        WallCheck();
        StateMachine();

        if (climbing && !exitingWall) ClimbingMovement();
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // State 0 - LedgeGrabbing
        if (lg.holding)
        {
            if (climbing) StopClimbing();
        }
        // State 1 -Climbing
        else if (wallFront && wallLookAngle < maxWallLookAngle && !exitingWall)
        {
            if (!climbing) StartClimbing();

        }

        // State 2 - Exiting
        else if (exitingWall)
        {
            if (climbing) StopClimbing();

            if (exitWallTimer > 0) exitWallTimer -= Time.deltaTime;
            if (exitWallTimer < 0) exitingWall = false;
        }

        // State 3 - None
        else
        {
            if (climbing) StopClimbing();
        }
    }

    private void WallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, Climb);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
    }

    private void StartClimbing()
    {
        climbing = true;
        pm.rockClimb = true;
    }

    private void ClimbingMovement()
    {
        Vector3 wallNormal = frontWallHit.normal;

        Vector3 wallRight = Vector3.Cross(wallNormal, transform.up);

        Vector3 wallUp = Vector3.Cross(wallRight, wallNormal);

        if (Input.GetKeyDown(jumpKey))
        {
            ClimbJump();
            Debug.Log("jump");
        }

        //Get directional inputs, maybe change this to horizontal and vertical input later.
        if(verticalInput == 1 && horizontalInput == -1)
        {
            //up left
            rb.velocity = (wallUp - wallRight) * diagonalClimbSpeed;
        }
        else if (verticalInput == 1 && horizontalInput == 1)
        {
            //up right
            rb.velocity = (wallUp + wallRight) * diagonalClimbSpeed;
        }
        else if (verticalInput == -1 && horizontalInput == -1)
        {
            //down left
            rb.velocity = (-wallUp - wallRight) * diagonalClimbSpeed;
        }
        else if (verticalInput == -1 && horizontalInput == 1)
        {
            //down right
            rb.velocity = (-wallUp + wallRight) * diagonalClimbSpeed;
        }
        else if (verticalInput == 1)
        {
            //up
            rb.velocity = wallUp * verticalClimbSpeed;
        }
        else if (horizontalInput == -1)
        {
            //left
            rb.velocity = -wallRight * horizontalClimbSpeed;
        }
        else if (verticalInput == -1)
        {
            //down
            rb.velocity = -wallUp * verticalClimbSpeed;
        }
        else if (horizontalInput == 1)
        {
            //right
            rb.velocity = wallRight * horizontalClimbSpeed;
        }
        
        //check to see if the player is moving
        if (horizontalInput == 0 && verticalInput == 0 && !exitingWall)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            rb.constraints = ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionY & ~RigidbodyConstraints.FreezePositionZ;
        }


    }

    private void StopClimbing()
    {
        climbing = false;
        pm.rockClimb = false;
        rb.constraints = ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionY & ~RigidbodyConstraints.FreezePositionZ;
    }

    private void ClimbJump()
    {
        if (pm.grounded) return;
        if (lg.holding || lg.exitingLedge) return;

        exitingWall = true;
        exitWallTimer = exitWallTime;
        Vector3 forceToApply = transform.up * climbJumpUpForce + frontWallHit.normal * climbJumpBackForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
