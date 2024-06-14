using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource musicPlayer;
    public GameObject rightGun;
    public GameObject leftGun;
    public GameObject endingMenu;


    public void TogglePauseMenu()
    {
        if (endingMenu.activeSelf == false)
        {
            if (!pauseMenu) return;
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            rightGun.SetActive(!rightGun.activeSelf);
            leftGun.SetActive(!leftGun.activeSelf);

            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
                musicPlayer.Pause();
            }
            else
            {
                Time.timeScale = 1;
                musicPlayer.Play();
            }
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
    
    public void OnRetryClicked()
    {
        TogglePauseMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OnMainMenuClicked()
    {
        TogglePauseMenu();
        const string sceneName = "MainMenu";
        SceneManager.LoadScene(sceneName);
    }
}
