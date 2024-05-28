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
    private List<GameObject> spheres = new List<GameObject>();
    [SerializeField]
    private GameObject shpereTiming;

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

    private void Start()
    {
        readJsonMap("Assets/maps/TutorialMap.json");
        Debug.Log("lecture terminee");
        Debug.Log(songName);

        beatTime = 60f / musicInfo.bpm;
        Debug.Log(beatTime);
        nextBeatTime = Time.time + levelData.initialWaitForBeat*beatTime;
        noteNumber = 0;
;
    }

    void Update()
    {
        if (Time.time >= nextBeatTime && noteNumber <= levelData.keys.Count()-1)
        {
            sphereLocation = new Vector3(levelData.keys[noteNumber].coordinates.x, levelData.keys[noteNumber].coordinates.y, levelData.keys[noteNumber].coordinates.z);
            GameObject newSphere = Instantiate(spheres[levelData.keys[noteNumber].note], sphereLocation, Quaternion.Euler(90f, 0f, 0f));
            GameObject newSphereTiming = Instantiate(shpereTiming, sphereLocation, Quaternion.identity);
            newSphereTiming.transform.SetParent(newSphere.transform);

            if (noteNumber != levelData.keys.Count()-1)
            {
                nextBeatTime += beatTime * levelData.keys[noteNumber + 1].waitForBeat;
            }

            noteNumber++;
        }
    }

    private void readJsonMap(string path)
    {
        var json = File.ReadAllText(path);
        Root data = JsonUtility.FromJson<Root>(json);
        songName = data.name;
        version = data.version;
        musicInfo = data.music;
        levelInfo = data.level;
        levelData = data.data;
    }
}