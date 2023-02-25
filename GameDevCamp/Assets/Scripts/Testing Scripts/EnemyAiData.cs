using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Enemy AI", menuName = "Enemy AI")]
public class EnemyAiData : ScriptableObject
{
    public float chaseRange = 20f;
    public float attackRange = 2f;
    public float damage = 10f;
    public float attackDelay = 1f;
    //public AnimationClip walkAnimation;
    //public AnimationClip runAnimation;
    //public AnimationClip attackAnimation;
    //public AnimationClip idleAnimation;
}

