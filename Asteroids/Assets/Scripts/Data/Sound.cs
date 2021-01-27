using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume = 0.5f;

    [Range(0.1f, 2f)]
    public float pitch = 1.0f;

    public bool isLooped;

    [HideInInspector]
    public AudioSource audioSource;
}
