using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spheres = new List<GameObject>();
    [SerializeField]
    private GameObject shpereTiming;
    public float bpm;

    private float beatime;
    private float nextBeatTime;
    private float probability = 1f;
    private float randomValue;
    private Vector3 randomLocation;
    private int randomSphereNumber;

    private void Start()
    {
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
}

