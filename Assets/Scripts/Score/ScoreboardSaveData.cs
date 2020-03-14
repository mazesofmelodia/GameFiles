using System;
using System.Collections.Generic;

[Serializable]
public class ScoreboardSaveData
{
    //List of high scores
    public List<ScoreEntryData> highScores = new List<ScoreEntryData>();

    public ScoreboardSaveData()
    {
        //Add initial scores to data
        highScores.Add(new ScoreEntryData("AAAAAA", 5000));
        highScores.Add(new ScoreEntryData("BBBBBB", 4500));
        highScores.Add(new ScoreEntryData("CCCCCC", 4000));
        highScores.Add(new ScoreEntryData("DDDDDD", 3500));
        highScores.Add(new ScoreEntryData("EEEEEE", 3000));
        highScores.Add(new ScoreEntryData("FFFFFF", 2500));
        highScores.Add(new ScoreEntryData("GGGGGG", 2000));
        highScores.Add(new ScoreEntryData("HHHHHH", 1500));
        highScores.Add(new ScoreEntryData("IIIIII", 1000));
        highScores.Add(new ScoreEntryData("JJJJJJ", 500));
    }
}
