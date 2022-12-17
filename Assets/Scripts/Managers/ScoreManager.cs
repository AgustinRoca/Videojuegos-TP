using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _text;
    
    void Start()
    {
        _text.text = "0";
        EventManager.instance.OnScoreChange += OnScoreChange;

    }

    public void OnScoreChange(int scoreToAdd)
    {
        int newScore;
        var currentScore = int.TryParse(_text.text, out newScore);
        newScore = newScore + scoreToAdd;
        GlobalData.instance.SetScoreField(newScore);
        if(newScore == 1) {
            EventManager.instance.WeaponSpawn(WeaponFactory.eWeapon.Shotgun);
        } else if (newScore == 4){
            EventManager.instance.WeaponSpawn(WeaponFactory.eWeapon.Machinegun);
        }
        _text.text = newScore.ToString();
    }
}
