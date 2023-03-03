using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarTrigger : MonoBehaviour
{
    public PillarController pillarController;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("PillarTrigger OnTriggerEnter");
        if (other.CompareTag("PlayerProjectile"))
        {
            pillarController.TakeShot();
        }
    }
}
