using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        PlayerController.OnGamePaused += pauseEvent;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void loadMenu()
    {
        Debug.Log("Loading main menu...");
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    void pauseEvent()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
