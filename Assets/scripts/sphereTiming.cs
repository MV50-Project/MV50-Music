using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sphereTiming : MonoBehaviour
{
    public float bpm;
    public float duration; 
    private Vector3 scale;
    private float elapsedTime = 0f;
    private Vector3 targetScale = new Vector3(0.4f, 0.4f, 0.4f);

    void Start()
    {
        duration = (60f / bpm) * 3 - ((60f / bpm) *0.3f);// == needs to be played in 3 beats
        scale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;


        float fraction = elapsedTime / duration;

        if (fraction < 1f)
        {
            transform.localScale = Vector3.Lerp(scale, targetScale, fraction);
        }
        else
        {
            transform.localScale = targetScale;
            StartCoroutine(DestroyObjectAfterDelay(transform.parent.gameObject, gameObject, 60f/bpm));


        }
    }


    IEnumerator DestroyObjectAfterDelay(GameObject gameObject1, GameObject gameObject2, float timeToDestroy)
    {

        transform.parent.gameObject.transform.position = new Vector3(0, -2f, 0);
        gameObject.transform.position = new Vector3(0, -2f, 0);
        yield return new WaitForSeconds(timeToDestroy);

        Destroy(gameObject1);
        Destroy(gameObject2);
    }

}
