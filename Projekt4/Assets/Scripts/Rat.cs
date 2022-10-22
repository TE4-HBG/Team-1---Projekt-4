using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using System;

public class Rat : MonoBehaviour
{
    //private Action<Rat> powerUpFunction = PowerUp.Jump;
    private PowerUpFunction powerUpFunction = PowerUp.Jump;

    public RatController controller;

    [SerializeField]
    private GameObject mesh;


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


        Vector3 motion = playerSpeed * Time.deltaTime * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        controller.Move(motion);

        
        if (motion != Vector3.zero)
        {
            mesh.transform.forward = -motion;
        }

        if (Input.GetKeyDown(KeyCode.Home)) SoundEffectManager.PlaySoundEffect(SoundEffect.Secret);
    }
}
