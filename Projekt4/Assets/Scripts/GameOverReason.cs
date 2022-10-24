
public struct GameOverReason
{
    public static readonly GameOverReason TimeRanOut = new GameOverReason("The time ran out!");
    public static readonly GameOverReason RatDied = new GameOverReason("The rat fucking died :(");
    public string info;
    public GameOverReason(string info)
    {
        this.info = info;
    }

    public override string ToString()
    {
        return info;
    }
}
