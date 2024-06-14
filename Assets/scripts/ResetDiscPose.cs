using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDiscPose : MonoBehaviour
{
    public GameObject disc;
    private float initX;
    private float initY;
    private float initZ;
    public float resetY = -10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        initX = disc.transform.position.x;
        initY = disc.transform.position.y;
        initZ = disc.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (disc.transform.position.y < resetY)
        {
            disc.transform.position = new Vector3(initX, initY, initZ);
            disc.transform.rotation = Quaternion.identity;
        }
    }
}
