using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class WinLoseManager : MonoBehaviour
{
    [Header("Level Loader")]
    [SerializeField] private string startLevelName;             //Start level name
    [SerializeField] private string menuLevelName;              //Menu level name
    [SerializeField] private StringEvent levelChangeEvent;      //Change level Event

    [Header("UI")]
    [SerializeField] private GameObject endScreenPanel;         //Screen that appears at the end of the game
    [SerializeField] private GameObject resultsContainer;       //All the results screen objects
    [SerializeField] private TextMeshProUGUI conditionText;     //Text which informs you if the player won or lost
    [SerializeField] private TextMeshProUGUI validEntryText;    //Checks to see if the entry is valid
    [SerializeField] private TextMeshProUGUI scoreText;         //Score text
    [SerializeField] private TMP_InputField nameEntry;          //Name entry of the player
    [SerializeField] private Button submitButton;               //Button to submit the score

    [Header("Sound effects")]
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClipEvent playSFXEvent;

    [Header("Scoreboard")]
    [SerializeField] private Scoreboard scoreboard;             //Scoreboard object
    [SerializeField] private GameObject firstScoreboardObject;  //Button to highlight

    [Header("Event System")]
    [SerializeField] private EventSystem eventSystem;           //Event System in the Scene

    private int finalScore = 0;                                 //Final score for the game

    //Set the score text on the scoreboard
    public void SetScoreText(int score)
    {
        //Set the score text based on the Player's score
        scoreText.text = $"Final Score: <color=yellow>{score}</color>";

        //Set the final score to be the input score
        finalScore = score;
    }

    //Function to play a sound depending on if the game was won or not
    public void EndGameSound(bool gameWon)
    {
        //If the player won the game
        if (gameWon)
        {
            //Play the win sound
            playSFXEvent.Raise(winSound);
        }
        else
        {
            //Play the lose sound
            playSFXEvent.Raise(loseSound);
        }
    }

    //Check the size of the input text
    public void ValidateText(string entryText)
    {
        //Check if the entry text is empty
        if(entryText.Length == 0)
        {
            //Enable the valid text, telling the user they need to add more text
            validEntryText.text = "Please Enter a Name";

            //Disable the submit button
            submitButton.interactable = false;
        }
        else
        {
            //Disable the valid entry text
            validEntryText.text = "";

            //Enable the submit button
            submitButton.interactable = true;
        }
    }

    public void SubmitEntry()
    {
        //Disable the results group
        resultsContainer.SetActive(false);

        //Enable the scoreboard object
        scoreboard.gameObject.SetActive(true);

        //Submit the player's score
        scoreboard.AddEntry(new ScoreEntryData(nameEntry.text, finalScore));

        //Disable the mouse and lock the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Highlight the first button on the scoreboard screen
        eventSystem.SetSelectedGameObject(firstScoreboardObject);
    }

    public void EndGame(string inputText)
    {
        //Set the timescale to 0
        Time.timeScale = 0;

        //Enable the Cursor and unlock the mouse
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //Set the score Text based on the condition
        conditionText.text = inputText;

        //Enable the panel
        endScreenPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        //Call the Level loader and load the first level
        levelChangeEvent.Raise(startLevelName);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;

        //Call the level loader and load the menu scene
        levelChangeEvent.Raise(menuLevelName);
    }
}
