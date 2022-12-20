using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpScript : MonoBehaviour
{
    public GameObject SPINNER;

    public PowerUp powerUp;
    public float rotationSpeed = 360f;
    private void Update()
    {
        SPINNER.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.HasLayer(Layer.Player))
        {
            
            other.gameObject.GetComponent<Rat>().PowerUp = this.powerUp;


            Destroy(this.gameObject);
        }
    }
}