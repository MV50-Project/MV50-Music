using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLightVisualizer : MonoBehaviour
{
    public Light directionalLight;  
    public int numberOfSamples = 64; 
    public float maxIntensity = 10f;  
    public AudioSource audioSource; 
    public float smoothingSpeed = 2f;  

    private float currentIntensity = 0f;  

    void Update()
    {

        float[] spectrumData = new float[256];


        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);


        int sampleStep = Mathf.CeilToInt((float)spectrumData.Length / numberOfSamples);

        float totalAverage = 0;


        for (int i = 0; i < numberOfSamples; i++)
        {
            float average = 0;
            for (int j = 0; j < sampleStep; j++)
            {
                int index = i * sampleStep + j;
                if (index < spectrumData.Length)
                {
                    average += spectrumData[index];
                }
            }
            average /= sampleStep;
            totalAverage += average;
        }


        totalAverage /= numberOfSamples;


        float targetIntensity = Mathf.Clamp(totalAverage * maxIntensity * 100, 0, maxIntensity);


        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, smoothingSpeed * Time.deltaTime);


        directionalLight.intensity = currentIntensity;
    }
}