using UnityEngine;
using Cinemachine;

public class IsometricCamera : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    private CinemachineVirtualCamera virtualCamera;  // Reference to the virtual camera component

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

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();  // Get the virtual camera component
    }

    private void LateUpdate()
    {
        virtualCamera.Follow = player;  // Set the target of the virtual camera to be the player's transform
        virtualCamera.LookAt = player;  // Set the LookAt target of the virtual camera to be the player's transform

        // Set the camera's rotation and position to give an isometric view
        //Calculate the position of the camera
        Vector3 position = player.position;
        position -= Vector3.forward * distance;
        position.y = height;

        // Set the position of the camera
        transform.position = position;

        // Look at the target
        transform.LookAt(player);

        // Rotate the camera by the specified angle
        transform.RotateAround(player.position, Vector3.up, rotation);
    }
}