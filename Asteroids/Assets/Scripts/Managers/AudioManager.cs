using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager singleton;

    public Sound[] Sounds;

    void Awake()
    {
        if (singleton != null)
        {
            Debug.Log(gameObject.name + " already exists.");
            return;
        }

        singleton = this;
    }

    void Start()
    {
        foreach (var s in Sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.loop = s.isLooped;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
        }
    }

    public void Play(string audioName)
    {
        Play(audioName, 1f);
    }

    public void Play(string audioName, float pitch)
    {
        Sound soundPlaying = Array.Find(Sounds, e => string.Equals(e.name, audioName));

        if (soundPlaying == null)
        {
            Debug.Log("Sound with name " + audioName + " not found");
            return;
        }

        soundPlaying.audioSource.pitch = pitch;

        if (soundPlaying.audioSource.isPlaying && soundPlaying.audioSource.loop)
        {
            return;
        }

        soundPlaying.audioSource.Play();
    }

    public void Stop(string audioName)
    {
        Sound soundPlaying = Array.Find(Sounds, e => string.Equals(e.name, audioName));

        if (soundPlaying == null)
        {
            Debug.Log("Sound with name " + audioName + " not found");
            return;
        }

        soundPlaying.audioSource.Stop();
    }
}
