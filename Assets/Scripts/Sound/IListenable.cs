using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListenable
{
    AudioClip AudioClip { get; }
    AudioSource AudioSource { get; }

    void InitAudioSource();
    void PlayOneShot();
    void Play();
    void Stop();
}
