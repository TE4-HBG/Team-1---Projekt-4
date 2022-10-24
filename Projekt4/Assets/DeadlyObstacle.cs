using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.HasLayer(Layer.Player))
        {
            GameManager.GameOver(GameOverReason.RatDied(this));
        }
    }
}
