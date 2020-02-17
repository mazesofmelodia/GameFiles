using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ResolutionItem
{
    public int horizontal;  //Horizontal Value
    public int vertical;    //Vertical Value
}

public class SettingsMenu : MonoBehaviour
{
    [Header("Graphics Settings")]
    [SerializeField] private TMP_Dropdown qualityDropDown;  //Drop down for quality settings
    [SerializeField] private Toggle fullScreenToggle;       //Fullscreen toggle
    [SerializeField] private Toggle vsyncToggle;            //VSync Toggle

    [Space]
    //List of potential resolutions for the game
    [SerializeField] private List<ResolutionItem> resolutions = new List<ResolutionItem>();
    [SerializeField] private TextMeshProUGUI resolutionText;    //Text to represent resolution

    [Header("Audio Settings")]
    [SerializeField] private AudioClip soundTest;   //Sound to play for sound testing
    [SerializeField] private Slider musicSlider;    //Sliders
    [SerializeField] private Slider sfxSlider;

    [Header("Event Data")]
    [SerializeField] private AudioClipEvent playSFXEvent;
    [SerializeField] private FloatEvent changeMusicVolumeEvent;
    [SerializeField] private FloatEvent changeSFXVolumeEvent;

    private int selectedResolution = 7;                 //Index number of the currently selected resolution

    private void Start()
    {
        //Set the quality dropdown selection based on the quality settings
        qualityDropDown.value = QualitySettings.GetQualityLevel();
        //Refresh the dropdown
        qualityDropDown.RefreshShownValue();

        //Check if fullscreen toggle should be on or off
        fullScreenToggle.isOn = Screen.fullScreen;

        //Check the current vsync value
        if(QualitySettings.vSyncCount == 0)
        {
            //If it's 0 the toggle should be off
            vsyncToggle.isOn = false;
        }
        else
        {
            //Otherwise it's on
            vsyncToggle.isOn = true;
        }

        //Check for current resolution
        bool foundResolution = false;

        //Loop through all resolutions available
        for (int i = 0; i < resolutions.Count; i++)
        {
            //If the current resolution matches the screen width and height
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                //The current resolution has been found
                foundResolution = true;

                //Set the current selected resolution
                selectedResolution = i;

                //Update the text label to reflect the resolution
                UpdateResolutionText(resolutions[i].horizontal, resolutions[i].vertical);
            }
        }

        //If the resolution still hasn't been found
        if (!foundResolution)
        {
            //Set the resolution to the current screen size
            UpdateResolutionText(Screen.width, Screen.height);
        }

        //Check for saved music value
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            //Change the value to the set value
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        //Check for saved music value
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            //Change the value to the set value
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    //Apply any graphic changes made
    public void ApplyGraphicsChanges()
    {
        //Apply Graphics Quality
        QualitySettings.SetQualityLevel(qualityDropDown.value);

        //Save the quality
        PlayerPrefs.SetInt("QualityLevel", qualityDropDown.value);

        //Apply VSync settings
        if (vsyncToggle.isOn)
        {
            //Set vsync to one, ensures that vsync is running
            QualitySettings.vSyncCount = 1;

            //Save vsync value
            SaveVSyncValue(1);
        }
        else
        {
            //Set vsync to 0 essentially turning vsync off
            QualitySettings.vSyncCount = 0;

            //Save vsync value
            SaveVSyncValue(0);
        }

        //Apply fullscreen and resolution settings
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullScreenToggle.isOn);
    }

    private void SaveVSyncValue(int vsyncValue)
    {
        //Save the vsync settings in PlayerPrefs
        PlayerPrefs.SetInt("VSyncValue", vsyncValue);
    }

    public void ResMoveLeft()
    {
        //Go down one on the selected resolution list
        selectedResolution--;

        //If the value is less than 0
        if(selectedResolution < 0)
        {
            //Set selectedResolution to the max number in the list
            selectedResolution = resolutions.Count - 1;
        }

        //Update the resolution text
        UpdateResolutionText(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical);
    }

    public void ResMoveRight()
    {
        //Go down one on the selected resolution list
        selectedResolution++;

        //If the value is higher than the resolution list
        if (selectedResolution > resolutions.Count - 1)
        {
            //Set selectedResolution to the minimum number in the list
            selectedResolution = 0;
        }

        //Update the resolution text
        UpdateResolutionText(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical);
    }

    private void UpdateResolutionText(int horizontalValue, int verticalValue)
    {
        //Updates the text to reflect the changes of resolution value
        resolutionText.text = horizontalValue.ToString() + " x " + verticalValue.ToString();
    }

    //Changes the volume of the music in the game
    public void SetMusicVolume (float newVolume)
    {
        //Set the audio manager volume
        changeMusicVolumeEvent.Raise(newVolume);

        //Save the music volume in PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", newVolume);
    }

    //Changes the volume of the music in the game
    public void SetSFXVolume(float newVolume)
    {
        //Set the audio manager volume
        changeSFXVolumeEvent.Raise(newVolume);

        //Save the sfx volume in PlayerPrefs
        PlayerPrefs.SetFloat("SFXVolume", newVolume);

        //Play a sound to indicate the volume change
        playSFXEvent.Raise(soundTest);
    }

    public void SetQuality(int qualityIndex)
    {
        Debug.Log("Selected Value is: " + qualityDropDown.value);

        QualitySettings.SetQualityLevel(qualityIndex);
    }

}
