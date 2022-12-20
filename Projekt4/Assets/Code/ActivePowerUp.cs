using UnityEngine;

public class ActivePowerUp
{
    public Reference<bool> isActive;
    public Coroutine coroutine;
    public PowerUp powerUp;

    public ActivePowerUp(Rat rat)
    {
        this.isActive = true;
        this.powerUp = rat.PowerUp;
        this.coroutine = rat.StartCoroutine(this.powerUp.method(this.isActive));
    }
}