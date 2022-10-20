using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset = new Vector3(0,0,60);
    
    
    private void OnTriggerEnter(Collider possiblePlayer)
    {
        if(possiblePlayer.gameObject.layer == Layer.Player)
        {
            GameManager.NextLevel(offset);
        }
    }
}
