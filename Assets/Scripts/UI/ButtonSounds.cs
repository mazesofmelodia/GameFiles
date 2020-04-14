using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioClip selectSound = null;  //Sound when the button is highlighted
    [SerializeField] private AudioClip clickSound = null;   //Sound when the button is clicked
    [SerializeField] private AudioClipEvent playSFXEvent;   //Sound play event

    //Function to raise the sfx event on select
    public void PlaySelectSFX()
    {
        playSFXEvent.Raise(selectSound);
    }

    //Function to raise the sfx event on click
    public void PlayClickSFX()
    {
        playSFXEvent.Raise(clickSound);
    }
}
