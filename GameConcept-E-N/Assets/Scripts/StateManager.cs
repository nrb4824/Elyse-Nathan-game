using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StateManager : MonoBehaviour
{
    public void Awake()
    {
    }
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ChangeSceneByName(string name)
    {
        if(name != null)
        {
            PauseMenu.GameIsPaused = false;
            if (name == "Tutorial")
            {
                AudioManager a = FindObjectOfType<AudioManager>();
                AudioSource s = Array.Find(a.audioSources, audioSources => audioSources.name == "Menu Screen");
                a.Play("BeachSound");
                a.Play("WindBottom");
                a.Play("WindTop");
                a.PlayGullSounds();
                a.Stop("MenuMusic");
            }

            if (name != "Menu" && name != "Controls" && name != "MissionObjective")
            {
                SceneManager.LoadScene(name);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;

            }
            else
            {
                SceneManager.LoadScene(name);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            
            
        }
        
    }
}
