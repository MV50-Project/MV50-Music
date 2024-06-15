using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMainManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gunToggle;
    public GameObject disque;

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(false);
        gunToggle.SetActive(true);
    }
    

    public void OnChangeClicked()
    {
        TogglePauseMenu();
        disque.transform.position = new Vector3(-0.28443f, 0.40526f, 0.77927f);
        GrabbableBehavior grabbableBehaviorScript = disque.GetComponent<GrabbableBehavior>();
        grabbableBehaviorScript.grabType = GrabType.Snap;
        disque.GetComponent<AudioSource>().Stop();
    }
    
    public void OnPlayClicked()
    {
        TogglePauseMenu();
        const string sceneName = "SampleScene";
        SceneManager.LoadScene(sceneName);
    }
}
