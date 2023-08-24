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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RespawnFromPreviousCheckPoint();
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            RespawnFromNextCheckPoint();
        }

    }
    
    public void RespawnFromCheckPointDeath()
    {
        UIManager.ToggleDeathPanel();
        Time.timeScale = 1f;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        checkpoint.player.SetActive(true);
        checkpoint.player.transform.position = vectorPoint;
    }

    public void RespawnFromCheckPoint()
    {
        checkpoint.player.transform.position = vectorPoint;
    }

    public void RespawnFromNextCheckPoint()
    {
        Debug.Log("Next Check Point.");
        if(checkPointIndex >= checkpoint.checkPoints.Count-1)
        {
            Debug.Log("You are at the last Check Point.");
            RespawnFromCheckPoint();
        }
        else
        {
            checkpoint.player.transform.position = checkpoint.checkPoints[checkPointIndex+1].transform.position;
            vectorPoint = checkpoint.checkPoints[checkPointIndex+1].transform.position;
            checkPointIndex += 1;
        }
        
    }

    public void RespawnFromPreviousCheckPoint()
    {
        Debug.Log("Previous Check Point.");
        if(checkPointIndex - 1 < 0)
        {
            Debug.Log("You are at the first Check Point.");
            RespawnFromCheckPoint();
        }
        else
        {
            checkpoint.player.transform.position = checkpoint.checkPoints[checkPointIndex - 1].transform.position;
            vectorPoint = checkpoint.checkPoints[checkPointIndex - 1].transform.position;
            checkPointIndex -= 1;
        }
        
    }
}
