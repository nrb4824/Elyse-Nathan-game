using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Controls looking around.
public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 1000f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //prevent looking more than 180 degrees up/down
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    // Updates the look 
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
