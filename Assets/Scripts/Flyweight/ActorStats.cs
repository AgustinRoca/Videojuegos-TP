using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/Actor", order = 0)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private ActorStatValues _stats;
    public int MaxHp => _stats.MaxHp;
    public float MovementSpeed => _stats.MovementSpeed;
    public float RotationSpeed => _stats.RotationSpeed;
}

[System.Serializable]
public struct ActorStatValues
{
    public int MaxHp;
    public float MovementSpeed;
    public float RotationSpeed;
}