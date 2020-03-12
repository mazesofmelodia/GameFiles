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
        highScores.Add(new ScoreEntryData("AAAAAA", 8000));
        highScores.Add(new ScoreEntryData("BBBBBB", 7000));
        highScores.Add(new ScoreEntryData("CCCCCC", 6000));
        highScores.Add(new ScoreEntryData("DDDDDD", 5000));
        highScores.Add(new ScoreEntryData("EEEEEE", 4500));
        highScores.Add(new ScoreEntryData("FFFFFF", 4000));
        highScores.Add(new ScoreEntryData("GGGGGG", 3500));
        highScores.Add(new ScoreEntryData("HHHHHH", 3000));
        highScores.Add(new ScoreEntryData("IIIIII", 2500));
        highScores.Add(new ScoreEntryData("JJJJJJ", 2000));
    }
}
