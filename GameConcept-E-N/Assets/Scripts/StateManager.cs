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
            if (name == "Tutorial")
            {
                UnityEngine.Debug.Log("help");
                AudioManager a = FindObjectOfType<AudioManager>();
                Sound s = Array.Find(a.sounds, sound => sound.name == "Menu Screen");
                s.playing = false;
                a.PlayBirds();
                a.Stop("Menu Screen");
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
