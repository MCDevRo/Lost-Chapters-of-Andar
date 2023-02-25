using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlash : MonoBehaviour
{
    public float damageAmount = 10f;

    void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // apply damage to the enemy
            enemyHealth.TakeDamage(damageAmount);
            Debug.Log("enemy took " + damageAmount + " damage");
        }
    }
}
