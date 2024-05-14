using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrabType { None, Free, Snap };
[RequireComponent(typeof(Rigidbody))]

public class GrabbableBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject grabber;
    private bool wasKinematic;
    private bool isHeld = false;
    public GrabType grabType = GrabType.Free;
    private Vector3 position_snap = new Vector3(6.68f, 0.83f, -7f);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wasKinematic = rb.isKinematic;
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
                break;
            case GrabType.Snap:
                rb.isKinematic = true;
                transform.parent = grabber.transform;
                this.grabber = grabber;
                isHeld = true;
                transform.position = grabber.transform.position;
                transform.rotation = grabber.transform.rotation;
                break;
        }
    }


    public void TryRelease(GameObject grabber)
    {
        if (grabber.Equals(this.grabber) && isHeld) 
        {
            transform.parent = null;
            rb.isKinematic = wasKinematic;
            isHeld =false;
        }
    }

}
