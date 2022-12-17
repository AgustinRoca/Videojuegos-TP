using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/Enemy", order = 0)]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private EnemyStatValues _stats;
    public int MaxHp => _stats.MaxHp;
    public float MovementSpeed => _stats.MovementSpeed;
    public float RotationSpeed => _stats.RotationSpeed;
    public float AttackRange => _stats.AttackRange;
    public float TouchRange => _stats.TouchRange;
}

[System.Serializable]
public struct EnemyStatValues
{
    public int MaxHp;
    public float MovementSpeed;
    public float RotationSpeed;
    public float AttackRange;
    public float TouchRange;
}
