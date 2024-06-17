using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LaserHit : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float maxDistance;
    private bool shoot = false;
    private bool hasShot = false;
    public GameObject laserToShow;
    public GameObject particleEffect;
    public int score = 0;
    public TMP_Text PistolScoreText;
    public TMP_Text feedbackText;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        HidePointer();

    }

    void HidePointer()
    {
        if (lineRenderer)
        {
            lineRenderer.enabled = false;
        }
        shoot = false;
        

    }

    void ShowPointer()
    {
        if (lineRenderer)
        {
            lineRenderer.enabled = true;
        }
        shoot = true;

    }

    public void OnShootAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShowPointer();

            hasShot = false;

        }
        if (context.canceled)
        {
            HidePointer();

        }

    }




    private void FixedUpdate()
    {

        lineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance))
        {


            lineRenderer.SetPosition(1, transform.position + transform.forward * hit.distance);



        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + transform.forward * maxDistance);


        }
        if (shoot)
        {
            if (!hasShot)
            {
                if (hit.rigidbody)
                {
                    GameObject sphereHit = hit.rigidbody.gameObject;
                    if (sphereHit.CompareTag("cible"))
                    {
                        AudioSource soundHit = hit.rigidbody.gameObject.GetComponent<AudioSource>();
                        soundHit.Play();
                        TMP_Text instanceFeedbackText = Instantiate(feedbackText, new Vector3(sphereHit.transform.position.x, sphereHit.transform.position.y, sphereHit.transform.position.z), Quaternion.identity); 
                        GameObject sphereTiming = sphereHit.transform.GetChild(1).gameObject;
                        float scaleScore = sphereTiming.transform.localScale.x;

                        if(scaleScore >= 1.9f && scaleScore <= 2.1f)
                        {
                            score += 100;
                            instanceFeedbackText.text = "Parfait";
                            instanceFeedbackText.color = new Color(0f, 0.9f, 1f, 1f);
                        }
                        else if(scaleScore >= 1.7f && scaleScore <= 2.3f)
                        {
                            score += 75;
                            instanceFeedbackText.text = "Bien";
                            instanceFeedbackText.color = new Color(1.643161f, 0.08525834f, 1.341241f, 1f);
                        }
                        else if(scaleScore >= 1.3f && scaleScore <= 2.7f)
                        {
                            score += 50;
                            instanceFeedbackText.text = "OK";
                            instanceFeedbackText.color = new Color(1.498039f, 1.081746f, 0.07843135f, 1f);
                        }
                        else //if(scaleScore >= 1.2 && scaleScore <= 2.8)
                        {
                            score += 25;
                            instanceFeedbackText.text = "Mauvais";
                            instanceFeedbackText.color = new Color(1.498039f, 0.02996079f, 0.05992157f, 1f);
                        }
                       
                        PistolScoreText.text = ""+score;


                        GameObject particle = Instantiate(particleEffect, hit.rigidbody.gameObject.transform.position, Quaternion.identity);

                        sphereHit.transform.position = new Vector3(0, -2f, 0);

                        GameObject tmpTextObject = instanceFeedbackText.gameObject;

                        StartCoroutine(DestroyObjectAfterDelay(sphereHit, 2f));
                        StartCoroutine(DestroyObjectAfterDelay(particle, 1f));
                        StartCoroutine(DestroyObjectAfterDelay(tmpTextObject, 1f));
                        
                    }



                }
                GameObject laser = Instantiate(laserToShow);
                LineRenderer renderLaser = laser.GetComponent<LineRenderer>();
                renderLaser.SetPosition(0, lineRenderer.GetPosition(0));
                renderLaser.SetPosition(1, lineRenderer.GetPosition(1));
                StartCoroutine(DestroyObjectAfterDelay(laser, 0.1f));
                hasShot = true;


            }
            
            
        }






    }

    IEnumerator DestroyObjectAfterDelay(GameObject gameObject, float time)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(time);

        // Destroy the object after the delay
        Destroy(gameObject);
    }


}
