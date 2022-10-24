using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpScript : MonoBehaviour
{

    [SerializeField]
    private int powerUpIndex;

    public PowerUp powerUp
    {
        get
        {
            return PowerUp.powerUps[powerUpIndex];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == Layer.Player)
        {
            
            other.gameObject.GetComponent<Rat>().powerUp = this.powerUp;


            Destroy(this.gameObject);
        }
    }
}