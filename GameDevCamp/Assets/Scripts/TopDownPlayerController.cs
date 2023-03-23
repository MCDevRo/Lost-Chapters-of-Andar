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

    //The speed at which the player rotates
    [SerializeField]
    public float rotationSpeed = 180f;

    // The animator component
    private Animator animator;

    // Dodge variables
    [SerializeField]
    private float dodgeDuration = 0.5f;
    [SerializeField]
    private float dodgeCooldown = 1f;
    [SerializeField]
    private float dodgeRange = 5f;
    [SerializeField]
    private GameObject fireTrailPrefab;
    private bool isDodging = false;
    private bool isDead = false;


    // Player mesh transform
    private Transform playerMesh;
    private Transform playerTrail;

    [SerializeField]
    private LayerMask dodgeRaycastLayerMask;

    // Current movement direction
    private Vector3 currentDirection = Vector3.zero;

    //Ui Reference
    public UIManager healthBar;
    public float deathAnimationDuration = 3f;

    private void Start()
    {
        // Get the animator component
        animator = GetComponent<Animator>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
       



        // Assign the player mesh transform
        playerMesh = transform.Find("Modular_Characters");
        playerTrail = transform.Find("Trail");
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            return;
        }
        // Get the joystick's horizontal and vertical axes
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

        // Update the current direction based on the player's movement
        if (direction != Vector3.zero)
        {
            currentDirection = direction;
        }

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

        // Dodge input
        if (Input.GetKeyDown(KeyCode.Q) && !isDodging)
        {
            StartCoroutine(Dodge());
        }
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

        healthBar.SetHealth(health);

        Debug.Log(health);
        if (health <= 0)
        {
            isDead = true;
            Die();
        }

        // Make sure the player's health does not go below 0
        health = Mathf.Max(health, 0);
    }
    public void Die()
    {
        animator.SetTrigger("Die");
        Debug.Log("Player is Dead!");
        this.enabled = false;

        StartCoroutine(ShowGameOverScreenAfterDeathAnimation());

    }

    IEnumerator Dodge()
    {
        // Set the dodging flag
        isDodging = true;

        // Calculate the direction in which the player should dodge
        Vector3 dodgeDirection = currentDirection.normalized * dodgeRange;

        // Perform a raycast in the dodge direction
        RaycastHit hit;
        bool raycastHit = Physics.Raycast(transform.position, dodgeDirection.normalized, out hit, dodgeRange, dodgeRaycastLayerMask);

        if (!raycastHit)
        {
            // Disable the player mesh child object
            playerMesh.gameObject.SetActive(false);

            // Instantiate the firetrail VFX prefab at the player's position
            playerTrail.gameObject.SetActive(true);

            // Move the player to the new position
            transform.position += dodgeDirection;

            // Wait for the dodge duration
            yield return new WaitForSeconds(dodgeDuration);

            // Destroy the fire trail VFX
            playerTrail.gameObject.SetActive(false);

            // Enable the player mesh child object
            playerMesh.gameObject.SetActive(true);
        }

        // Wait for the dodge cooldown
        yield return new WaitForSeconds(dodgeCooldown);

        // Reset the dodging flag
        isDodging = false;
    }
    IEnumerator ShowGameOverScreenAfterDeathAnimation()
    {
        // Wait for the duration of the death animation
        yield return new WaitForSeconds(deathAnimationDuration);

        // Show the game over screen
        healthBar.ShowGameOverScreen();
    }
}