using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class manages the pause menu.
///  - Natalia Pietrzak
/// </summary>
public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] private GameObject _pauseMenu;

    private void Awake()
    {
        isPaused = false;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ChangePauseState();
        }
    }


    public void ChangePauseState()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            _pauseMenu.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            _pauseMenu.SetActive(true);
            isPaused = true;
        }
    }
}