using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    static public GlobalData instance;

    public bool IsVictory => _isVictory;
    [SerializeField] private bool _isVictory;

    public int Score => _score;
    [SerializeField] private int _score;

    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        instance = this;

        DontDestroyOnLoad(this);
    }

    public void SetVictoryField(bool isVictory) => _isVictory = isVictory;

    public void SetScoreField(int score) => _score = score;
}
