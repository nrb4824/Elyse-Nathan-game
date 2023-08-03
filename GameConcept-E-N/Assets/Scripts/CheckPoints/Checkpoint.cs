using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject player;
    private CheckPointManager cpm;
    public List<GameObject> checkPoints;
    [SerializeField] GameObject flag;


    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("LevelManager") != null)
        {
            cpm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<CheckPointManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CheckPoint"))
        {
            cpm.vectorPoint = other.transform.position;
            other.GetComponent<Collider>().enabled = false;
            cpm.checkPointIndex = other.GetComponent<CheckPointObject>().index -1;
            flag.SetActive(true);
            StartCoroutine(Flag());
        }
        
    }

    IEnumerator Flag()
    {
        yield return new WaitForSeconds(3.0f);
        flag.SetActive(false);
    }
}
