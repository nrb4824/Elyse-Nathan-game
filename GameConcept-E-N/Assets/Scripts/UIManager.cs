using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject winPanel;
    public void ToggleDeathPanel()
    {
        deathPanel.SetActive(!deathPanel.activeSelf);
    }
    public void ToggleWinPanel()
    {
        Console.WriteLine("Pannel");
        winPanel.SetActive(!winPanel.activeSelf);
    }
}
