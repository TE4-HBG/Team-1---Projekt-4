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
    RatJump,
    Secret,
    Death_Fence,
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
        if(soundEffect != SoundEffect.None)
        {
            instance.soundEffectPlayer.PlayOneShot(instance.soundEffects[(int)soundEffect]);
        }
    }
    public static void ChangePitchOverTime(float newPitch = 0f, float time = 2f, ulong steps = 64)
    {
        instance.StartCoroutine(ChangePitchOverTimeEnumerator(newPitch, time, steps));
    }
    private static IEnumerator ChangePitchOverTimeEnumerator(float newPitch, float time, ulong steps)
    {
        float distancePerStep = (newPitch - instance.songPlayer.pitch) / steps;
        for (ulong i = 0; i < steps; i++)
        {
            yield return new WaitForSeconds(time / steps);

            instance.songPlayer.pitch += distancePerStep;
            
        }
        instance.songPlayer.pitch = newPitch;
    }
    public static void SetPitch(float newPitch = 1f)
    {
        instance.songPlayer.pitch = newPitch;
    }

    private void Start()
    {
        Play(Song.Menu);
    }
}
