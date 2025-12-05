using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int HighScore;
    public List<ScoreData> list;

    public SaveData(int HighScore, List<ScoreData> list)
    {
        this.HighScore = HighScore;
        this.list = list;
    }
}

[Serializable]
public class ScoreData
{
    public string playerName;
    public int playerScore;

    public ScoreData(string playerName, int playerScore)
    {
        this.playerName = playerName;
        this.playerScore = playerScore;
    }
}