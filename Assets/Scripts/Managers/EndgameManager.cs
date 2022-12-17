using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameManager : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Button _rankingButton;

    void Start()
    {
        _text.text = "Game over";

        string s = "s";
        int score = GlobalData.instance.Score;
        if (score <= 0)
        {
            _rankingButton.interactable = false;
        }

        if (score == 1)
        {
            s = "";
        }
        
        ((Text)GameObject.Find("Points").GetComponent<Text>()).text = $"You got {score} point{s}";
    }

}
