using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Enemy enemyData;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float attackDelay = 1f;

    public NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform playerTransform;
    private bool isAttacking;
    private float attackCooldownTimer;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = enemyData.AttackRange * 0.9f;
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
                StartCoroutine(Attack());
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else if (distanceToPlayer <= enemyData.ChaseRange)
        {
            navMeshAgent.SetDestination(playerTransform.position);
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false); // Set isAttacking to false
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        attackCooldownTimer -= Time.deltaTime;
    }

    private IEnumerator Attack()
    {
        if (isAttacking) yield break; // Prevent multiple simultaneous instances of the coroutine

        isAttacking = true;
        animator.SetBool("isAttacking", true); // Set isAttacking to true

        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
        animator.SetBool("isAttacking", false); // Set isAttacking to false
    }
    public void DealDamage()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= enemyData.AttackRange)
        {
            playerTransform.GetComponent<TopDownPlayerController>().TakeDamage(enemyData.Damage);
            attackCooldownTimer = enemyData.DelayBetweenAttacks;
        }
    }
}