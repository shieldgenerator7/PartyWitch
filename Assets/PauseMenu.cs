﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        PlayerController.OnGamePaused += pauseEvent;
    }

    // Update is called once per frame
    void Update()
    {

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
