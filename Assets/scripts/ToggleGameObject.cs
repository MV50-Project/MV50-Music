using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObjectToToggle;

    // Update is called once per frame
    public void Toggle()
    {
        if (gameObjectToToggle)
        {
            gameObjectToToggle.SetActive(!gameObjectToToggle.activeSelf);
        }
    }
}
