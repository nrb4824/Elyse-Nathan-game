using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    /*private RespawnScript respawn;
    private BoxCollider checkPointCollider;*/
    public GameObject player;
    private CheckPointManager cpm;
    [SerializeField] List<GameObject> checkPoints;


    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("LevelManager") != null)
        {
            cpm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<CheckPointManager>();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CheckPoint"))
        {
            cpm.vectorPoint = other.transform.position;
            other.GetComponent<Collider>().enabled = false;
            cpm.checkPointIndex = other.GetComponent<CheckPointObject>().index;
            /*Destroy(other.gameObject);*/
        }
        
    }
}
