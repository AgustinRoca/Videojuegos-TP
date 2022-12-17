using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider),typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IBullet
{
    #region PRIVATE_PROPERTEIS
    [SerializeField] private IGun _owner;
    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _speed = 80;
    private float _lifetime;

    [SerializeField] private List<int> _layerTarget; //Esto es para saber con quien tiene que interactuar
    #endregion

    #region I_BULLET_PROPERTIES
    public IGun Owner => _owner;
    public float Speed => _speed;
    public float LifeTime => _owner.LifeTime;
    public List<int> LayerTarget => _layerTarget;
    
    public Collider Collider => _collider;
    public Rigidbody Rigidbody => _rigidbody;
    #endregion

    #region I_BULLET_METHODS
    public void Travel() => transform.Translate(Vector3.forward * Time.deltaTime * _speed);

    public virtual void OnTriggerEnter(Collider collider)
    {
        if(_layerTarget.Contains(collider.gameObject.layer)){ //Me fijo si es destruible..
            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(_owner.Damage);
            Destroy(this.gameObject);
        }
    
    }

    #endregion

    #region UNITY_EVENTS
    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;

        _lifetime = LifeTime;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative; 
     }

    private void Update()
    {
        Travel();

        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0) Destroy(this.gameObject);
    }
    #endregion


    public void SetOwner(IGun owner) => _owner = owner;

    public void setTargets(List<int> layerTarget) => _layerTarget = layerTarget;
}