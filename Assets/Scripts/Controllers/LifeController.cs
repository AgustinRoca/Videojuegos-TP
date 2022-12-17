using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Actor))]

public class LifeController : MonoBehaviour, IDamageable
{
    #region I_DAMAGABLE_PROPERTIES


    public int Hp => _hp;
    [SerializeField] private int _hp;
    private bool dead = false;
    private Animator animator;

    private int _maxHp;
    public int MaxHp
    {
        get
        {
            if(GetComponent<Actor>().ActorStats != null) {
                return GetComponent<Actor>().ActorStats.MaxHp;
            } else if(GetComponent<Enemy>() != null) {
                return GetComponent<Enemy>().EnemyStats.MaxHp;
            } else {
                return GetComponent<Boss>().EnemyStats.MaxHp;
            }
        }
        set { _maxHp = value; }
    }
    #endregion

    #region UNITY_EVENTS
    void Start()
    {
        _hp = MaxHp;
    }
    #endregion

    #region I_DAMAGABLE_METHODS
    public void TakeDamage(int damage)
    {
        if(GetComponent<Boss>() == null || FindObjectsOfType<Enemy>().Length == 0){
            _hp -= damage;
            if (IsDead()) Die();
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private bool IsDead() => _hp <= 0;

    public void Die() 
    { 
        if (tag == "Enemy" && !dead) {
            if(GetComponent<Enemy>() != null){
                GetComponent<Enemy>().Stop();
                GetComponent<Enemy>().animator.SetBool("Dead", true);
                EventManager.instance.ScoreChange(1);
                dead = true;
            } else {
                GetComponent<Boss>().Stop();
                GetComponent<Boss>().animator.SetBool("Dead", true);
                EventManager.instance.ScoreChange(5);
                dead = true;
                GetComponent<Boss>()?.DebuffZombies();
                if(FindObjectsOfType<Boss>().Length == 1){
                    EventManager.instance.ChangeBackgroundMusic(false);
                }
            }
        }
        Destroy(this.gameObject, 1.5f);
    }
    #endregion
}
