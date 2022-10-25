using UnityEngine;
using System;
public struct PowerUp
{
    public Sprite sprite;
    public string name;
    public Action<Rat> method;
    public PowerUp(Sprite sprite, string name, Action<Rat> method)
    {
        this.sprite = sprite;
        this.name = name;
        this.method = method;
    }

    public static readonly PowerUp None = new PowerUp(null, "None", PowerUp.Methods.None);
    public static readonly PowerUp Jump = new PowerUp(null, "Jump", PowerUp.Methods.Jump);

    public static readonly PowerUp[] powerUps = new PowerUp[]
    {
        PowerUp.None,
        PowerUp.Jump,
    };

    public static class Methods
    {
        public static void Jump(Rat rat)
        {
            
            float jumpHeight = 100f;

            if (rat.controller.isGrounded)
            {
                // f(x)=((x^(2))/(2*9.81))

                SoundEffectManager.PlaySoundEffect(SoundEffect.RatJump);
                rat.controller.AddForce( new Vector3(5f * 5f, (jumpHeight * jumpHeight) / (Physics.gravity.y * -2f), 5f));
                //Debug.Log("Rat did a big jump");
            }
        }
        public static void None(Rat rat)
        {

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
