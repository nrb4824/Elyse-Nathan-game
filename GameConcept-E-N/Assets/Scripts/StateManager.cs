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
            if (name != "Menu" && name != "Controls" && name != "MissionObjective")
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                AudioManager a = FindObjectOfType<AudioManager>();
                a.StopBirds();

            }
            SceneManager.LoadScene(name);


            if (name == "Tutorial")
            {
                
                AudioManager a = FindObjectOfType<AudioManager>();
                Sound s = Array.Find(a.sounds, sound => sound.name == "Menu Screen");
                s.playing = false;
                a.PlayBirds();
                a.Stop("Menu Screen");
            }
            
        }
        
    }
}
