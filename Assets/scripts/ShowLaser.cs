using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class ShowLaser : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public float maxDistance;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

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

        


    }

    
}
