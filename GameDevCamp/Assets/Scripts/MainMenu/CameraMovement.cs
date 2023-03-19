using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject cameraPath;
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 2.0f;

    private Transform[] waypoints;
    private int currentWaypointIndex;
    private int waypointDirection = 1;

    void Start()
    {
        waypoints = cameraPath.GetComponentsInChildren<Transform>();
        currentWaypointIndex = 1;
    }

    void Update()
    {
        MoveCamera();
        RotateCamera();
    }

    void MoveCamera()
    {
        if (currentWaypointIndex >= 1 && currentWaypointIndex < waypoints.Length)
        {
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, step);

            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex += waypointDirection;

                if (currentWaypointIndex == waypoints.Length - 1 || currentWaypointIndex == 1)
                {
                    waypointDirection *= -1;
                }
            }
        }
    }

    void RotateCamera()
    {
        if (currentWaypointIndex >= 1 && currentWaypointIndex < waypoints.Length)
        {
            Quaternion targetRotation = Quaternion.LookRotation(waypoints[currentWaypointIndex].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}