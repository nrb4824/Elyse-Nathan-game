using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    private float speed = 6.0f;
    private float gravity = -19.62f;
    private float jumpHeight = 1.5f;

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
    private float crouchingMultiplier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if you are touching any part of the world. (Anything that has label "Ground")
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Allows jump when grounded, if you are crouching disables crouch
        if((Input.GetButtonDown("Jump") && isGrounded) || (Input.GetButtonDown("Jump") && isCrouching == true))
        {
            isCrouching = false;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Toggles crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if(isCrouching == false)
            {
                isCrouching = true;
            }
            else if(isCrouching == true)
            {
                isCrouching = false;
            }
        }

        //toggles sprint. disables crouch if crouched
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            isCrouching = false;
        }
        else
        {
            isSprinting = false;
        }

        Vector3 move = transform.right * x + transform.forward * z;  //creates move which controls direction/speed.

        if (isCrouching == true)
        {
            controller.height = crouchingHeight;
            move *= crouchingMultiplier;
        }
        else
        {
            controller.height = standingHeight;
        }

        if (isSprinting == true)
        {
            move *= sprintMultiplier;
        }

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
