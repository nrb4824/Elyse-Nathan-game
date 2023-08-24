using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


//Controls looking around.
public class MouseLook : MonoBehaviour
{
    public float sensitivity;

    public Transform orientation;
    public Transform camHolder;

    private float xRotation;
    private float yRotation;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sliderText.text = (sensitivity/5).ToString();
        slider.value = sensitivity / 5;
        slider.onValueChanged.AddListener((var) =>
        {
            sliderText.text = var.ToString("0");
            sensitivity = var*5;
        });

    }

    // Update is called once per frame
    // Updates the look 
    void Update()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //rotate cam and orientation
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
}
