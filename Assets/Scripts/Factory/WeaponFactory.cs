using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class WeaponFactory
{
    public enum eWeapon {None, Pistol, Shotgun, Machinegun};

    public static Gun createWeapon(GameObject gunGO, eWeapon weapon){
        Gun gun = null;
        GameObject newGun = null;
        
        switch (weapon)
        {
            case eWeapon.Pistol:
                newGun = new GameObject("Pistol");
                gun = newGun.AddComponent<Pistol>();
                gun.setStats(Resources.Load<GunStats>("Stats/Pistol"));
                gun.setShotClip(Resources.Load<AudioClip>("Audio/Shot"));
                Sprite sprite = Resources.Load<Sprite>("GunsSprites/pistol");
                gun.setSprite(sprite);
                break;
            case eWeapon.Shotgun:
                newGun = new GameObject("Shotgun");
                gun = newGun.AddComponent<Shotgun>();
                gun.setStats(Resources.Load<GunStats>("Stats/Shotgun"));
                gun.setShotClip(Resources.Load<AudioClip>("Audio/Shotgun Shot"));
                gun.setSprite(Resources.Load<Sprite>("GunsSprites/shotgun"));
                break;
            case eWeapon.Machinegun:
                newGun = new GameObject("Machinegun");
                gun = newGun.AddComponent<Pistol>();
                gun.setStats(Resources.Load<GunStats>("Stats/Machinegun"));
                gun.setShotClip(Resources.Load<AudioClip>("Audio/Shot"));
                gun.setSprite(Resources.Load<Sprite>("GunsSprites/machinegun"));
                break;
            default:
                Debug.Log($"{weapon} not recongnized as a weapon in enum eWeapon");
                break;
        }
        newGun.transform.parent = gunGO.transform;
        newGun.transform.localPosition = new Vector3(0, 0, 0);
        newGun.transform.localRotation = Quaternion.identity; 
        newGun.GetComponent<SoundEffectController>().setAudioSource(newGun.GetComponent<AudioSource>());
        return gun;
    }

}
