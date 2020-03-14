using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;   //Level Loader Object
    [SerializeField] private GameObject pauseScreen;    //Pause Screen
    [SerializeField] private GameObject optionsScreen;  //Options Screen

    [Header("Buttons to Highlight for event system")]
    [SerializeField] private GameObject firstPauseButton;   //The first button highlighted in the pause menu
    [SerializeField] private GameObject secondPauseButton;  //Button to highlight when the options are closed
    [SerializeField] private GameObject firstOptionsButton; //The first button to highlight in the options menu

    [SerializeField] private EventSystem eventSystem;       //Event system

    private bool isPaused = false;
    private bool optionsScreenOpen = false;
    private bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        //If the pause button has been pressed
        if (Input.GetButtonDown("Pause") && !gameOver)
        {
            //Check if the options screen is open
            if (!optionsScreenOpen)
            {
                //Pause or unpause the game
                PauseUnpause();
            }
        }
    }

    public void PauseUnpause()
    {
        //Change the value of the isPaused bool
        isPaused = !isPaused;

        //Check if the game is paused
        if (isPaused)
        {
            //Show the pause Screen
            pauseScreen.SetActive(true);

            //Set the selected game object on the event system to be the first button
            eventSystem.SetSelectedGameObject(firstPauseButton);

            //Stop game time
            Time.timeScale = 0;
        }
        else
        {
            //Show the pause Screen
            pauseScreen.SetActive(false);

            //Stop game time
            Time.timeScale = 1;
        }
    }

    public void OpenOptions()
    {
        //Hide the pause screen and show the options screen
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);

        //Highlight the first options menu button
        eventSystem.SetSelectedGameObject(firstOptionsButton);

        //The options screen is now open
        optionsScreenOpen = true;
    }

    public void CloseOptions()
    {
        //Show the pause screen and hide the options screen
        pauseScreen.SetActive(true);
        optionsScreen.SetActive(false);

        //Highlight the second pause screen button
        eventSystem.SetSelectedGameObject(secondPauseButton);

        //The options screen is now open
        optionsScreenOpen = false;
    }

    public void QuitToMenu(string menuLevelName)
    {
        //Set the timescale to 1
        Time.timeScale = 1;

        //Fade to the menu scene
        levelLoader.LoadLevel(menuLevelName);
    }

    //Function called when the player wins or loses
    public void GameOver()
    {
        //Set game over bool to be true
        gameOver = true;
    }
}
