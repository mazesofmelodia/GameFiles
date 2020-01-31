using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    //Make changes to the graphics settings when the game loads up
    private void Awake()
    {
        //Check if there are quality settings saved
        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            //Apply Graphics Quality
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel"));
        }

        //Check if there are saved vsync levels
        if (PlayerPrefs.HasKey("VSyncValue"))
        {
            QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSyncValue");
        }
    }
}
