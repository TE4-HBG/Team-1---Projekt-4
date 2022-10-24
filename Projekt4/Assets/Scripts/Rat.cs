using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using System;
using System.ComponentModel;

public class Rat : MonoBehaviour
{
    //private Action<Rat> powerUpFunction = PowerUp.Jump;
    private PowerUp _powerUp;

    public PowerUp powerUp
    {
        get { return _powerUp; }
        set 
        {
            _powerUp = value;
            if(powerUp != null) Debug.Log("Rat picked up " + _powerUp.name);

            // do shit here!
        }
    }

    public RatController controller;

    public GameObject mesh;


    

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
            UsePowerUp();
        }


        Vector3 motion = playerSpeed * Time.deltaTime * new Vector3(Input.GetAxisRaw("Horizontal") , 0, Input.GetAxisRaw("Vertical")).normalized;

        controller.Move(motion);

        
        if (motion != Vector3.zero)
        {
            mesh.transform.forward = motion;
        }

        if (Input.GetKeyDown(KeyCode.Home)) SoundEffectManager.PlaySoundEffect(SoundEffect.Secret);
    }

    void UsePowerUp()
    {
        if(_powerUp != null && _powerUp.powerUpFunction != null)
        {
            _powerUp.powerUpFunction(this);
        }
    }
}
