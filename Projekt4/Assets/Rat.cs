using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using System;

[RequireComponent(typeof(CharacterController))]
public class Rat : MonoBehaviour
{
    //private Action<Rat> powerUpFunction = PowerUp.Jump;
    private PowerUpFunction powerUpFunction = PowerUp.Jump;

    public CharacterController controller;
    //private Vector3 playerVelocity;
    [SerializeField]
    private float basePlayerSpeed = 8f;
    [SerializeField]
    private float sprintMultiplier = 1.5f;

    // Update is called once per frame
    void Update()
    {
        float playerSpeed = basePlayerSpeed;
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed *= sprintMultiplier;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            powerUpFunction(this);
        }


        Vector3 motion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * playerSpeed;

        // apply gravity
        motion += Physics.gravity;

        // multiply by time previous frame took
        motion *= Time.deltaTime;

        // move
        controller.Move(motion);

        /*
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        */
        // Changes the height position of the player..


        /*
        // why is this here???
        // this does absolutely nothing!!
        controller.Move(playerVelocity * Time.deltaTime);
        */
    }
}
