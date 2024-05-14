using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))]
public class TriggerBehavior : MonoBehaviour
{
    public UnityEvent onTriggerEvents;


    // Update is called once per frame
    public void Trigger()
    {
        onTriggerEvents.Invoke();
    }


}
