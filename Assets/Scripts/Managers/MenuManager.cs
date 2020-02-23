using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;       //Music to play for menu scene
    [SerializeField] private LevelLoader levelLoader;   //Level Loader
    [SerializeField] private GameObject menuPanel;      //Menu panel
    [SerializeField] private GameObject optionsPanel;   //Options panel

    [Header("Event data")]
    [SerializeField] private AudioClipEvent playMusicEvent;

    [Header("Objects to highlight for Event system")]
    [SerializeField] private GameObject firstMenuButton;    //First menu button to highlight
    [SerializeField] private GameObject firstOptionButton;  //First option button to highlight

    [SerializeField] private EventSystem eventSystem;                    //Event System in the Scene
    // Start is called before the first frame update
    void Start()
    {
        //Play the menu music
        playMusicEvent.Raise(menuMusic);

        //Hide the mouse cursor and lock it in place
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StartGame(string levelName)
    {
        //Load the given level
        levelLoader.LoadLevel(levelName);
    }

    public void QuitGame(){
        //Call the level loader to quit the game
        levelLoader.QuitGame();
    }

    public void OpenOptions()
    {
        //Hide the menu panel
        menuPanel.SetActive(false);
        //Display the options panel
        optionsPanel.SetActive(true);

        //Set the first highlighted object to the first options button
        eventSystem.SetSelectedGameObject(firstOptionButton);
    }

    public void CloseOptions()
    {
        //Hide the options panel
        optionsPanel.SetActive(false);
        //Show the menu panel
        menuPanel.SetActive(true);

        //Set the first highlighted object to the first menu button
        eventSystem.SetSelectedGameObject(firstMenuButton);
    }
}
