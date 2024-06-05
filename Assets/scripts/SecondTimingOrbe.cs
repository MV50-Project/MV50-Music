using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTimingOrbe : MonoBehaviour
{

    private Vector3 finalPos = new Vector3(0f, 19.19f, 44.15f);
    private Vector3 startPos;
    public float beatTime;
    public float duration;
    private float elapsedTime = 0f;

    void Start()
    {
        beatTime = 60f / 105f;
        duration = beatTime * 2f;
        startPos = this.gameObject.transform.position;
    }

    void FixedUpdate()
    {
        if (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, finalPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
               
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
