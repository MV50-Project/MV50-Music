using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spheres = new List<GameObject>();
    [SerializeField]
    private GameObject shpereTiming;
    public float bpm;
    
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
    
    private string name = new string("None");
    private string version = new string("0.0.0");
    private MusicInfo musicInfo = new MusicInfo();
    private LevelInfo levelInfo = new LevelInfo();
    private LevelData levelData = new LevelData();

    private float beatime;
    private float nextBeatTime;
    private float probability = 1f;
    private float randomValue;
    private Vector3 randomLocation;
    private int randomSphereNumber;

    private void Start()
    {
        readJsonMap("Assets/maps/TutorialMap.json");
        beatime = 60 / bpm;
        nextBeatTime = Time.time + beatime;
    }

    void Update()
    {
        if (Time.time >= nextBeatTime)
        {
            nextBeatTime += beatime;

            randomValue = Random.Range(0f, 1f);
            if (randomValue < probability)
            {
                randomLocation = new Vector3(Random.Range(-2f, 2f), Random.Range(0.25f, 2f), 5f);
                randomSphereNumber = Random.Range(0, spheres.Count);
                GameObject newSphere = Instantiate(spheres[randomSphereNumber], randomLocation, Quaternion.Euler(90f, 0f, 0f));
                GameObject newSphereTiming = Instantiate(shpereTiming, randomLocation, Quaternion.identity);
                newSphereTiming.transform.SetParent(newSphere.transform);
            }
        }
    }

    private void readJsonMap(string path)
    {
        var json = File.ReadAllText(path);
        Root data = JsonUtility.FromJson<Root>(json);
        name = data.name;
        version = data.version;
        musicInfo = data.music;
        levelInfo = data.level;
        levelData = data.data;
    }
}

