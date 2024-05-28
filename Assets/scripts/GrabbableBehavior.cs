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

    private Vector3 previousGrabberPosition;
    private Vector3 grabberVelocity;

    private float throwBoost = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wasKinematic = rb.isKinematic;
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
            rb.velocity = grabberVelocity*throwBoost;  // Apply grabber's velocity to the object
            isHeld = false;
            this.grabber = null;  // Clear the reference to the grabber
        }
    }
}
