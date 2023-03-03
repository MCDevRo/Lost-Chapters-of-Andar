using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlash : MonoBehaviour
{
    public float damageAmount = 10f;

    void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        BossHealth bossHealth = other.GetComponent<BossHealth>();

        if (enemyHealth != null)
        {
            // apply damage to the enemy
            enemyHealth.TakeDamage(damageAmount);
            Debug.Log("enemy took " + damageAmount + " damage");
        }
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damageAmount);
        }
        else if (bossHealth != null)
        {
            bossHealth.PillarCharged();
        }
    }
}
