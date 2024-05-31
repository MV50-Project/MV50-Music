using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public void TogglePauseMenu()
    {
        if(pauseMenu)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
}
