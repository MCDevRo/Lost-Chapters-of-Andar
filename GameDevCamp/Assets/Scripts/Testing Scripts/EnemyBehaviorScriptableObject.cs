using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyBehavior")]
public class EnemyBehaviorScriptableObject : ScriptableObject
{
    // Variables for waypoints
    public GameObject[] waypoints;
    public float waypointRadius = 0.1f;
    public float waypointDwellTime = 1.0f;

    // Variables for chasing and attacking player
    public float chaseRadius = 5.0f;
    public float attackRadius = 1.0f;

    // Variables for animations
    public AnimationClip roamAnimation;
    public AnimationClip chaseAnimation;
    public AnimationClip attackAnimation;
    public AnimationClip idleAnimation;
}

