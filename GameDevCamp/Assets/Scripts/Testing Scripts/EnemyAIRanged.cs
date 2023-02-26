using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIRanged : MonoBehaviour
{
    [SerializeField] private Enemy enemyData;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackCooldown = 1f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform playerTransform;
    private bool isAttacking;
    private float attackCooldownTimer;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= enemyData.AttackRange)
        {
            if (!isAttacking && attackCooldownTimer <= 0)
            {
                transform.LookAt(playerTransform);
                StartAttack();
            }
        }
        else if (distanceToPlayer <= enemyData.ChaseRange)
        {
            navMeshAgent.SetDestination(playerTransform.position);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        attackCooldownTimer -= Time.deltaTime;
       
    }

    private void StartAttack()
    {
        
        isAttacking = true;
        animator.SetTrigger("attack");
    }

    public void SpawnProjectile()
    {
        // Get the center point of the player's collider
        Vector3 playerCenter = playerTransform.GetComponent<Collider>().bounds.center;

        // Calculate the direction towards the center of the player's collider
        Vector3 directionToCenter = (playerCenter - projectileSpawnPoint.position).normalized;

        // Create a new projectile at the spawn point and set its direction
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        projectile.GetComponent<IceProjectile>().SetDirection(directionToCenter);

        attackCooldownTimer = attackCooldown;
        isAttacking = false;
    }
}