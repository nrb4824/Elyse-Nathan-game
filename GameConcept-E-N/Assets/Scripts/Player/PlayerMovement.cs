using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public float wallrunSpeed;
    public float climbSpeed;
    public float ceilingClimbSpeed;
    public float rockClimbSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;
    public float ceilingDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;
    public float fallMultiplier;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;
    private bool lowCeiling;
    private bool crouch;

    [Header("Stairs")]
    public GameObject stepRayUpper;
    public GameObject stepRayLower;
    public float stepHeight;
    public float stepSmooth;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    public LayerMask Water;
    public bool watered;
    public bool grounded;
    public bool wallGroundCheck;
    public float fallingSpeed = 0.0f;

    [Header("End Check")]
    /*public bool atEnd = false;
    public LayerMask EndBlock;*/
    public bool atEnd = false;
    public KeyCode EndKey = KeyCode.E;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    public float slopeSpeed;

    [Header("References")]
    public Climbing climbingScript;
    public GameObject capsule;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        freeze,
        unlimited,
        walking,
        sprinting,
        wallRunning,
        climbing,
        ceilingClimb,
        rockClimb,
        crouching,
        sliding,
        air,
    }

    public bool sliding;
    public bool crouching;
    public bool wallrunning;
    public bool climbing;
    public bool ceilingClimb;
    public bool rockClimb;

    public bool freeze;
    public bool unlimited;

    public bool restricted;

    public bool standing;


    bool keepMomentum;

    bool soundWalking;
    bool soundRunning;
    bool soundJump;
    bool falling;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight + rb.position.y-1, stepRayUpper.transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        watered = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Water);
        lowCeiling = Physics.Raycast(transform.position, Vector3.up, playerHeight * 0.5f + 0.2f, Ground);

        MyInput();
        SpeedControl();
        StateHandler();

        if(watered)
        {
            LevelManager.instance.GameOver();
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (grounded)
        {
            wallGroundCheck = true;
            rb.drag = groundDrag;
        }
        else if(ceilingClimb)
        {
            rb.drag = ceilingDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (state == MovementState.walking && !soundWalking && !standing)
        {
            soundWalking = true;
            FindObjectOfType<AudioManager>().Play("Walking");
        }
        else if((state != MovementState.walking || standing) && soundWalking)
        {
            soundWalking = false;
            FindObjectOfType<AudioManager>().Stop("Walking");
        }
        else if (!soundRunning && state == MovementState.sprinting && !standing)
        {
            soundRunning = true;
            FindObjectOfType<AudioManager>().Play("Running");
        }
        else if(state != MovementState.sprinting && soundRunning)
        {
            soundRunning = false;
            FindObjectOfType<AudioManager>().Stop("Running");
        }
        else if(state != MovementState.air && soundJump)
            {
            soundJump = false;
            FindObjectOfType<AudioManager>().Play("JumpingDown");
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0f || verticalInput != 0f) standing = false;
        else standing = true;

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // start crouch
        if (Input.GetKeyDown(crouchKey) && !crouch)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            crouch = true;
        }

        // stop crouch
        else if (Input.GetKeyDown(crouchKey) && !lowCeiling && crouch)
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            crouch = false;
        }
    }

    private void StateHandler()
    {   // at the end of the game
        if(atEnd && Input.GetKey(EndKey))
        {
            
            LevelManager.instance.atEnd = true;
            LevelManager.instance.GameWon();
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            
        }
        // Mode - Freeze
        else if (freeze)
        {
            state = MovementState.freeze;
            rb.velocity = Vector3.zero;
        }

        // Mode - Unlimited
        else if (unlimited)
        {
            state = MovementState.unlimited;
            moveSpeed = 999f;
            return;
        }

        //Mode - rockClimb
        else if(rockClimb)
        {
            state = MovementState.rockClimb;
            desiredMoveSpeed = rockClimbSpeed;
        }

        //Mode - ceilingClimb
        else if(ceilingClimb)
        {
            state = MovementState.ceilingClimb;
            desiredMoveSpeed = ceilingClimbSpeed;
        }

        // Mode - Climbing
        else if (climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
        }

        // Mode - Wallrunning
        else if (wallrunning)
        {
            state = MovementState.wallRunning;
            desiredMoveSpeed = wallrunSpeed;
        }

        // Mode - Sliding
        else if (sliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
            {
                desiredMoveSpeed = slideSpeed;
                keepMomentum = true;
            }
            else
            {
                desiredMoveSpeed = sprintSpeed;
            }
        }

        // Mode - Crouching
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }

        //Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }

        //Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;

        }

        //Mode - Air
        else
        {
            state = MovementState.air;
        }

        // check if desiredMoveSpeed has changed drastically
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 6f && moveSpeed != 0)  // Change the 4 to a greater number if difference between sprinting and walking is increased
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;

        // deactivate keepMomentum
        if (Mathf.Abs(desiredMoveSpeed - moveSpeed) < 0.1f) keepMomentum = false;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            
            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
            {
                time += Time.deltaTime * speedIncreaseMultiplier;
            }
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    {
        
        if (climbingScript.exitingWall || restricted) return;

        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * slopeSpeed, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        // on ground
        else if (grounded)
        {

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            //stepClimb();
        }

        // in air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // turn gravity off while on slope
        if (!wallrunning)
        {
            rb.useGravity = !OnSlope();
        }
        
    }

    private void SpeedControl()
    {
        if (falling && fallingSpeed > -30.0f && grounded)
        {
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            UnityEngine.Debug.Log("working");
            capsule.GetComponent<Player>().TakeDamage(fallingSpeed);
        }
        if (rb.velocity.y < 0)
        {
            bool falling = true;
            if (fallingSpeed > rb.velocity.y)
            {
                fallingSpeed = rb.velocity.y;
            }
            
            UnityEngine.Debug.Log(fallingSpeed< -30.0f);
            UnityEngine.Debug.Log(falling);
            UnityEngine.Debug.Log(grounded);
        }
        
        // limiting speed on slope
        else if (OnSlope() && !exitingSlope)
        {
            //UnityEngine.Debug.Log("working2");
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }

        // limiting speed on ground or in air
        else
        {
            //UnityEngine.Debug.Log("working3");
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            
            //limit velocity if needed
            if (flatVel.magnitude > moveSpeed && rb.velocity.y >=0)
            {
                
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        FindObjectOfType<AudioManager>().Play("JumpingUp");
        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
    }

    private void ResetJump()
    {
        soundJump = true;
        readyToJump = true;
        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    public void stepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, .6f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, .7f))
            {
                rb.position -= new Vector3(0f, stepSmooth, 0f);
            }

        }

        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.6f))
        {
            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.7f))
            {
                rb.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.6f))
        {
            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.7f))
            {
                rb.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "End")
        {
            TutorialArea o = other.gameObject.GetComponent<TutorialArea>();
            o.message.SetActive(true);
            o.active = true;
            atEnd = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "End")
        {
            TutorialArea o = other.gameObject.GetComponent<TutorialArea>();
            o.message.SetActive(false);
            o.active = false;
            atEnd = false;
        }
    }
}


