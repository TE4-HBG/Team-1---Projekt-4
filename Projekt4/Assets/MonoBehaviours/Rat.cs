using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using System;
using System.ComponentModel;
using UnityEngine.Experimental.GlobalIllumination;
using Unity.VisualScripting;

public class Rat : MonoBehaviour
{
    public List<ActivePowerUp> activePowerUps = new List<ActivePowerUp>();
    private PowerUp _powerUp;

    public void DisablePowerUps()
    {
        for (int i = activePowerUps.Count - 1; i >= 0; i--)
        {
            if (activePowerUps[i].isActive)
            {
                StopCoroutine(activePowerUps[i].coroutine);
                activePowerUps[i].powerUp.cancel();

                activePowerUps.RemoveAt(i);
            }
        }
    }
    public PowerUp PowerUp
    {
        get { return _powerUp; }
        set 
        {
            _powerUp = value;
            //if(powerUp != null) Debug.Log("Rat picked up " + _powerUp.name);

            // do shit here!
            if(_powerUp != null)
            {
                GameManager.instance.powerUpSpriteUI.gameObject.SetActive(false);
                GameManager.instance.powerUpSpriteUI.sprite = _powerUp.sprite;

                GameManager.instance.powerUpNameUI.text = _powerUp.name;

                JukeBox.Play(SoundEffect.CollectPowerup);
            }
            else
            {
                GameManager.instance.powerUpNameUI.text = string.Empty;
                GameManager.instance.powerUpSpriteUI.gameObject.SetActive(false);
            }
        }
    }

    public RatController controller;
    public GameObject currentTile;
    public GameObject mesh;
    public Light light;

    public AudioSource audioSource;

    //private Vector3 playerVelocity;
    [SerializeField]
    private float basePlayerSpeed = 8f;
    [NonSerialized]
    public float speedMultiplier = 1f;
    // Update is called once per frame
    void Update()
    {
        float playerSpeed = basePlayerSpeed * speedMultiplier;

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
        PowerUp = default;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == Layer.TileWalkable)
        {
            currentTile = collision.transform.parent.gameObject;
        }
    }
}
