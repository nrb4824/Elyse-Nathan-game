using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject Controls;
    public GameObject Settings;
    public GameObject restartPanel;
    public bool controlsActive = false;
    public bool settingsActive = false;
    public bool restartActive = false;
    public GameObject settingsButton;
    public GameObject controlsButton;
    public GameObject restartButton;
    private GameObject levelManager;
    

    void Awake()
    {
        levelManager = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name != "Tutorial")
            {
                Back();
            }
            else if(GameIsPaused)
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
        HideControls();
        HideSettings();
        HideRestart();

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
    public void Back()
    {
        controlsActive = false;
        settingsActive = false;
        HideControls();
        HideSettings();

        PauseMenuUI.SetActive(false);
    }
        
    public void ShowRestart()
    {
        restartPanel.SetActive(true);
        restartActive = true;
        if (settingsActive) HideSettings();
        if (controlsActive) HideControls();
        restartButton.SetActive(true);
    }

    public void HideRestart()
    {
        restartPanel.SetActive(false);
        restartActive = false;
        restartButton.SetActive(false);
    }

    public void RestartLevel()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartFromCheckPoint()
    {
        Resume();
        levelManager.GetComponent<CheckPointManager>().RespawnFromCheckPoint();
    }

    public void ShowControls()
    {
        Controls.SetActive(true);
        controlsActive = true;
        if (settingsActive) HideSettings();
        if (restartActive) HideRestart();
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
        if (restartActive) HideRestart();
        settingsButton.SetActive(true);
    }
    public void HideSettings()
    {
        Settings.SetActive(false);
        settingsActive = false;
        settingsButton.SetActive(false);
    }
}
