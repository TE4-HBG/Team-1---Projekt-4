using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public enum SoundEffect
{
    RatDie,
    CollectPowerup,
    PlaceObstacle,
    Goal,
    UsePowerup,
    GameOver,
    RatJump,
    Secret,
}

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;
    

    public AudioSource soundEffectPlayer;

    public SoundEffect[] _keys;
    public AudioClip[] _values;
    public Dictionary<SoundEffect,AudioClip> audioClips;

    public void Awake() 
    { 
        instance = this;
        audioClips = new Dictionary<SoundEffect,AudioClip>();
        for (int i = 0; i < _keys.Length; i++)
        {
            audioClips.Add(_keys[i], _values[i]);
        }
    }

    public static void PlaySoundEffect(SoundEffect soundEffect)
    {
        instance.soundEffectPlayer.PlayOneShot(instance.audioClips[soundEffect]);
    }
}
