using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObstacle : MonoBehaviour
{
    public SoundEffect soundEffect = SoundEffect.None;
    private void OnTriggerEnter(Collider other)
    {
        if (other.HasLayer(Layer.Player))
        {
            JukeBox.Play(soundEffect);
            GameManager.GameOver(GameOverReason.RatDied(this));
        }
    }
}
