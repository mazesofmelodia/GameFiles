using System;
using System.Collections.Generic;

[Serializable]
public class ScoreboardSaveData
{
    //List of high scores
    public List<ScoreEntryData> highScores = new List<ScoreEntryData>();
}
