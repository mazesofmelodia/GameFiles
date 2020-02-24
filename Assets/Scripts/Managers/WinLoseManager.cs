using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WinLoseManager : MonoBehaviour
{
    [Header("Level Loader")]
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private string startLevelName;
    [SerializeField] private string menuLevelName;

    [Header("UI Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject firstWinPanelButton;
    [SerializeField] private GameObject firstLosePanelButton;
    [SerializeField] private EventSystem eventSystem;                    //Event System in the Scene

    [Header("Sound effects")]
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClipEvent playSFXEvent;

    public void OpenWinScreen()
    {
        //Enable the win panel
        winPanel.SetActive(true);

        //Have the event system select the first win panel button
        eventSystem.SetSelectedGameObject(firstWinPanelButton);

        //Play the win sound
        playSFXEvent.Raise(winSound);
    }

    public void OpenLoseScreen()
    {
        //Enable the lose panel
        losePanel.SetActive(true);

        //Have the event system select the first lose panel button
        eventSystem.SetSelectedGameObject(firstLosePanelButton);

        //Play the lose sound
        playSFXEvent.Raise(loseSound);
    }

    public void RestartGame()
    {
        //Call the Level loader and load the first level
        levelLoader.LoadLevel(startLevelName);
    }

    public void QuitToMenu()
    {
        //Call the level loader and load the menu scene
        levelLoader.LoadLevel(menuLevelName);
    }
}
