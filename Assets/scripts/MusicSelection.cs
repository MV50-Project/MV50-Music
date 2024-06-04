using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("disque_song1"))
        {
            Debug.Log("salut a tous");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
