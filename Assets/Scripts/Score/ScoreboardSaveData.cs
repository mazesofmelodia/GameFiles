using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreboardSaveData
{
    //List of high scores
    public List<ScoreEntryData> highScores = new List<ScoreEntryData>();

    public ScoreboardSaveData()
    {
        //Add initial scores to data
        highScores.Add(new ScoreEntryData("AAAAAA", 3000));
        highScores.Add(new ScoreEntryData("BBBBBB", 2700));
        highScores.Add(new ScoreEntryData("CCCCCC", 2400));
        highScores.Add(new ScoreEntryData("DDDDDD", 2100));
        highScores.Add(new ScoreEntryData("EEEEEE", 1800));
        highScores.Add(new ScoreEntryData("FFFFFF", 1500));
        highScores.Add(new ScoreEntryData("GGGGGG", 1200));
        highScores.Add(new ScoreEntryData("HHHHHH", 900));
        highScores.Add(new ScoreEntryData("IIIIII", 600));
        highScores.Add(new ScoreEntryData("JJJJJJ", 300));
    }
}
