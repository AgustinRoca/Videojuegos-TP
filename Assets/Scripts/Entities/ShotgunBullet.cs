using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet {

    public override void OnTriggerEnter(Collider collider)
    {
        if(LayerTarget.Contains(collider.gameObject.layer)){ //Me fijo si es destruible..
            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(Owner.Damage);
            if(collider.GetComponent<Enemy>() != null){
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.Push(transform.forward, 50);
            } else {
                Boss enemy = collider.GetComponent<Boss>();
                enemy.Push(transform.forward, 50);
            }
            Destroy(this.gameObject);
        }
    
    }
}