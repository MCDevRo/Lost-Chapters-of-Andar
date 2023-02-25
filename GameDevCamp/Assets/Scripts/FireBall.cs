using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float damageAmount = 50f;
    public float fireballPushForce = 10f;

    void OnTriggerEnter(Collider other)
    {
        // check if the collided object has an EnemyHealth component
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        Rigidbody enemyRigidbody = other.GetComponent<Rigidbody>();
        if (enemyRigidbody != null)
        {
            enemyRigidbody.AddForce(transform.forward * fireballPushForce, ForceMode.Impulse);
        }
        if (enemyHealth != null)
        {
            // deal damage to the enemy
            enemyHealth.TakeDamage(damageAmount);
            Destroy(this.gameObject);
        }
    }
}
