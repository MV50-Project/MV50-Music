using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;

public enum GrabType { None, Free, Snap };
[RequireComponent(typeof(Rigidbody))]

public class GrabbableBehavior : MonoBehaviour
{

    private AudioSource audioSource;
    public GameObject menuToggle;
    public GameObject scoreboardToggle;
    public TMP_Text scoreboardText;
    private Rigidbody rb;
    private GameObject grabber;
    public GameObject gunToggle;
    private bool wasKinematic;
    private bool isHeld = false;
    public GrabType grabType = GrabType.Free;
    private Vector3 position_snap = new Vector3(6.68f, 0.83f, -7f);
    

    private Vector3 previousGrabberPosition;
    private Vector3 grabberVelocity;

    private float throwBoost = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wasKinematic = rb.isKinematic;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (isHeld && grabber != null)
        {
            grabberVelocity = (grabber.transform.position - previousGrabberPosition) / Time.fixedDeltaTime;
            previousGrabberPosition = grabber.transform.position;
        }
    }

    public void TryGrab(GameObject grabber)
    {
        switch (grabType)
        {
            case GrabType.None:
                break;
            case GrabType.Free:
                rb.isKinematic = true;
                transform.parent = grabber.transform;
                this.grabber = grabber;
                isHeld = true;
                previousGrabberPosition = grabber.transform.position;
                break;
            case GrabType.Snap:
                rb.isKinematic = true;
                transform.parent = grabber.transform;
                this.grabber = grabber;
                isHeld = true;
                transform.position = grabber.transform.position;
                transform.rotation = grabber.transform.rotation;
                previousGrabberPosition = grabber.transform.position;
                break;
        }
    }

    public void TryRelease(GameObject grabber)
    {
        if (grabber.Equals(this.grabber) && isHeld)
        {
            transform.parent = null;
            rb.isKinematic = wasKinematic;
            rb.velocity = grabberVelocity * throwBoost;  // Apply grabber's velocity to the object
            isHeld = false;
            this.grabber = null;  // Clear the reference to the grabber
        }
    }

    private void showScoreboard(string mapName)
    {
        
        string scoreboardPath = "scoreboardFinal" + mapName + ".txt";
        string fullPath = Path.Combine(Application.persistentDataPath, scoreboardPath);
        Debug.Log(fullPath);
        if (!File.Exists(fullPath))
        {
            using (StreamWriter writer = File.CreateText(fullPath))
            {
                writer.WriteLine("10000");
                writer.WriteLine("8000");
                writer.WriteLine("6000");
                writer.WriteLine("4000");
                writer.WriteLine("2000");
            }
        }
        string[] lines = File.ReadAllLines(fullPath);

        int[] numbers = lines.Select(int.Parse).ToArray();

        int[] sortedValues = numbers.OrderByDescending(num => num).ToArray();

        string resultText = "";
        for (int i = 0; i < Mathf.Min(5, sortedValues.Length); i++)
        {
            resultText += sortedValues[i] + "\n";
        }

        scoreboardText.text = resultText;


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tourne_disque"))
        {
            if (!menuToggle.activeInHierarchy)
            {
                menuToggle.SetActive(true);
                scoreboardToggle.SetActive(true);
                gunToggle.SetActive(false);
            }
            audioSource.Play();
            gameObject.transform.position = new Vector3(-0.219f, 0.8516f, 0.7786f);
            gameObject.transform.rotation = Quaternion.identity;
            if (grabber != null) // Check if grabber is not null
            {
                TryRelease(grabber);
            }
            grabType = GrabType.None;
            PlayerPrefs.SetString("EntryMethod", "song1Map");
            showScoreboard("song1Map");

            //SceneManager.LoadScene("SampleScene");
        }
    }



}



