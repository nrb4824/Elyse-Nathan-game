using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    /*public CharacterController controller;

    private float speed = 6.0f;
    private float gravity = -19.62f;
    private float jumpHeight = 1.5f;

    Rigidbody rb;

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    public bool isSprinting = false;
    private float sprintMultiplier = 2.0f;

    private float crouchingHeight = 3.0f;
    private float standingHeight = 5.0f;
    public bool isCrouching = false;
    private float crouchingMultiplier = 0.5f;*/

    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    private bool grounded;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        MyInput();
        SpeedControl();

        //handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
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

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

        /*//Checks if you are touching any part of the world. (Anything that has label "Ground")
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        jump();
        crouchToggle();
        sprintToggle();

        Vector3 move = transform.right * x + transform.forward * z;  //creates move which controls direction/speed.

        //applys the crouch
        if (isCrouching == true)
        {
            controller.height = crouchingHeight;
            move *= crouchingMultiplier;
        }
        else
        {
            controller.height = standingHeight;
        }

        //applys the sprint
        if (isSprinting == true)
        {
            move *= sprintMultiplier;
        }

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void jump()
    {
        //Allows jump when grounded, if you are crouching disables crouch
        if ((Input.GetButtonDown("Jump") && isGrounded) || (Input.GetButtonDown("Jump") && isCrouching == true))
        {
            isCrouching = false;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void crouchToggle()
    {
        //Toggles crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (isCrouching == false)
            {
                isCrouching = true;
            }
            else if (isCrouching == true)
            {
                isCrouching = false;
            }
        }
    }

    void sprintToggle()
    {
        //toggles sprint. disables crouch if crouched
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isSprinting == false)
            {
                isSprinting = true;
                isCrouching = false;
            }
            else if (isSprinting == true)
            {
                isSprinting = false;
            }
        }
    }*/
}
