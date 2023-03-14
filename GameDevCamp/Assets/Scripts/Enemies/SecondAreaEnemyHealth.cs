using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondAreaEnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public float vfxOffset = 1f;

    private float currentHealth;

    public GameObject impactVFXPrefab;

    TaskManager manager;

    void Start()
    {
        manager = GetComponent<TaskManager>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //Debug.Log(currentHealth);
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else if (impactVFXPrefab != null)
        {
            // Instantiate the impact VFX prefab and play it at the enemy's position
            Instantiate(impactVFXPrefab, new Vector3(transform.position.x, transform.position.y + vfxOffset, transform.position.z), transform.rotation);
        }
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        if (manager != null)
        {
            // Call the IncrementEnemiesDefeated function on the TaskManager script
            manager.IncrementEnemiesDefeated();
        }

        Instantiate(impactVFXPrefab, new Vector3(transform.position.x, transform.position.y + vfxOffset, transform.position.z), transform.rotation);
        Destroy(gameObject);
        
    }
}
