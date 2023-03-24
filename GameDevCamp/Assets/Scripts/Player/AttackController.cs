using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform fireballSpawnPoint;
    [SerializeField] private GameObject fireSlashVFX;
    [SerializeField] private float fireballSpeed = 20f;
    [SerializeField] private float fireballDuration = 5f;
    [SerializeField] private float fireballRange = 50f;

    private bool isAttacking = false;
    private bool isFiring = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking && !isFiring)
        {
            // Perform fire slash attack
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }

        if (Input.GetMouseButtonDown(1) && !isFiring && !isAttacking)
        {
            // Perform fire projectile attack
            animator.SetBool("isFiring", true);
            isFiring = true;
        }
        else if (Input.GetMouseButtonUp(1) && isFiring)
        {
            // Stop firing
            animator.SetBool("isFiring", false);
            isFiring = false;
        }

        if (!isAttacking && !isFiring)
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("isFiring", false);
        }
    }

    void AttackStarted()
    {
        // Instantiate FireSlashVFX prefab at the player's position and rotation
        Instantiate(fireSlashVFX, transform.position, transform.rotation);
    }

    void AttackFinished()
    {
        // Transition out of fire slash attack animation
        isAttacking = false;
    }

    void FireFinished()
    {
        // Transition out of fire projectile attack animation
        isFiring = false;
    }

    void SpawnFireball()
    {
        // Instantiate the fireball prefab at the spawn point
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        // Set the fireball's velocity in the direction the player is facing
        Rigidbody fireballRb = fireball.GetComponent<Rigidbody>();
        Vector3 direction = transform.forward * fireballRange;
        fireballRb.AddForce(direction.normalized * fireballSpeed, ForceMode.Impulse);

        // Destroy the fireball after a set duration
        Destroy(fireball, fireballDuration);
    }
}