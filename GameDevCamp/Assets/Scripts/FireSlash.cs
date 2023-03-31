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

        SecondAreaEnemyHealth gardenEnemyHealth = other.GetComponent<SecondAreaEnemyHealth>();


        if (gardenEnemyHealth != null)
        {
            gardenEnemyHealth.TakeDamage(damageAmount);
        }

        if (enemyHealth != null)
        {
            // apply damage to the enemy
            enemyHealth.TakeDamage(damageAmount);
            Debug.Log("enemy took " + damageAmount + " damage");
        }
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damageAmount);
            //Destroy(gameObject);
        }
        else if (bossHealth != null)
        {
            bossHealth.PillarCharged();
            //Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
    }
}
