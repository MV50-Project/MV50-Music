using UnityEngine;

public class RotateAroundYAxis : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
