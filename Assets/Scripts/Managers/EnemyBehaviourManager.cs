using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourManager : MonoBehaviour
{
    private Enemy[] _enemies;
    private Boss[] _bosses;
    
    void Start()
    {
        EventManager.instance.OnEnemyTouchedPlayer += OnEnemyTouchedPlayer;
    }

    public void OnEnemyTouchedPlayer(bool touchedPlayer)
    {
        _enemies = FindObjectsOfType<Enemy>();
        _bosses = FindObjectsOfType<Boss>();
        foreach (var enemy in _enemies)
        {
            enemy.LeavePlayerAloneForAMoment();
        }

        foreach (var boss in _bosses)
        {
            boss.LeavePlayerAloneForAMoment();
        }
    }
}
