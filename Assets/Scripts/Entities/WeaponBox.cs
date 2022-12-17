using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider),typeof(Rigidbody))]
public class WeaponBox : MonoBehaviour
{
    #region PRIVATE_PROPERTEIS
    [SerializeField] private WeaponFactory.eWeapon _owner;
    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _raycastRange = 7f;
    [SerializeField] private float _rigidbodyDrag = 7f;
    private bool _isGrounded = false;
    #endregion

    #region I_BULLET_PROPERTIES
    public WeaponFactory.eWeapon Owner => _owner;    
    public Collider Collider => _collider;
    public Rigidbody Rigidbody => _rigidbody;
    #endregion
    private Rigidbody rb;

    public void SetOwner(WeaponFactory.eWeapon owner){
        _owner = owner;
    }

    private void Start(){
        rb = GetComponent<Rigidbody>();
    }

    private void Update(){
        if(!_isGrounded){
            RaycastHit hit;
            Debug.DrawRay(transform.position,Vector3.down);
            Ray ray = new Ray(transform.position, Vector3.down);

            if(Physics.Raycast(ray,out hit,_raycastRange)){
                if(hit.transform.tag.Equals("Ground")){
                    _isGrounded = true;
                    if(rb != null) rb.drag = _rigidbodyDrag;

                }
            }
        }
    }

    #region I_BULLET_METHODS
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){ //Me fijo si es character..
            Character c = other.GetComponent<Character>();

            c.addWeapon(_owner);
            Destroy(this.gameObject);
        }
    
    }
    #endregion
}