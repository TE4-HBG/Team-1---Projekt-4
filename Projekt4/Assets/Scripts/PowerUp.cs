using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerUp : MonoBehaviour
{
    public Sprite sprite;
    public PowerUpFunction powerUpFunction;

    public static void Jump(Rat rat)
    {
        Debug.Log("Rat did a big jump at " + rat.transform.position.ToString());
    }
}