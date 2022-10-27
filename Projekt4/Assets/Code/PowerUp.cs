using UnityEngine;
using System;
using JetBrains.Annotations;
using System.Collections;

public struct PowerUp
{
    public Sprite sprite;
    public string name;
    public Func<Reference<bool>, IEnumerator> method;
    public Action cancel;
    public PowerUp(Sprite sprite, string name, Func<Reference<bool>, IEnumerator> method, Action cancel = null)
    {
        this.sprite = sprite;
        this.name = name;
        this.method = method;
        this.cancel = cancel;
    }

    public static readonly PowerUp None = new PowerUp(null, "None", Methods.None);
    public static readonly PowerUp Jump = new PowerUp(null, "Spring", Methods.Jump);
    public static readonly PowerUp Light = new PowerUp(null, "Methanol", Methods.Light, Methods.LightCancel);
    public static readonly PowerUp SpeedUp = new PowerUp(null, "Coffee Bean", Methods.SpeedUp, Methods.SpeedUpCancel);
    public static readonly PowerUp Dynamite = new PowerUp(null, "Dynamite", Methods.SpeedUp);

    public static readonly PowerUp[] powerUps = new PowerUp[]
    {
        Jump,
        Light,
        SpeedUp,
        Dynamite,
        None,
    };

    public static class Methods
    {
        public static IEnumerator Jump(Reference<bool> isActive)
        {
            isActive.Set(true);
            float jumpHeight = 100f;

            //if (rat.controller.isGrounded)
            //{
            // f(x)=((x^(2))/(2*9.81))

            JukeBox.Play(SoundEffect.RatJump);
            GameManager.instance.rat.controller.AddForce(new Vector3(5f * 5f, (jumpHeight * jumpHeight) / (Physics.gravity.y * -2f), 5f));
            //Debug.Log("Rat did a big jump");

            //}
            yield return null;
            isActive.Set(false);
        }

        public static IEnumerator None(Reference<bool> isActive)
        {
            isActive.Set(true);
            yield return null;
            isActive.Set(false);
        }
        public static IEnumerator Light(Reference<bool> isActive)
        {
            isActive.Set(true);
            GameManager.instance.rat.light.enabled = true;
            GameManager.SetLight(false);

            yield return new WaitForSeconds(3);

            GameManager.instance.rat.light.enabled = false;
            GameManager.SetLight(true);
            isActive.Set(false);
        }
        public static void LightCancel()
        {
            GameManager.instance.rat.light.enabled = false;
            GameManager.SetLight(true);
        }

        public static IEnumerator SpeedUp(Reference<bool> isActive)
        {
            isActive.Set(true);
            GameManager.instance.rat.speedMultiplier += 1f;

            yield return new WaitForSeconds(4.5f);

            GameManager.instance.rat.speedMultiplier -= 1f;
            isActive.Set(false);
        }
        public static void SpeedUpCancel()
        {
            GameManager.instance.rat.speedMultiplier -= 1f;
        }
    }
    public static string[] names
    {
        get
        {
            string[] names = new string[powerUps.Length];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = powerUps[i].name;
            }
            return names;
        }
    }
}
