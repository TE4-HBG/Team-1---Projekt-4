using UnityEngine;
using System;
using JetBrains.Annotations;
using System.Collections;

public struct PowerUp
{
    public Sprite sprite;
    public string name;
    public Func<Rat, IEnumerator> method;
    public PowerUp(Sprite sprite, string name, Func<Rat, IEnumerator> method)
    {
        this.sprite = sprite;
        this.name = name;
        this.method = method;
    }

    public static readonly PowerUp None = new PowerUp(null, "None", PowerUp.Methods.None);
    public static readonly PowerUp Jump = new PowerUp(null, "Jump", PowerUp.Methods.Jump);
    public static readonly PowerUp Light = new PowerUp(null, "Light", PowerUp.Methods.Light);
    public static readonly PowerUp SpeedUp = new PowerUp(null, "SpeedUp", PowerUp.Methods.SpeedUp);

    public static readonly PowerUp[] powerUps = new PowerUp[]
    {
        PowerUp.None,
        PowerUp.Jump,
        PowerUp.Light,
        PowerUp.SpeedUp,
    };

    public static class Methods
    {
        public static IEnumerator Jump(Rat rat)
        {
            
            float jumpHeight = 100f;

            if (rat.controller.isGrounded)
            {
                // f(x)=((x^(2))/(2*9.81))

                JukeBox.Play(SoundEffect.RatJump);
                rat.controller.AddForce( new Vector3(5f * 5f, (jumpHeight * jumpHeight) / (Physics.gravity.y * -2f), 5f));
                //Debug.Log("Rat did a big jump");
                
            }
            yield return null;
        }

        public static IEnumerator None(Rat rat)
        {
            yield return null;
        }
        public static IEnumerator Light(Rat rat)
        {
            rat.light.enabled = true;
            GameManager.SetLight(false);
            
            yield return new WaitForSeconds(3);

            rat.light.enabled = false;
            GameManager.SetLight(true);
        }

        public static IEnumerator SpeedUp(Rat rat)
        {
            rat.speedMultiplier += 1f;

            yield return new WaitForSeconds(4.5f);

            rat.speedMultiplier -= 1f;
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
