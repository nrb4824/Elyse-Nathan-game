using System;
using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool destroyed;
    public bool atEnd;
    public GameObject endBlock;
    public Material canEnter;

    private void Awake()
    {
        atEnd = false;
        destroyed = false;
        if (LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        UIManager ui = GetComponent<UIManager>();
        if(ui != null)
        {
            Time.timeScale = 0f;
            ui.ToggleDeathPanel();
        }
    }
    public void GameWon()
    {
        UIManager ui = GetComponent<UIManager>();
        if(ui != null && destroyed && atEnd)
        {
            Console.WriteLine("if statement");
            ui.ToggleWinPanel();
        }
    }
}
