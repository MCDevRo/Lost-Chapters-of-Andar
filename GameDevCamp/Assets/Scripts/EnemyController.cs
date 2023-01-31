using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //The animator component
    private Animator animator;

    // The speed at which the enemy moves
    public float moveSpeed = 2.0f;

    public int damage = 2;

    // The player game object
    public GameObject player;

    // The player's health script
    private TopDownPlayerController playerHealth;

    // The player's maximum health
    public int maxHealth = 100;

    // The player's current health
    public int health;

    // The distance at which the enemy will attack the player
    public float attackRange = 0.3f;

    // The interval between attacks
    public float attackInterval = 10.0f;

    //private bool isAttacking = false;

    private bool attackCoroutineRunning = false;

    void Start()
    {
        // Set the player game object
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<TopDownPlayerController>();
        health = maxHealth;

        // Get the animator component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the direction to the player
        Vector3 direction = player.transform.position - transform.position;

        // Normalize the direction
        direction.Normalize();

        float distance = Vector3.Distance(player.transform.position,transform.position);

        // Check if the enemy is within attack range
        if (distance <= attackRange && !attackCoroutineRunning)
        {
            attackCoroutineRunning = true;
            // Attack the player
            StartCoroutine(WaitForAttack());
            
            Debug.Log("Player took damage current health is " + playerHealth.health);
        }
        else
        {
            if(distance > attackRange)
            {
                // If the player is no longer in the attack range, stop the attack animation
                animator.SetBool("IsAttacking", false);
                //isAttacking = false;

                // Set the animator's "IsRunning" parameter based on the player's movement
                animator.SetBool("IsRunning", distance > attackRange);

                // Rotate the enemy to face the player
                transform.rotation = Quaternion.LookRotation(direction);
                // Move the enemy in the direction
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
            
        }
    }

    // A coroutine to wait for the attack interval
    IEnumerator WaitForAttack()
    {
        // Set the animator's "IsAttacking" parameter
        animator.SetBool("IsAttacking", true);
        //isAttacking = true;

        yield return new WaitForSeconds(attackInterval);

        // Deal damage to the player
        playerHealth.TakeDamage(damage);

        // Reset the animator's "IsAttacking" parameter
        animator.SetBool("IsAttacking", false);

        attackCoroutineRunning = false;
    }

    //A function to deal damage to the enemy
    public void TakeDamage(int amount)
    {
        // Decrement the player's health by the specified amount
        health -= amount;

        // Make sure the player's health does not go below 0
        health = Mathf.Max(health, 0);
    }
}
