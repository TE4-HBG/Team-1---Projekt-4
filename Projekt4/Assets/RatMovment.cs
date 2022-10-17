using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class RatMovment : MonoBehaviour
{
    private PowerUpFunction powerUpFunction = PowerUp.Jump;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.5f;
    private float jumpHeight = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = 5.5f;
        }
        else
        {
            playerSpeed = 2.5f;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            powerUpFunction(this);
        }


        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..


        controller.Move(playerVelocity * Time.deltaTime);
    }
}
