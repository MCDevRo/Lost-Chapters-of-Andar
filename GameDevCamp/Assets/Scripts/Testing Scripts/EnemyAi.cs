using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private EnemyAiData aiData;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float idleDuration;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;
    private bool isRoaming;
    private bool isChasing;
    private bool isAttacking;
    private float lastAttackTime;
    private int currentWaypointIndex;
    private float lastIdleTime;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isRoaming = true;
        isChasing = false;
        isAttacking = false;
        lastAttackTime = -aiData.attackDelay;
        currentWaypointIndex = 0;
        lastIdleTime = -idleDuration;
    }

    private void Update()
    {
        if (isRoaming)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (Time.time - lastIdleTime > idleDuration)
                {
                    animator.SetBool("IsIdle", true);
                    animator.SetBool("IsWalking", false);
                    lastIdleTime = Time.time;
                }
                else
                {
                    agent.SetDestination(waypoints[currentWaypointIndex].position);
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsIdle", false);
                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                }
            }
            CheckPlayerInRange();
        }
        else if (isChasing)
        {
            agent.SetDestination(player.position);
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdle", false);
            CheckPlayerInRange();
        }
        else if (isAttacking)
        {
            if (Time.time - lastAttackTime > aiData.attackDelay)
            {
                animator.SetBool("IsAttacking", true);
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsIdle", false);
                TopDownPlayerController playerController = player.GetComponent<TopDownPlayerController>();
                if (playerController != null)
                {
                    playerController.TakeDamage(aiData.damage);
                }
                lastAttackTime = Time.time;
                Debug.Log("Player took " + aiData.damage);
            }
            CheckPlayerInRange();
        }
    }
    private void CheckPlayerInRange()
    {
        if (Vector3.Distance(transform.position, player.position) <= aiData.attackRange)
        {
            isRoaming = false;
            isChasing = false;
            isAttacking = true;
            agent.isStopped = true;
        }
        else if (Vector3.Distance(transform.position, player.position) <= aiData.chaseRange)
        {
            isRoaming = false;
            isChasing = true;
            isAttacking = false;
            agent.isStopped = false;
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdle", false);
        }
        else
        {
            isRoaming = true;
            isChasing = false;
            isAttacking = false;
            agent.isStopped = false;
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsAttacking", false);
        }
    }
}