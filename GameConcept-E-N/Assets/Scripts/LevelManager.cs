using System;
using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    //public bool destroyed;
    public bool atEnd;
    public GameObject endBlock;
    public Material canEnter;

    private void Awake()
    {
        atEnd = false;
        //destroyed = false;
        if (LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
        
    }
    private void Start()
    {
        AudioManager a = FindObjectOfType<AudioManager>();
        AudioSource s = Array.Find(a.audioSources, audioSources => audioSources.name == "MenuMusic");
        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            a.Play("MenuMusic");
        }
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
        //if(ui != null && destroyed && atEnd)
        if (ui != null && atEnd)
        {
            Console.WriteLine("if statement");
            ui.ToggleWinPanel();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
