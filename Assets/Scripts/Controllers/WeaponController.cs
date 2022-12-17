using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Actor))]

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Text _text;
    private List<Gun> _guns = new List<Gun>();
    private Gun _currentGun;
    private CmdAttack _cmdAttack;
    [SerializeField] private Image HUDImage;

    public void addWeapon(GameObject gunGO, WeaponFactory.eWeapon gun){
        _guns.Add(WeaponFactory.createWeapon(gunGO, gun));
        changeWeapon(_guns.Count - 1);
    }

    public void changeWeapon(int index)
    {
        if(0 <= index && index < _guns.Count){
            foreach(Gun gun in _guns) {
                gun.gameObject.SetActive(false);
            }
            _currentGun = _guns[index];
            _currentGun.gameObject.SetActive(true);
            _cmdAttack = new CmdAttack(_currentGun);
            changeWeaponText(index);
        }
    }

    public void attack(){
        EventQueueManager.instance.AddCommand(_cmdAttack);
    }

    public void changeWeaponText(int index){      
        string s = (index + 1).ToString() + " of " + _guns.Count.ToString();
        _text.text = s;
        HUDImage.sprite = _guns[index].Sprite;
    }
}
