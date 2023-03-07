using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Enemy enemyData;
    [SerializeField] private string playerTag = "Player";
    //[SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackDelay = 0.5f;

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
                StartCoroutine(Attack());
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

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(attackDelay);

        if (Vector3.Distance(transform.position, playerTransform.position) <= enemyData.AttackRange)
        {
            playerTransform.GetComponent<TopDownPlayerController>().TakeDamage(enemyData.Damage);
            attackCooldownTimer = enemyData.DelayBetweenAttacks;
        }

        isAttacking = false;
    }
}