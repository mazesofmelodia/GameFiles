using System;
using UnityEngine;

[Serializable]
public struct ScoreEntryData
{
    public string entryName;    //Score entry name
    public int entryScore;      //Score entry score

    public ScoreEntryData(string name, int score)
    {
        entryName = name;
        entryScore = score;
    }
}
