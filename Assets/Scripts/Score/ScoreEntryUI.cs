using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryNameText = null;  //Name text reference
    [SerializeField] private TextMeshProUGUI entryScoreText = null; //Score text reference
    [SerializeField] private Image backgroundImage = null;          //Background image
    [SerializeField] private Color highlightColor;                  //Color to highlight the background image

    public void Initialize(ScoreEntryData entryData)
    {
        //Set the entry text based on the data
        entryNameText.text = entryData.entryName;
        entryScoreText.text = $"{entryData.entryScore}";
    }

    public void HighlightUI()
    {
        //Change the background image colour to the highlighted colour
        backgroundImage.color = highlightColor;
    }
}
