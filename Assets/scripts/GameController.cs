using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject sphere;
    [SerializeField]
    private GameObject shpereTiming;
    [SerializeField]
    private GameObject secondSphereTiming;



    private AudioSource audioSource;
    private AudioClip[] leadNotes = new AudioClip[5];

    // Level variables
    [System.Serializable]
    private class MusicInfo
    {
        public string title;
        public string artist;
        public string album;
        public int year;
        public int bpm;
        public int duration;
        public string path;
    }
    [System.Serializable]
    private class LevelInfo
    {
        public string description;
        public int difficulty;
    }
    [System.Serializable]
    private class LevelData
    {
        public int initialWaitForBeat;
        public Key[] keys;
    }
    [System.Serializable]
    private class Key
    {
        public float waitForBeat;
        public int note;
        public Coordinates coordinates;
    }
    [System.Serializable]
    private class Coordinates
    {
        public float x;
        public float y;
        public float z;
    }
    [System.Serializable]
    private class Root
    {
        public string name;
        public string version;
        public MusicInfo music;
        public LevelInfo level;
        public LevelData data;
    }

    private string songName = new string("None");
    private string version = new string("0.0.0");
    private MusicInfo musicInfo = new MusicInfo();
    private LevelInfo levelInfo = new LevelInfo();
    private LevelData levelData = new LevelData();

    private float beatTime;
    private float nextBeatTime;
    private Vector3 sphereLocation;
    private int noteNumber;
    private bool preSpawn = false;
    private GameObject newSphere;
    private GameObject newSphereTiming;
    private GameObject newSecondSphereTiming;

    private void Start()
    {
        TextAsset map = Resources.Load<TextAsset>("song1Map");
        readJsonMap(map);
        Debug.Log("lecture terminee");
        Debug.Log(songName);
        beatTime = 60f / musicInfo.bpm;
        Debug.Log(beatTime);
        nextBeatTime = Time.time + levelData.initialWaitForBeat*beatTime;
        noteNumber = 0;

        audioSource = GetComponent<AudioSource>();
        //AudioClip audioClip = Resources.Load<AudioClip>("song1_noLead");
        AudioClip audioClip = Resources.Load<AudioClip>("test");
       
        leadNotes[0] = Resources.Load<AudioClip>("sound1");
        leadNotes[1] = Resources.Load<AudioClip>("sound2");
        leadNotes[2] = Resources.Load<AudioClip>("sound3");
        leadNotes[3] = Resources.Load<AudioClip>("sound4");
        leadNotes[4] = Resources.Load<AudioClip>("sound5");


        if (audioSource != null && audioClip != null)
        {
            // Assign the AudioClip to the AudioSource
            audioSource.clip = audioClip;

            // Optionally, play the audio
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing.");
            if (audioSource == null)
            {
                Debug.LogWarning("AudioSource is missing.");
            }
            if (audioClip == null)
            {
                Debug.LogWarning("AudioClip is missing.");
            }
        }
    }

    void FixedUpdate()
    {
        if (preSpawn == false)
        {
            sphereLocation = new Vector3(levelData.keys[noteNumber].coordinates.x, levelData.keys[noteNumber].coordinates.y, levelData.keys[noteNumber].coordinates.z);
            newSphere = Instantiate(sphere, new Vector3(0,-3, 0), Quaternion.Euler(90f, 0f, 0f));
            newSphere.GetComponent<AudioSource>().clip = leadNotes[levelData.keys[noteNumber].note];
            preSpawn = true;
        }
        if (Time.time >= nextBeatTime && noteNumber <= levelData.keys.Count()-1)
        {
            newSphere.transform.position = sphereLocation;
            newSphereTiming = Instantiate(shpereTiming, sphereLocation, Quaternion.identity);
            newSecondSphereTiming = Instantiate(secondSphereTiming, new Vector3(0.0215f, 1.353f, 0.019f), Quaternion.Euler(90f, 0f, 0f));
            newSphereTiming.transform.SetParent(newSphere.transform);

            preSpawn = false;

            if (noteNumber != levelData.keys.Count()-1)
            {
                nextBeatTime += beatTime * levelData.keys[noteNumber + 1].waitForBeat;
            }

            noteNumber++;
        }
    }

    private void readJsonMap(TextAsset jsonMap)
    {
        var json = jsonMap.text;
        Root data = JsonUtility.FromJson<Root>(json);
        songName = data.name;
        version = data.version;
        musicInfo = data.music;
        levelInfo = data.level;
        levelData = data.data;
    }
}