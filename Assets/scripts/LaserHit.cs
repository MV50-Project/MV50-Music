using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserHit : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float maxDistance;
    private bool shoot = false;
    private bool hasShot = false;
    public GameObject laserToShow;
    public GameObject particleEffect;

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
                        GameObject particle = Instantiate(particleEffect, hit.rigidbody.gameObject.transform.position, Quaternion.identity);
                        sphereHit.transform.position = new Vector3(0, -2f, 0);
                        soundHit.Play();
                        
                        StartCoroutine(DestroyObjectAfterDelay(sphereHit, 1f));
                        StartCoroutine(DestroyObjectAfterDelay(particle, 1f));
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
