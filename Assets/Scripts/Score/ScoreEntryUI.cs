using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryNameText = null;  //Name text reference
    [SerializeField] private TextMeshProUGUI entryScoreText = null;  //Score text reference

    public void Initialize(ScoreEntryData entryData)
    {
        //Set the entry text based on the data
        entryNameText.text = entryData.entryName;
        entryScoreText.text = $"{entryData.entryScore}";
    }
}
