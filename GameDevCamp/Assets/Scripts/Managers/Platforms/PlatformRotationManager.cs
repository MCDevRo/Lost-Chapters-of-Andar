using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotationManager : MonoBehaviour
{
    public float rotationSpeed = 90.0f;
    public float rotationAngle = 180.0f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        if (transform.eulerAngles.y >= rotationAngle)
        {
            rotationSpeed = -rotationSpeed;
        }
        else if (transform.eulerAngles.y <= 0)
        {
            rotationSpeed = -rotationSpeed;
        }
    }
}
