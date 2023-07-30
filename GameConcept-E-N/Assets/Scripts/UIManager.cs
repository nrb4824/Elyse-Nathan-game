using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject deathPanel;
    private GameObject winPanel;

    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        /*if (GameObject.FindGameObjectWithTag("DeathPanel") != null)
        {
            Debug.Log("working1");
            
        }
        if (GameObject.FindGameObjectWithTag("WinPanel") != null)
        {
            Debug.Log("working2");
            
        }*/

    }
    public void ToggleDeathPanel()
    {
        deathPanel = GameObject.FindGameObjectWithTag("DeathPanel");
        deathPanel.SetActive(!deathPanel.activeSelf);
    }
    public void ToggleWinPanel()
    {
        winPanel = GameObject.FindGameObjectWithTag("WinPanel");
        Console.WriteLine("Pannel");
        winPanel.SetActive(!winPanel.activeSelf);
    }
}
