using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private float damage = 120f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TopDownPlayerController playerController = other.GetComponent<TopDownPlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage);
                FindObjectOfType<AudioManager>().Play("IceHit");
            }
        }
    }
}
