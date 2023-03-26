using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public float vfxOffset = 1f;

    private float currentHealth;

    public GameObject impactVFXPrefab;

    private bool isDead = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            isDead = true;
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
        animator.SetTrigger("Die");
        Destroy(gameObject,2);
    }
}
