using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenuMaanager : MonoBehaviour
{
    public GameObject endingMenu;

    public void TogglePauseMenu()
    {
        endingMenu.SetActive(false);
    }


    public void OnRertyClicked()
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
