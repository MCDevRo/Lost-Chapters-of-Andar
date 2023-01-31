using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    // The target that the camera should follow
    public Transform target;

    // The distance between the camera and the target
    public float distance = 10.0f;

    // The height of the camera above the target
    public float height = 5.0f;

    // The smoothness of the camera's movement
    public float smoothness = 5.0f;

    // The minimum and maximum pitch (rotation around the x axis) of the camera
    public float minPitch = -30.0f;
    public float maxPitch = 30.0f;

    // The sensitivity of the mouse input for camera rotation
    public float mouseSensitivity = 2.0f;

    // The current pitch of the camera
    //private float pitch = 0.0f;

    void Update()
    {
        // Update the pitch of the camera based on mouse input
        //pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        //pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    void LateUpdate()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = target.position + Vector3.up * height - Vector3.forward * distance;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothness);

        // Look at the target's position, but ignore its rotation
        transform.LookAt(target.position, Vector3.up);

        // Rotate the camera around the x axis to adjust the pitch
       // transform.Rotate(pitch, 0, 0, Space.Self);
    }
}
