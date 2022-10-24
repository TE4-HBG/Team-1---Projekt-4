
using UnityEngine;

public struct GameOverReason
{
    public static GameOverReason TimeRanOut(MonoBehaviour caller = null)
    {
        return new GameOverReason("The time ran out!", caller);
    }
    public static GameOverReason RatDied(MonoBehaviour caller = null)
    {
        return new GameOverReason("The rat fucking died :(", caller);
    }
    public string info;
    public MonoBehaviour caller;
    public GameOverReason(string info, MonoBehaviour caller = null)
    {
        this.info = info;
        this.caller = caller;
    }
}
