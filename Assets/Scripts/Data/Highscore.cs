using System;

[Serializable]
public class Highscore
{
    public string Name { get; set; }
    public long Score { get; set; }

    public Highscore(string name, long score)
    {
        Name = name;
        Score = score;
    }
}