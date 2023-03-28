using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform fireballSpawnPoint;
    [SerializeField] private GameObject fireSlashVFX;
    [SerializeField] private float fireballSpeed = 20f;
    [SerializeField] private float fireballDuration = 5f;
    [SerializeField] private float fireballRange = 50f;
    private TopDownPlayerController player;

    private bool isAttacking = false;
    private bool isFiring = false;

    private void Start()
    {
        player = GetComponent<TopDownPlayerController>();
    }

    void Update()
    {
        if (player.health <= 0)
        {
            this.enabled = false;
            return;
        }

        if (!isAttacking && !isFiring)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Perform fire slash attack
                animator.SetBool("isAttacking", true);
                isAttacking = true;
                FindObjectOfType<AudioManager>().Play("FireSlash");
            }
            else if (Input.GetMouseButton(1))
            {
                // Perform fire projectile attack
                animator.SetBool("isFiring", true);
                isFiring = true;
                FindObjectOfType<AudioManager>().Play("FireCast");
            }
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
        animator.SetBool("isAttacking", false);
    }

    void FireFinished()
    {
        // Transition out of fire projectile attack animation
        isFiring = false;
        animator.SetBool("isFiring", false);
    }

    void SpawnFireball()
    {
        // Instantiate the fireball prefab at the spawn point
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
        player = GetComponent<TopDownPlayerController>();

        // set the fireball's velocity to the direction of the mouse pointer
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, fireballRange))
        {
            Vector3 direction = (hit.point - fireball.transform.position).normalized;
            fireball.GetComponent<Rigidbody>().velocity = direction * fireballSpeed;

            // rotate the player to face the direction they're shooting
            Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
            if (lookDirection != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDirection);
                player.transform.rotation = rotation;
            }
        }

        // Destroy the fireball after a set duration
        Destroy(fireball, fireballDuration);
    }
}