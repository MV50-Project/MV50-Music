using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class TeleporterBehavior : MonoBehaviour
{

    private bool canTeleport = false;
    private bool pointerVisible = false;
    private Vector3 destinationPoint;
    private LineRenderer lineRenderer;
    public float maxDistance;
    public GameObject player;
    public string floorTag;
    public string floor_2Tag;
    public Material materialOk;
    public Material materialNok;

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
        pointerVisible = false;

    }

    void ShowPointer()
    {
        if (lineRenderer)
        {
            lineRenderer.enabled = true;
        }
        pointerVisible = true;

    }

    private void Teleport()
    {
        if (player)
        {

            player.transform.position = destinationPoint;
        }
    }


    public void OnTeleportAction(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            ShowPointer();
        }
        if (context.canceled)
        {
            if (canTeleport)
            {
                Teleport();
            }
            HidePointer() ;
        }
    }

    private void FixedUpdate()
    {
        if (pointerVisible)
        {
            lineRenderer.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance))
            {
                if (hit.collider.gameObject.CompareTag(floorTag))
                {
                    canTeleport = true;
                    destinationPoint = hit.point;
                    lineRenderer.material = materialOk;
                }
                else if (hit.collider.gameObject.CompareTag(floor_2Tag))
                {
                    canTeleport = true;
                    Vector3 teleport_point = new Vector3(6.1f, player.transform.position.y, -6.7f) ;
                    destinationPoint = teleport_point;
                    lineRenderer.material = materialOk;
                }
                else
                {
                    canTeleport = false;
                    lineRenderer.material = materialNok;
                    

                }
                

                lineRenderer.SetPosition(1, transform.position + transform.forward * hit.distance);

            }
            else
            {

                lineRenderer.material = materialNok;

                lineRenderer.SetPosition(1, transform.position + transform.forward * maxDistance);
            }

        }


    }

}
