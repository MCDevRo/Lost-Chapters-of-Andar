using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public GameObject objectToMove;
    public Vector3 moveDirection;

    void OnTriggerEnter(Collider other) 
    {
        objectToMove.transform.position += moveDirection;
    }
}
