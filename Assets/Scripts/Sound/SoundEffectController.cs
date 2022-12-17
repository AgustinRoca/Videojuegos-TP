using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Agregamos un componente obligatorio -> Esto fueza a que unity agregue 
// el componente si no existe en el objeto.
[RequireComponent(typeof(AudioSource))]

public class SoundEffectController : MonoBehaviour
{
    public AudioSource AudioSource => _audioSource;
    [SerializeField] private AudioSource _audioSource;

    public void setAudioSource(AudioSource audioSource){
        _audioSource = audioSource;
    }

    // Reproducir de esta manera evita tener que asignar un clip al source
    public void PlayOneShot(AudioClip clip, float volume = 1.0F) {
        AudioSource.PlayOneShot(clip, volume);
    }

    // Esta reproducci�n necesita tener que asignar un clip al source
    public void Play(AudioClip clip, bool loop = false, float volume = 1.0F) {
        AudioSource.clip = clip;
        AudioSource.loop = loop;
        AudioSource.volume = volume;
        AudioSource.Play();
    } 

    // Detiene un clip si esta asignado y en reproducci�n
    public void Stop() => AudioSource.Stop();

    public bool IsPlaying() => AudioSource.isPlaying;
}