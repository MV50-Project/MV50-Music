using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    public GameObject barPrefab;
    public int numberOfBars = 64;
    public float maxScale = 10f;
    public float barSpacing = 0.2f; 
    public Material emissiveMaterial; 
    private GameObject[] bars;
    public AudioSource audioSource;

    void Start()
    {
        bars = new GameObject[numberOfBars * 2];


        for (int i = 0; i < numberOfBars; i++)
        {

            GameObject barRight = Instantiate(barPrefab, transform);
            barRight.transform.localPosition = new Vector3(i * barSpacing, 0, 0);
            barRight.GetComponent<Renderer>().material = emissiveMaterial; 
            bars[i] = barRight;


            GameObject barLeft = Instantiate(barPrefab, transform);
            barLeft.transform.localPosition = new Vector3(-i * barSpacing, 0, 0);
            barLeft.GetComponent<Renderer>().material = emissiveMaterial; 
            bars[numberOfBars + i] = barLeft;
        }
    }

    void Update()
    {

        float[] fullSpectrumData = new float[256];
        audioSource.GetSpectrumData(fullSpectrumData, 0, FFTWindow.BlackmanHarris);

        int sampleStep = Mathf.CeilToInt((float)fullSpectrumData.Length / numberOfBars);

        for (int i = 0; i < numberOfBars; i++)
        {
            float average = 0;
            for (int j = 0; j < sampleStep; j++)
            {
                int index = i * sampleStep + j;
                if (index < fullSpectrumData.Length)
                {
                    average += fullSpectrumData[index];
                }
            }
            average /= sampleStep;

            float height = Mathf.Clamp(average * maxScale * 100, 0, maxScale);

            Vector3 scaleRight = bars[i].transform.localScale;
            scaleRight.y = height;
            bars[i].transform.localScale = scaleRight;

            Vector3 scaleLeft = bars[numberOfBars + i].transform.localScale;
            scaleLeft.y = height;
            bars[numberOfBars + i].transform.localScale = scaleLeft;


            Renderer barRendererRight = bars[i].GetComponent<Renderer>();
            barRendererRight.material.color = Color.Lerp(Color.black, Color.white, height / maxScale);

            Renderer barRendererLeft = bars[numberOfBars + i].GetComponent<Renderer>();
            barRendererLeft.material.color = Color.Lerp(Color.black, Color.white, height / maxScale);
        }
    }
}
