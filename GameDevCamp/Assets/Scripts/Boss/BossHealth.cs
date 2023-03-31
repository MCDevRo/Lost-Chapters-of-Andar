using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 250f;
    public float currentHealth;
    public float fireSlashDamage = 20f;
    public GameObject impactVFXPrefab;
    public GameObject shieldVFXPrefab; // reference to the shield VFX prefab to be spawned

    public int numPillarsCharged = 0;
    private bool shieldActive = true;
    private GameObject shieldVFXInstance; // reference to the shield VFX instance
    private bool isDead = false;
    private Animator animator;
    private EnemyAI enemyAI;
    public UIManager healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        ActivateShield();
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
        healthBar.BossSetMaxHealth(maxHealth);
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
        healthBar.BossSetHealth(currentHealth);
        Debug.Log("Boss has : " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
        else if (impactVFXPrefab != null)
        {
            // Instantiate the impact VFX prefab and play it at the boss's position
            Instantiate(impactVFXPrefab, new Vector3(transform.position.x, 1.6f, transform.position.z), transform.rotation);
            FindObjectOfType<AudioManager>().Play("FireHit");
            FindObjectOfType<AudioManager>().Play("BossHit");
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
        enemyAI.navMeshAgent.enabled = false;
        enemyAI.enabled = false;       
        this.enabled = false;
        FindObjectOfType<AudioManager>().Play("FireHit");
        FindObjectOfType<AudioManager>().Play("BossDie");
        animator.SetTrigger("Die");
        Destroy(gameObject,2);
        
        
    }

}