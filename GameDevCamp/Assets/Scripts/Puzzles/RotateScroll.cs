using UnityEngine;

public class RotateScroll : MonoBehaviour
{
    public int scrollID = 1;
    public float rotationSpeed = 30.0f;

    void Update()
    {
        float rotationStep = rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotationStep, 0, Space.Self);
    }
}
