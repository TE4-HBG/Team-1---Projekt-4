using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using System;
using System.ComponentModel;
using UnityEngine.Experimental.GlobalIllumination;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class Rat : MonoBehaviour
{
    public List<ActivePowerUp> activePowerUps = new List<ActivePowerUp>();
    private PowerUp _powerUp;
    Vector2 input;
    //Vector3 lastMotion;
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
            if (_powerUp != null)
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

        Vector3 motion = playerSpeed * Time.deltaTime * new Vector3(input.x, 0, input.y).normalized;

        controller.Move(motion);

        if (motion != Vector3.zero)
        {
            mesh.transform.forward = Vector3.LerpUnclamped(mesh.transform.forward, motion.normalized, Time.deltaTime * 10f);
        }

        if (motion != Vector3.zero && controller.isGrounded)
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
        //mesh.transform.localScale = Vector3.one - (((motion - lastMotion) / Time.deltaTime) * 10);     

        if (Keyboard.current.homeKey.wasPressedThisFrame) JukeBox.Play(SoundEffect.Secret);



        UpdateActivePowerUps();
    }
    /*
    private void LateUpdate()
    {
        lastMotion = motion;
    }
    */
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
    public void Input_Move(InputAction.CallbackContext callbackContext) { input = callbackContext.ReadValue<Vector2>(); Debug.Log(input); }
    public void Input_UsePowerUp(InputAction.CallbackContext context) { if (context.phase == InputActionPhase.Performed && PowerUp != null) { activePowerUps.Add(new ActivePowerUp(this)); PowerUp = null; } }
    public void Input_Secret(InputAction.CallbackContext context) { if (context.phase == InputActionPhase.Performed) JukeBox.Play(SoundEffect.Secret); }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Layer.TileWalkable)
        {
            currentTile = collision.transform.parent.gameObject;
        }
    }
}
