using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public int checkPointIndex;
    public Vector3 vectorPoint;
    private Checkpoint checkpoint;
    private UIManager UIManager;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            checkpoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Checkpoint>();
        }
        UIManager = this.GetComponent<UIManager>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RespawnFromCurrentCheckPoint()
    {
        UIManager.ToggleDeathPanel();
        Time.timeScale = 1f;
        checkpoint.player.SetActive(true);
        checkpoint.player.transform.position = vectorPoint;
    }
}
