using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using UnityEngine;

public enum Song
{
    Menu,
    Preparations,
    Level,
    /// <summary>
    /// The last item in the songs enum
    /// </summary>
    None,
}
public enum SoundEffect
{
    CollectPowerup,
    PlaceObstacle,
    Goal,
    GameOver,
    RatJump,
    Secret,
    None,
}

public class JukeBox : MonoBehaviour
{
    private void OnValidate()
    {
        if (songs.Length != (int)Song.None)
        {
            AudioClip[] newSongs = new AudioClip[(int)Song.None];

            for (int i = 0; i < songs.Length && i < newSongs.Length; i++)
            {
                newSongs[i] = songs[i];
            }
            songs = newSongs;
        }
        if (soundEffects.Length != (int)SoundEffect.None)
        {
            AudioClip[] newSoundEffects = new AudioClip[(int)SoundEffect.None];

            for (int i = 0; i < soundEffects.Length && i < newSoundEffects.Length; i++)
            {
                newSoundEffects[i] = soundEffects[i];
            }
            soundEffects = newSoundEffects;
        }
    }
    public static JukeBox instance;
    private void Awake() => instance = this; 
    public AudioSource songPlayer;
    public AudioSource soundEffectPlayer;
    public AudioClip[] songs;
    public AudioClip[] soundEffects;
    public static void Play(Song song)
    {
        if(song == Song.None)
        {
            instance.songPlayer.Stop();
        }
        else
        {
            if(instance.songPlayer.clip != instance.songs[(int)song])
            {
                instance.songPlayer.clip = instance.songs[(int)song];
            }
            instance.songPlayer.Play();
        }
    }
    public static void Play(SoundEffect soundEffect)
    {
        instance.soundEffectPlayer.PlayOneShot(instance.soundEffects[(int)soundEffect]);
    }
    private void Start()
    {
        Play(Song.Menu);
    }
}
