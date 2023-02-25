using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    // public variables that can be tweaked in the Inspector
    public float damageAmount = 10f;
    public GameObject magicSlashPrefab;
    private FireSlash fireSlashCollider;
    public GameObject fireballPrefab;
    public float fireballSpeed = 20f;
    public float fireballDuration = 5f;
    public Transform fireballSpawnPoint;
    public Animator animator;
    public float fireballPushForce = 10f;
    public float fireballRange = 50f;

    // size of the box collider used to detect enemies in the player's attack range
    private Vector3 attackColliderSize = new Vector3(2f, 2f, 2f);
    

    private bool isAttacking = false;
    private bool isFiring = false;

    private void Start()
    {
        // get the MagicSlashCollider component from the MagicSlashPrefab
        fireSlashCollider = magicSlashPrefab.GetComponent<FireSlash>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            // set the isAttacking parameter to true
            animator.SetBool("isAttacking", true);
            isAttacking = true;

            // instantiate the MagicSlashPrefab and get its MagicSlashCollider component
            GameObject magicSlash = Instantiate(magicSlashPrefab, transform.position, transform.rotation);
            fireSlashCollider = magicSlash.GetComponent<FireSlash>();
        }

        // check if attack animation is finished and destroy the MagicSlashPrefab
        AnimatorStateInfo attackStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (isAttacking && attackStateInfo.IsName("Attack") && attackStateInfo.normalizedTime >= 0.5f)
        {
            if(fireSlashCollider != null)
            {
                // destroy the MagicSlashPrefab
                Destroy(fireSlashCollider.gameObject);
            }
                
            
            

            // set the isAttacking parameter to false
            animator.SetBool("isAttacking", false);
            isAttacking = false;
        }
        /*if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            // set the isAttacking parameter to true
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }*/
        else if (Input.GetMouseButtonDown(1) && !isFiring)
        {
            // set the isFiring parameter to true
            animator.SetBool("isFiring", true);
            isFiring = true;
        }

        // check if attack animation is finished and spawn the effects and deal damage
        /* AnimatorStateInfo attackStateInfo = animator.GetCurrentAnimatorStateInfo(0);
         if (isAttacking && attackStateInfo.IsName("Attack") && attackStateInfo.normalizedTime >= 0.5f)
         {
             // play the magic slash VFX
             Instantiate(magicSlashPrefab, transform.position, transform.rotation);

             // detect enemies within attack range and apply damage
             Collider[] hitColliders = Physics.OverlapBox(transform.position, attackColliderSize /2, transform.rotation);
             foreach (Collider hitCollider in hitColliders)
             {
                 EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();
                 if (enemyHealth != null)
                 {
                     // apply damage to the enemy
                     enemyHealth.TakeDamage(damageAmount);
                     Debug.Log("enemy took " + damageAmount + "damage");

                     // apply force to the enemy
                     Rigidbody enemyRigidbody = hitCollider.GetComponent<Rigidbody>();
                     if (enemyRigidbody != null)
                     {
                         enemyHealth.TakeDamage(damageAmount);
                         enemyRigidbody.AddForce(transform.forward * fireballPushForce, ForceMode.Impulse);
                     }
                 }
             }

             // set the isAttacking parameter to false
             animator.SetBool("isAttacking", false);
             isAttacking = false;
         }*/

        // check if fire animation is finished and spawn the fireball
        AnimatorStateInfo fireStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (isFiring && fireStateInfo.IsName("Fire") && fireStateInfo.normalizedTime >= 0.5f)
        {
            // spawn the fireball projectile
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, transform.rotation);

            // set the fireball's velocity to the direction of the mouse pointer
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, fireballRange))
            {
                Vector3 direction = (hit.point - fireball.transform.position).normalized;
                fireball.GetComponent<Rigidbody>().velocity = direction * fireballSpeed;
            }

            // destroy the fireball after a set duration
            Destroy(fireball, fireballDuration);

            // set the isFiring parameter to false
            animator.SetBool("isFiring", false);
            isFiring = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // draw a box to represent the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, attackColliderSize);
    }
}
