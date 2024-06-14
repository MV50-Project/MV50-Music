using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsBehaviorLeft : MonoBehaviour
{

    public Animator leftAnimatorHand;


   

    public void OnLeftTriggerAxis(InputAction.CallbackContext context)
    {
        if (leftAnimatorHand)
        {
            leftAnimatorHand.SetFloat("Close", context.ReadValue<float>());
        }
    }
    public void OnLeftTriggerTouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (leftAnimatorHand)
            {
                leftAnimatorHand.SetBool("Point", false);
            }
        }
        if (context.canceled)
        {
            if (leftAnimatorHand)
            {
                leftAnimatorHand.SetBool("Point", true);
            }
        }
    }




}
