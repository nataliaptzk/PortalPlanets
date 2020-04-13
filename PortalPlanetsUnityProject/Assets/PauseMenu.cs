using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] private GameObject _pauseMenu;

    private void Start()
    {
        isPaused = false;
        _pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ChangePauseState();
        }
    }


    private void ChangePauseState()
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