using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    // The target to follow
    public Transform target;

    // The distance from the target
    [SerializeField]
    [Tooltip("Set the distance between player and camera")]
    private float distance = 10.0f;

    // The height of the camera
    [SerializeField]
    [Tooltip("Set the height of the camera")]
    private float height = 5.0f;

    // The rotation of the camera
    [SerializeField]
    [Tooltip("Set the angle of the camera")]
    private float rotation = 45.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        // Calculate the position of the camera
        Vector3 position = target.position;
        position -= Vector3.forward * distance;
        position.y = height;

        // Set the position of the camera
        transform.position = position;

        // Look at the target
        transform.LookAt(target);

        // Rotate the camera by the specified angle
        transform.RotateAround(target.position, Vector3.up, rotation);
    }
}
