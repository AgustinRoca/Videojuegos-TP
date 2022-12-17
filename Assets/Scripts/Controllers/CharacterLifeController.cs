using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(SoundEffectController))]
public class CharacterLifeController : MonoBehaviour
{
    #region CHARACTER_SPECIFIC_PROPERTIES

    [SerializeField] private GameObject _characterLifePrefab;
    [SerializeField] private Transform _characterLivesGridTransform;
    private List<Object> _characterLifeElements = new List<Object>();
    [SerializeField] private AudioClip _hurtSoundClip;
    [SerializeField] private SoundEffectController _soundFxController;

    #endregion
    #region I_DAMAGABLE_PROPERTIES

    public bool invincibilityActivated;
    public int Hp => _hp;
    [SerializeField] private int _hp;

    public int MaxHp => GetComponent<Actor>().ActorStats.MaxHp;
    #endregion

    #region UNITY_EVENTS
    void Start()
    {
        _soundFxController = GetComponent<SoundEffectController>();
        invincibilityActivated = false;
        _hp = MaxHp;
        for (int i = 0; i < _hp; i++)
        {
            var element = Instantiate(_characterLifePrefab, _characterLivesGridTransform);
            _characterLifeElements.Add(element);
        }
    }
    #endregion

    public void TakeDamage(int damage)
    {
        if (!invincibilityActivated)
        {
            _hp -= damage;
            CharacterTakeDamage();
            if (IsDead()) Die();
        }
    }

    public void ActivateOrDeactivateInvincibility()
    {
        invincibilityActivated = !invincibilityActivated;
    }

    private bool IsDead() => _hp <= 0;

    public void Die() 
    { 
        Destroy(this.gameObject); 
        EventManager.instance.EventGameOver(false);
    }

    private void CharacterTakeDamage()
    {
        var heartObject = GameObject.Find("Life Heart");
        Destroy(heartObject);

        foreach (var element in _characterLifeElements)
        {
            Destroy(element);
        }
        _characterLifeElements = new List<Object>();
		
        for (int i = 0; i < _hp; i++)
        {
            var element = Instantiate(_characterLifePrefab, _characterLivesGridTransform);
            _characterLifeElements.Add(element);
        }
        _soundFxController.PlayOneShot(_hurtSoundClip);
    }
}
