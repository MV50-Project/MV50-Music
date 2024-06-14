using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsBehaviorRight : MonoBehaviour
{

    public Animator rightAnimatorHand;

   
    public void OnTriggerAxis(InputAction.CallbackContext context)
    {
        if (rightAnimatorHand)
        {
            rightAnimatorHand.SetFloat("Close", context.ReadValue<float>());
        }
    }
    public void OnTriggerTouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (rightAnimatorHand)
            {

                rightAnimatorHand.SetBool("Point", true);
            }
        }
        if (context.canceled)
        {
            if (rightAnimatorHand)
            {

                rightAnimatorHand.SetBool("Point", false);
            }
        }
    }

}
