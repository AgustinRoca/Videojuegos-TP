using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    IGun Owner {get;}
    float Speed { get; }
    Rigidbody Rigidbody {get;}
    Collider Collider {get;}

    void Travel();
    void OnTriggerEnter(Collider collider);


    void SetOwner(IGun gun);
}
