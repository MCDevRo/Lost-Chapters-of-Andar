using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecondAreaEnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public float vfxOffset = 1f;

    private float currentHealth;

    public GameObject impactVFXPrefab;

    public SpawnManager spawnManager;

    public UIManager uiManager; // Reference to the UIManager script

    private Animator animator;

    private EnemyAI enemyAI;

    private bool isDead = false;

   



    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
        
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        //Debug.Log(currentHealth);
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
        else if (impactVFXPrefab != null)
        {
            // Instantiate the impact VFX prefab and play it at the enemy's position
            Instantiate(impactVFXPrefab, new Vector3(transform.position.x, transform.position.y + vfxOffset, transform.position.z), transform.rotation);
            FindObjectOfType<AudioManager>().Play("FireHit");
        }
    }

    void Die()
    {

        Instantiate(impactVFXPrefab, new Vector3(transform.position.x, transform.position.y + vfxOffset, transform.position.z), transform.rotation);
                
        
        this.enabled = false;

        FindObjectOfType<AudioManager>().Play("SkeletonDie");
        animator.SetTrigger("Die");
        Destroy(gameObject,3);


        // Call the UIManager's IncrementEnemiesDefeated method to update the UI
        uiManager.IncrementEnemiesDefeated();
    }
}
