using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    static public EventManager instance;

    #region UNITY_EVENTS
    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
    #endregion

    #region GAME_MANAGER
    public event Action<bool> OnGameOver;

    public void EventGameOver(bool hasWon) 
    {
        if (OnGameOver != null) OnGameOver(hasWon);
    }

    public event Action<bool> OnChangeBackgroundMusic;

    public void ChangeBackgroundMusic(bool boss) 
    {
        if (OnChangeBackgroundMusic != null) OnChangeBackgroundMusic(boss);
    }
    #endregion

    public event Action<int> OnScoreChange;

    public event Action<WeaponFactory.eWeapon> OnWeaponSpawn;
    public void WeaponSpawn(WeaponFactory.eWeapon weapon){
        if(OnWeaponSpawn != null) OnWeaponSpawn(weapon);
    }
    
    public void ScoreChange(int score)
    {
        if (OnScoreChange != null) OnScoreChange(score);
    }

    public event Action<bool> OnEnemyTouchedPlayer;

    public void EnemyTouchedPlayer()
    {
        if (OnEnemyTouchedPlayer != null) OnEnemyTouchedPlayer(true);
    }

}