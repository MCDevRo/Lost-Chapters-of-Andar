using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementManager : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2.0f;
    public bool loop = true;

    private int currentWaypoint = 0;

    void Start()
    {
        transform.position = waypoints[0].position;
    }

    void Update()
    {
        if (currentWaypoint < waypoints.Length)
        {
            Move();
        }
        else
        {
            if (loop)
            {
                currentWaypoint = 0;
            }
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);

        if (transform.position == waypoints[currentWaypoint].position)
        {
            currentWaypoint++;
        }
    }
}
