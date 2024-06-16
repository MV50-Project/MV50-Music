using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class EndingMenuMaanager : MonoBehaviour
{
    public GameObject endingMenu;
    public GameObject Button;
    public TMP_Text finalScoreText;


    public void TogglePauseMenu()
    {
        endingMenu.SetActive(false);
    }


    public void OnRertyClicked()
    {
        TogglePauseMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnSaveClicked()
    {

        string scoreText = finalScoreText.text;
        int index = scoreText.IndexOf(':') + 1;
        string scoreString = scoreText.Substring(index).Trim();
        string mapName = PlayerPrefs.GetString("EntryMethod", "none");
        string scoreboardPath = "scoreboardFinal" + mapName + ".txt";
        string fullPath = Path.Combine(Application.persistentDataPath, scoreboardPath);
        scoreText = scoreString + "\n";
        File.AppendAllText(fullPath, scoreText);
        Destroy(Button);
        
    }

    public void OnMainMenuClicked()
    {
        TogglePauseMenu();
        const string sceneName = "MainMenu";
        SceneManager.LoadScene(sceneName);
    }
}
