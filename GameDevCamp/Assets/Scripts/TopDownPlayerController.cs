using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{

    // The player's maximum health
    [SerializeField]
    private float maxHealth = 100;

    // The player's current health
    [SerializeField]
    public float health;

    // The speed at which the player moves
    [SerializeField]
    public float moveSpeed = 5.0f;

    // Joystick UI 
    //public bl_Joystick Joystick;

    //The speed at which the player rotates
    [SerializeField]
    public float rotationSpeed = 180f;


    // The animator component
    private Animator animator;

    public static object Instance { get; internal set; }

    private void Start()
    {
        // Get the animator component
        animator = GetComponent<Animator>();
        health = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the joystick's horizontal and vertical axes
        //float horizontal = Joystick.Horizontal;
        //float vertical = Joystick.Vertical;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get the horizontal and vertical axes from the keyboard
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontal = Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            vertical = Input.GetAxis("Vertical");
        }

        // Calculate the direction in which the player should move
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        // Normalize the direction vector
        direction = direction.normalized;

        // Set the animator's "Movement" parameter based on the player's movement
        animator.SetFloat("Movement", direction.magnitude);

        // Rotate the player to face the direction of movement
        if (direction != Vector3.zero)
        {
            // Calculate the rotation needed to face the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move the player in the desired direction
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // A function to add health to the player
    public void AddHealth(float amount)
    {
        // Increment the player's health by the specified amount
        health += amount;

        // Make sure the player's health does not exceed the maximum
        health = Mathf.Min(health, maxHealth);

    }

    // A function to deal damage to the player
    public void TakeDamage(float amount)
    {
        // Decrement the player's health by the specified amount
        health -= amount;
        Debug.Log(health);
        if(health <= 0)
        {
            Die();
        }

        // Make sure the player's health does not go below 0
        health = Mathf.Max(health, 0);
    }

    public void Die()
    {
        Debug.Log("Player is Dead!");
    }
}