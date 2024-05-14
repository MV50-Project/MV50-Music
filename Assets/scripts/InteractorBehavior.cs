using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractorBehavior : MonoBehaviour
{

    Dictionary<string, GameObject> overlappingGameObjects = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> overlappingTriggers = new Dictionary<string, GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        GrabbableBehavior gb = other.GetComponentInParent<GrabbableBehavior>();
        if (gb)
        {
            overlappingGameObjects.Add(gb.gameObject.name, gb.gameObject);
        }
        TriggerBehavior tb = other.GetComponentInParent<TriggerBehavior>();
        if (tb)
        {
            overlappingTriggers.Add(tb.gameObject.name, tb.gameObject);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        GrabbableBehavior gb = other.GetComponentInParent<GrabbableBehavior>();
        if (gb)
        {
            overlappingGameObjects.Remove(gb.gameObject.name);
        }
        TriggerBehavior tb = other.GetComponentInParent<TriggerBehavior>();
        if (tb)
        {
            overlappingTriggers.Remove(tb.gameObject.name);
        }

    }
    public void OnTriggerAction(InputAction.CallbackContext context)
    {
        GameObject nearestTrigger = GetNearestTrigger();
        if (nearestTrigger)
        {
            if (context.started)
            {
                nearestTrigger.GetComponent<TriggerBehavior>().Trigger();
            }
        }
    }
    private GameObject GetNearestTrigger()
    {
        GameObject nearestTrigger = null;
        float minDistance = Mathf.Infinity;
        foreach (KeyValuePair<string, GameObject> kvp in overlappingTriggers)
        {
            float distance = Vector3.Distance(transform.position, kvp.Value.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTrigger = kvp.Value;
            }
        }
        return nearestTrigger;
    }

    private GameObject GetNearestGrabbable()
    {
        GameObject nearestGrabbable = null;
        float minDistance = Mathf.Infinity;

        foreach (KeyValuePair<string, GameObject> kvp in overlappingGameObjects)
        {
            if (kvp.Value.GetComponent<GrabbableBehavior>())
            {
                float distance = Vector3.Distance(transform.position, kvp.Value.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestGrabbable = kvp.Value;
                }
            }
        }
        return nearestGrabbable;
    }


    public void OnGrabAction(InputAction.CallbackContext context)
    {
        GameObject nearestGrabbable = GetNearestGrabbable();
        if (nearestGrabbable)
        {
            if (context.started)
            {
                nearestGrabbable.GetComponent<GrabbableBehavior>().TryGrab(gameObject);
            }
            if (context.canceled)
            {
                nearestGrabbable.GetComponent<GrabbableBehavior>().TryRelease(gameObject);
            }
        }
    }


}
