using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public float fireSlashDamage = 20f;
    public GameObject impactVFXPrefab;
    public GameObject shieldVFXPrefab; // reference to the shield VFX prefab to be spawned

    public int numPillarsCharged = 0;
    private bool shieldActive = true;
    private GameObject shieldVFXInstance; // reference to the shield VFX instance

    void Start()
    {
        currentHealth = maxHealth;
        ActivateShield();
    }
    void ActivateShield()
    {
        // Instantiate the shield VFX prefab and play it at the boss's position
        if (shieldVFXPrefab != null)
        {
            shieldVFXInstance = Instantiate(shieldVFXPrefab, transform.position + new Vector3(0,0.4f,0), transform.rotation, transform);
        }

        // Set the shieldActive flag to true
        shieldActive = true;
    }

    public void TakeDamage(float damage)
    {
        if (shieldActive)
        {
            return; // shield is active, damage is not applied
        }

        currentHealth -= damage;
        Debug.Log("Boss has : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (impactVFXPrefab != null)
        {
            // Instantiate the impact VFX prefab and play it at the boss's position
            Instantiate(impactVFXPrefab, new Vector3(transform.position.x, 1.6f, transform.position.z), transform.rotation);
        }
    }

    public void PillarCharged()
    {
        numPillarsCharged++;

        if (numPillarsCharged >= 4)
        {
            shieldActive = false;

            // Destroy the shield VFX instance
            if (shieldVFXInstance != null)
            {
                Destroy(shieldVFXInstance);
            }
            Debug.Log("BossHealth PillarCharged() called. numPillarsCharged = " + numPillarsCharged + ", shieldActive = " + shieldActive);
        }
    }

    public bool IsShieldActive()
    {
        return shieldActive;
    }


    void Die()
    {
        Instantiate(impactVFXPrefab, new Vector3(transform.position.x, 1.6f, transform.position.z), transform.rotation);
        Destroy(gameObject);
    }

}