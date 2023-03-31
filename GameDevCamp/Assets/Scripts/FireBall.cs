using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float damageAmount = 50f;
    public float fireballPushForce = 10f;
    public GameObject impactVFXPrefab;

    void OnTriggerEnter(Collider other)
    {
        // check if the collided object has an EnemyHealth component
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        //Rigidbody enemyRigidbody = other.GetComponent<Rigidbody>();
        SecondAreaEnemyHealth gardenEnemyHealth = other.GetComponent<SecondAreaEnemyHealth>();
      

        BossHealth bossHealth = other.GetComponent<BossHealth>();

        /*if (enemyRigidbody != null)
        {
            enemyRigidbody.AddForce(transform.forward * fireballPushForce, ForceMode.Impulse);
        }*/
        if(gardenEnemyHealth != null)
        {
            gardenEnemyHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Brazier"))
        {
            Braziers brazier = other.gameObject.GetComponent<Braziers>();
            if (brazier != null)
            {
                brazier.LightBrazier();
                BrazierManager.Instance.IncrementBraziersLit();
            }
            Destroy(gameObject);
        }
        if (enemyHealth != null)
        {
            // deal damage to the enemy
            enemyHealth.TakeDamage(damageAmount);
            Destroy(this.gameObject);
        }
        if(bossHealth != null)
        {
            bossHealth.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
        else if (bossHealth != null)
        {
            bossHealth.PillarCharged();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if(other.gameObject != null)
        {
            FindObjectOfType<AudioManager>().Play("FireHit");
            Instantiate(impactVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}

