using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SoundEffectController))]
[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour, IGun
{
    #region GUN_PROPERTIES

    [SerializeField] private GunStats _stats;
    private SoundEffectController _soundFxController;
    [SerializeField] private AudioClip _shotClip;
    public float timeStamp = 0.0f;
    private Sprite _sprite;

    
    #endregion

    #region I_GUN_PROPERTIES
    public float LifeTime => _stats.LifeTime;
    public int Damage => _stats.Damage;
    public AudioClip ShotClip => _shotClip;
    public SoundEffectController SoundFXController => _soundFxController;
    public GameObject BulletPrefab =>  _stats.BulletPrefab;
    public float ShotCooldown => _stats.ShotCooldown;
    public Sprite Sprite => _sprite;
    #endregion

    public virtual void setStats(GunStats stats){
        _stats = stats;
    }

    public virtual void setShotClip(AudioClip clip){
        _shotClip = clip;
    }

    public virtual void setSprite(Sprite sprite){
        _sprite = sprite;
    }

    void Start()
    {
        _soundFxController = GetComponent<SoundEffectController>();
    }

    #region I_GUN_PROPERTIES
    public virtual void Attack()
    {
        if (timeStamp <= Time.time){
            timeStamp = Time.time + ShotCooldown;
            var bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            bullet.name = "Bullet";
            bullet.GetComponent<Bullet>().SetOwner(this);
            _soundFxController.PlayOneShot(_shotClip, 0.5F);
        }
    }

    #endregion
}
