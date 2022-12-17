using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int _shotCount = 5;

    public override void setStats(GunStats stats) => base.setStats(stats);

    public override void Attack()
    {
        if (timeStamp <= Time.time){
            timeStamp = Time.time + ShotCooldown;
            for (int i = 0; i < _shotCount; i++)
            {
                var gameObject = Instantiate(
                    BulletPrefab,
                    transform.position+ Random.insideUnitSphere * 2,
                    transform.rotation);
                gameObject.name= "Shotgun Bullet";
                ShotgunBullet shotgunBullet = gameObject.AddComponent<ShotgunBullet>();
                shotgunBullet.SetOwner(this);
                Bullet bullet = gameObject.GetComponent<Bullet>();
                shotgunBullet.setTargets(bullet.LayerTarget);
                Destroy(bullet);
                

            }
            SoundFXController.PlayOneShot(ShotClip, 0.5F);
        }
    }

}
