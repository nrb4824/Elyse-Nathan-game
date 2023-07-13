using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Rigidbody rb;


    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TutorialArea")
        {
            TutorialArea o = other.gameObject.GetComponent<TutorialArea>();
            o.message.SetActive(true);
            o.active = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        TutorialArea o = other.gameObject.GetComponent<TutorialArea>();
        o.message.SetActive(false);
        o.active = false;
    }
}
