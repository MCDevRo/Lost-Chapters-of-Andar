using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damageAmount = 10f;
    public GameObject magicSlashPrefab;
    public GameObject fireballPrefab;
    public float fireballSpeed = 20f;
    public float fireballDuration = 5f;
    public Transform fireballSpawnPoint;
    public Animator animator;

    private Vector3 attackColliderSize = new Vector3(1f, 1f, 1f);

    private bool isAttacking = false;
    private bool isFiring = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            // set the isAttacking parameter to true
            animator.SetBool("isAttacking", true);
            isAttacking = true;

            // play the magic slash VFX
            Instantiate(magicSlashPrefab, transform.position, transform.rotation);

            // detect enemies within attack range and apply damage
            //Collider[] hitColliders = Physics.OverlapBox(transform.position, attackColliderSize / 2f, transform.rotation);
            /*foreach (Collider hitCollider in hitColliders)
            {
                EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // apply damage to the enemy
                    enemyHealth.TakeDamage(damageAmount);
                }
            }*/

        }
        else if (Input.GetMouseButtonDown(1) && !isFiring)
        {
            // set the isFiring parameter to true
            animator.SetBool("isFiring", true);
            isFiring = true;

            // spawn the fireball projectile
            GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, transform.rotation);

            // set the fireball's velocity
            Rigidbody fireballRigidbody = fireball.GetComponent<Rigidbody>();
            fireballRigidbody.velocity = transform.forward * fireballSpeed;

            // destroy the fireball after a set duration
            Destroy(fireball, fireballDuration);
        }

        // set the isAttacking and isFiring parameters to false when the attack animations are complete
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Base Layer.Attack") && stateInfo.normalizedTime >= 1f)
        {
            animator.SetBool("isAttacking", false);
            isAttacking = false;
            Debug.Log("Attack animation complete. isAttacking set to false.");
        }
        else if (stateInfo.IsName("Base Layer.Fire") && stateInfo.normalizedTime >= 1f)
        {
            animator.SetBool("isFiring", false);
            isFiring = false;
            Debug.Log("Fire animation complete. isFiring set to false.");
        }

        // check if the attack and fire animations were interrupted
        if (!isAttacking && animator.GetBool("isAttacking"))
        {
            animator.SetBool("isAttacking", false);
        }
        if (!isFiring && animator.GetBool("isFiring"))
        {
            animator.SetBool("isFiring", false);
        }
    }

    void OnDrawGizmosSelected()
    {
        // draw a box to represent the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, attackColliderSize);
    }
}
