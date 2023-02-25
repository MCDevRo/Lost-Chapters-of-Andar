using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent navMeshAgent;
    public WaypointContainer waypointContainer;
    public float waypointRadius = 0.1f;
    public float waypointDwellTime = 1.0f;
    public float chaseRadius = 5.0f;
    public float attackRadius = 1.0f;
    public AnimationClip roamAnimation;
    public AnimationClip chaseAnimation;
    public AnimationClip attackAnimation;
    public AnimationClip idleAnimation;
    private int currentWaypoint = 0;
    private bool roaming = true;
    private bool attacking = false;
    private Animation animationComponent;

    private void Start()
    {
        animationComponent = GetComponent<Animation>();
        animationComponent.clip = roamAnimation;
        animationComponent.Play();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within the chase radius, start chasing
        if (distanceToPlayer <= chaseRadius && !attacking)
        {
            roaming = false;
            StartCoroutine(ChasePlayer());
        }

        // If the player is outside the chase radius and the enemy is not attacking, return to roaming
        if (distanceToPlayer > chaseRadius && !attacking)
        {
            roaming = true;
            animationComponent.clip = roamAnimation;
            animationComponent.Play();
        }

        // If the player is within the attack radius, start attacking
        if (distanceToPlayer <= attackRadius && !attacking)
        {
            attacking = true;
            StartCoroutine(AttackPlayer());
        }

        // If the enemy is roaming, move to the next waypoint
        if (roaming)
        {
            MoveToWaypoint();
        }
        else if (!attacking)
        {
            animationComponent.clip = idleAnimation;
            animationComponent.Play();
        }
    }

    private IEnumerator ChasePlayer()
    {
        animationComponent.clip = chaseAnimation;
        animationComponent.Play();
        while (Vector3.Distance(transform.position, player.position) > attackRadius)
        {
            navMeshAgent.SetDestination(player.position);
            yield return null;
        }
    }

    private IEnumerator AttackPlayer()
    {
        animationComponent.clip = attackAnimation;
        animationComponent.Play();
        yield return new WaitForSeconds(attackAnimation.length);
        attacking = false;
    }

    private void MoveToWaypoint()
    {
        Transform target = waypointContainer.waypoints[currentWaypoint];
        navMeshAgent.SetDestination(target.position);
        if (Vector3.Distance(transform.position, target.position) < waypointRadius)
        {
            StartCoroutine(DwellAtWaypoint());
        }
    }

    private IEnumerator DwellAtWaypoint()
    {
        roaming = false;
        yield return new WaitForSeconds(waypointDwellTime);
        currentWaypoint = (currentWaypoint + 1) % waypointContainer.waypoints.Length;
        roaming = true;
    }
}

[System.Serializable]
public class WaypointContainer
{
    public Transform[] waypoints;
}