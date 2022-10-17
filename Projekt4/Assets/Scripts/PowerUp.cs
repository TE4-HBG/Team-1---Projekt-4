using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerUp : MonoBehaviour
{
    public Sprite sprite;
    public PowerUpFunction powerUpFunction;

    public static void Jump(Rat rat)
    {
        float jumpHeight = 2.5f;
        if (rat.controller.isGrounded)
        {
            rat.controller.Move(jumpHeight * Time.deltaTime * Physics.gravity);
            Debug.Log("Rat did a big jump");
        }
    }
}