using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject Controls;
    public GameObject Settings;
    public bool controlsActive = false;
    public bool settingsActive = false;
    public GameObject settingsButton;
    public GameObject controlsButton;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        controlsActive = false;
        settingsActive = false;
        HideControls();
        HideSettings();

        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowControls()
    {
        Controls.SetActive(true);
        controlsActive = true;
        if (settingsActive) HideSettings();
        controlsButton.SetActive(true);
    }
    public void HideControls()
    {
        Controls.SetActive(false);
        controlsActive = false;
        controlsButton.SetActive(false);
    }

    public void ShowSettings()
    {
        Settings.SetActive(true);
        settingsActive = true;
        if (controlsActive) HideControls();
        settingsButton.SetActive(true);
    }
    public void HideSettings()
    {
        Settings.SetActive(false);
        settingsActive = false;
        settingsButton.SetActive(false);
    }
}
