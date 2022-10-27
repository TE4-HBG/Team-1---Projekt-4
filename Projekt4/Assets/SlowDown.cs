using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    // Start is called before the first frame update

    private float speed = 1f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.HasLayer(Layer.Player))
        {
            other.GetComponent<Rat>().speedMultiplier += speed;
        }
    }
}
