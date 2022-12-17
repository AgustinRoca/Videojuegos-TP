using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingElement : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _scoreText;
    
    public void SetModel(string name, int score)
    {
        _nameText.text = name;
        _scoreText.text = score.ToString();
    }
}
