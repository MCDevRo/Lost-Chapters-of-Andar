using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public float vfxOffset = 1f;

    private float currentHealth;

    public GameObject impactVFXPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else if (impactVFXPrefab != null)
        {
            // Instantiate the impact VFX prefab and play it at the enemy's position
            Instantiate(impactVFXPrefab, new Vector3(transform.position.x,transform.position.y + vfxOffset,transform.position.z), transform.rotation);
        }
    }

    void Die()
    {
        Instantiate(impactVFXPrefab, new Vector3(transform.position.x, transform.position.y + vfxOffset, transform.position.z), transform.rotation);
        Destroy(gameObject);
    }
}
