using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    [Header("Sources")]
    
    [Header("Clips")]
    
    [Header("Mixer")] 
    [SerializeField] private AudioMixer audioMixer;

    public AudioMixer AudioMixer => audioMixer;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("Volume", value);
    }
}
