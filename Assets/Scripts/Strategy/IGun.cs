using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun
{
    GameObject BulletPrefab { get; }
    int Damage { get; }
    float LifeTime {get;}
    void Attack();
}
