using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _gameoverMessage;
    private SoundEffectController _backgroundMusicController;
    [SerializeField] private AudioClip _victoryClip;
    [SerializeField] private AudioClip _defeatClip;
    [SerializeField] private AudioClip _inGameClip;
    [SerializeField] private AudioClip _bossFightClip;


    void Start()
    {
        EventManager.instance.OnGameOver += OnGameOver;
        EventManager.instance.OnChangeBackgroundMusic += OnChangeBackgroundMusic;
        _gameoverMessage.text = string.Empty;
        _backgroundMusicController = GetComponent<SoundEffectController>();
        playInGameMusic();
    }

    public void OnChangeBackgroundMusic(bool boss){
        if(boss){
            playBossFightMusic();
        } else {
            playInGameMusic();
        }
    }

    private void playBossFightMusic(){
        if(_backgroundMusicController.AudioSource.clip != _bossFightClip){
            _backgroundMusicController.Play(_bossFightClip, true, 0.8f);
        }
    }

    private void playInGameMusic(){
        if(_backgroundMusicController.AudioSource.clip != _inGameClip){
            _backgroundMusicController.Play(_inGameClip, true, 1f);
        }
    }


    public void OnGameOver(bool hasWon)
    {
        _gameoverMessage.text = "You died";
        _backgroundMusicController.Stop();
        _backgroundMusicController.Play(_defeatClip, true);
        
        Invoke("LoadEndgameScene", 3);
    }

    private void LoadEndgameScene() => SceneManager.LoadScene("Endgame");
}
