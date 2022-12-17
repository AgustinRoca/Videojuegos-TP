using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingModel
{
    private string _name;
    private int _score;

    public string Name => _name;
    public int Score => _score;

    public RankingModel(string name, int score)
    {
        _name = name;
        _score = score;
    }
}
