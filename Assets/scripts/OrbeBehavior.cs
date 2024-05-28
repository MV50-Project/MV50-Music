using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbeBehavior : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("floor")) 
        {
            Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
            
        }
    }

}
