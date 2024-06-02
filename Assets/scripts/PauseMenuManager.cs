using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource musicPlayer;

    public void TogglePauseMenu()
    {
        if (!pauseMenu) return;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
            
        if(pauseMenu.activeSelf)
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
    
    public void OnVolumeChanged(float value)
    {
        musicPlayer.volume = value;
    }

    public void OnResumeClicked()
    {
        TogglePauseMenu();
    }
    
    public void OnMainMenuClicked()
    {
        TogglePauseMenu();
        // Cacher la map de la musique
        // Afficher le menu principal
        const string sceneName = "MainMenu";
        SceneManager.LoadScene(sceneName);
    }
}
