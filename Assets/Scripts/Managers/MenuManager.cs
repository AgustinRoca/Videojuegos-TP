using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private SoundEffectController _backgroundMusicController;
    [SerializeField] private AudioClip _music;

    void Start()
    {
        _backgroundMusicController = GetComponent<SoundEffectController>();
        _backgroundMusicController.Play(_music, true);
    }

}
