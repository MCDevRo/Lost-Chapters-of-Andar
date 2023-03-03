using UnityEngine;

public class MouseLookController : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 100f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player body based on the mouse movement
        playerBody.Rotate(Vector3.up * mouseX);
        playerBody.rotation *= Quaternion.Euler(-mouseY, 0f, 0f);

        // Clamp the rotation along the x-axis to prevent flipping
        float xRotation = playerBody.rotation.eulerAngles.x;
        if (xRotation > 180)
        {
            xRotation -= 360;
        }
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerBody.rotation = Quaternion.Euler(xRotation, playerBody.rotation.eulerAngles.y, playerBody.rotation.eulerAngles.z);
    }
}