using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDiscPose : MonoBehaviour
{
    public GameObject disc;
    private Transform initTransform;
    public float resetY = -10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        initTransform = disc.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (disc.transform.position.y < resetY)
        {
            disc.transform.position = initTransform.position;
            disc.transform.rotation = initTransform.rotation;
        }
    }
}
