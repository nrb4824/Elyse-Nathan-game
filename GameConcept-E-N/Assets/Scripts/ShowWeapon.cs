using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWeapon : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] public GameObject gun;
    // Add more gameobjects to rotate through

    public bool showGun;


    // Start is called before the first frame update
    void Start()
    {
        showGun = false;

        //Set aditional items to false
    }

    // Update is called once per frame
    void Update()
    {
        if (showGun == false) gun.SetActive(false);

        if (showGun == true) gun.SetActive(true);

        if(Input.GetKeyDown(KeyCode.Alpha1) && showGun == false)
        {
            showGun = true;
            //set all other items to false
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            showGun = false;
            //set all items to false
        }
    }
}
