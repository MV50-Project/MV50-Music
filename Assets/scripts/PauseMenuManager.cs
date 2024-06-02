using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource musicPlayer;

    public void TogglePauseMenu()
    {
        if(pauseMenu)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
    
    public void OnVolumeChanged(float value)
    {
        musicPlayer.volume = value;
    }

    public void OnResumeClicked()
    {
        TogglePauseMenu();
    }
}
