using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutoMainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject sphere;
    [SerializeField]
    private GameObject shpereTiming;


    private int bpm = 105;
    private AudioClip tutoNote;
    private float beatTime;
    private float nextBeatTime;
    private Vector3 sphereLocation = new Vector3(0f,1.515f,2.326f);
    private GameObject newSphere;
    private GameObject newSphereTiming;




    void Start()
    {

        tutoNote = Resources.Load<AudioClip>("sound3");
        beatTime = 60f / bpm;
        nextBeatTime = Time.time + 6 * beatTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextBeatTime)
        {
            newSphere = Instantiate(sphere, sphereLocation, Quaternion.Euler(90f, 0f, 0f));
            newSphere.GetComponent<AudioSource>().clip = tutoNote;
            newSphereTiming = Instantiate(shpereTiming, sphereLocation, Quaternion.identity);
            newSphereTiming.transform.SetParent(newSphere.transform);
            nextBeatTime += beatTime * 4;
        }
    }
}
