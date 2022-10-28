using UnityEngine;
using System;
using System.Collections;

[CreateAssetMenu(fileName = "Power Up", menuName = "Power Up")]
public class PowerUp : ScriptableObject
{
    public Sprite sprite;

    
    public int methodIndex;
    public Func<Reference<bool>, IEnumerator> method => methods[methodIndex];
    public Action cancel => cancels[methodIndex];

    /*
    public static readonly PowerUp None = new PowerUp(null, "None", Methods.None);
    public static readonly PowerUp Jump = new PowerUp(null, "Spring", Methods.Jump);
    public static readonly PowerUp Light = new PowerUp(null, "Methanol", Methods.Light, Methods.LightCancel);
    public static readonly PowerUp SpeedUp = new PowerUp(null, "Coffee Bean", Methods.SpeedUp, Methods.SpeedUpCancel);
    public static readonly PowerUp Dynamite = new PowerUp(null, "Dynamite", Methods.SpeedUp);
    */

    public static readonly Func<Reference<bool>, IEnumerator>[] methods = new Func<Reference<bool>, IEnumerator>[]
    {
        Methods.Jump,
        Methods.Light,
        Methods.SpeedUp,
        //Methods.Dynamite,
    };
    public static string[] methodNames
    {
        get
        {
            string[] names = new string[methods.Length];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = methods[i].Method.Name;
            }
            return names;
        }
    }
    public static readonly Action[] cancels = new Action[]
    {
        Methods.NoCancel,
        Methods.LightTurnOff,
        Methods.SpeedUpTurnOff,
    };
    public static class Methods
    {
        public static void NoCancel(){}
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
        public static IEnumerator Light(Reference<bool> isActive)
        {
            isActive.Set(true);
            GameManager.instance.rat.light.enabled = true;
            GameManager.SetLight(false);

            yield return new WaitForSeconds(3);

            LightTurnOff();
            isActive.Set(false);
        }
        public static void LightTurnOff()
        {
            GameManager.instance.rat.light.enabled = false;
            GameManager.SetLight(true);
        }

        public static IEnumerator SpeedUp(Reference<bool> isActive)
        {
            isActive.Set(true);
            GameManager.instance.rat.speedMultiplier = 1.5f;
            JukeBox.ChangePitchOverTime(1.5f, 0.25f, 32);

            yield return new WaitForSeconds(2.5f);


            JukeBox.ChangePitchOverTime(1.0f, 0.5f, 32);
            GameManager.instance.rat.speedMultiplier = 1f;
            
            isActive.Set(false);
        }
        public static void SpeedUpTurnOff()
        {
            GameManager.instance.rat.speedMultiplier = 1f;
            JukeBox.SetPitch(1f);
        }
    }
}
