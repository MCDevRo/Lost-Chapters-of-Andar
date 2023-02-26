using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float delayBetweenAttacks = 1f;
    [SerializeField] private bool isRanged = false;

    public float AttackRange { get => attackRange; }
    public float Damage { get => damage; }
    public float ChaseRange { get => chaseRange; }
    public float DelayBetweenAttacks { get => delayBetweenAttacks; }
    public bool IsRanged { get => isRanged; }
}