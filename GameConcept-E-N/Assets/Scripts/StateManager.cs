using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StateManager : MonoBehaviour
{
    public void Awake()
    {
        if(SceneManager.GetActiveScene().name == "lv1")
        {

        }
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
            }
            SceneManager.LoadScene(name);

            
        }
        
    }
}
