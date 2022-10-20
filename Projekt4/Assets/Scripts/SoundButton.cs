using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public AudioSource audioSource;
    public Image soundOn;
    public Image soundOff;
    public void Press()
    {
        if (soundOn.gameObject.activeSelf)
        {
            soundOn.gameObject.SetActive(false);
            soundOff.gameObject.SetActive(true);
            audioSource.Pause();
        }
        else
        {
            soundOn.gameObject.SetActive(true);
            soundOff.gameObject.SetActive(false);
            audioSource.Play();
        }
    }
}
