using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunStats", menuName = "Stats/Gun", order = 0)]
public class GunStats : ScriptableObject
{
    [SerializeField] private GunStatValues _stats;
    public int Damage => _stats.Damage;
    public float LifeTime => _stats.LifeTime;
    public GameObject BulletPrefab => _stats.BulletPrefab;

    public float ShotCooldown => _stats.ShotCooldown;

}

[System.Serializable]
public struct GunStatValues
{
    public float LifeTime;
    public int Damage;
    public GameObject BulletPrefab;
    public  float RotationSpeed;
    public float ShotCooldown;
}