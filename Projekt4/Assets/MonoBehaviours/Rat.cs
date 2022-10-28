using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using System;
using System.ComponentModel;
using UnityEngine.Experimental.GlobalIllumination;

public class Rat : MonoBehaviour
{
    public List<ActivePowerUp> activePowerUps = new List<ActivePowerUp>();
    private PowerUp _powerUp;
    public PowerUp powerUp
    {
        get { return _powerUp; }
        set 
        {
            _powerUp = value;
            //if(powerUp != null) Debug.Log("Rat picked up " + _powerUp.name);

            // do shit here!
            if(_powerUp != null)
            {
                JukeBox.Play(SoundEffect.CollectPowerup);
            }
        }
    }

    public RatController controller;

    public GameObject mesh;
    public Light light;

    public AudioSource audioSource;

    //private Vector3 playerVelocity;
    [SerializeField]
    private float basePlayerSpeed = 8f;
    [SerializeField]
    private float sprintMultiplier = 1.5f;
    public float speedMultiplier = 1f;
    // Update is called once per frame
    void Update()
    {
        float playerSpeed = basePlayerSpeed * speedMultiplier;
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed *= sprintMultiplier;
        }

        audioSource.pitch = playerSpeed / basePlayerSpeed;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            UsePowerUp();
        }


        Vector3 motion = playerSpeed * Time.deltaTime * new Vector3(Input.GetAxisRaw("Horizontal") , 0, Input.GetAxisRaw("Vertical")).normalized;

        controller.Move(motion);

        
        if (motion != Vector3.zero)
        {
            mesh.transform.forward = Vector3.LerpUnclamped(mesh.transform.forward, motion.normalized, Time.deltaTime * 10f);
            
        }
        
        if(motion != Vector3.zero && controller.isGrounded)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Pause();
        }

        if (Input.GetKeyDown(KeyCode.Home)) JukeBox.Play(SoundEffect.Secret);

        UpdateActivePowerUps();
    }
    private void UpdateActivePowerUps()
    {
        for (int i = activePowerUps.Count - 1; i >= 0; i--)
        {
            if (!activePowerUps[i].isActive)
            {
                activePowerUps.RemoveAt(i);
            }
        }
    }
    void UsePowerUp()
    {
        activePowerUps.Add(new ActivePowerUp(this));
        powerUp = default;
    }
}
