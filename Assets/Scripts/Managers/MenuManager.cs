using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;           //Music to play for menu scene
    [SerializeField] private GameObject menuPanel;          //Menu panel
    [SerializeField] private GameObject charactersPanel;    //Character select panel
    [SerializeField] private GameObject optionsPanel;       //Options panel
    [SerializeField] private GameObject scoreBoardPanel;    //High Scores Panel

    [Header("Event data")]
    [SerializeField] private AudioClipEvent playMusicEvent;
    [SerializeField] private StringEvent levelChangeEvent;
    [SerializeField] private VoidEvent quitGameEvent;

    [Header("Objects to highlight for Event system")]
    [SerializeField] private GameObject firstMenuButton;        //First menu button to highlight
    [SerializeField] private GameObject secondMenuButton;       //Second menu Button to highlight
    [SerializeField] private GameObject thirdMenuButton;        //Third menu Button to highlight
    [SerializeField] private GameObject firstCharacterButton;   //First character button to highlight
    [SerializeField] private GameObject firstOptionButton;      //First option button to highlight
    [SerializeField] private GameObject firstScoreBoardButton;  //First button to highlight for scoreboard

    [Header("Event System")]
    [SerializeField] private EventSystem eventSystem;           //Event System in the Scene

    [Header("Scoreboard")]
    [SerializeField] private Scoreboard scoreboard;             //Scoreboard object

    private void Awake()
    {
        //Check if there is a player game object in the scene
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if(playerObject != null)
        {
            //Set player spawned to false
            PlayerSpawner.PlayerSpawned = false;

            //Destroy the object
            Destroy(playerObject);

            //Set the selected character to null
            PlayerSpawner.SelectedCharacter = null;
        }
    }
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
        levelChangeEvent.Raise(levelName);
    }

    public void QuitGame(){
        //Call the level loader to quit the game
        quitGameEvent.Raise();
    }

    public void OpenCharacterSelect()
    {
        //Hide the menu panel
        menuPanel.SetActive(false);
        //Display the characters panel
        charactersPanel.SetActive(true);

        //Set the first highlighted object to the first character button
        eventSystem.SetSelectedGameObject(firstCharacterButton);
    }

    public void CloseCharacterSelect()
    {
        //Hide the characters panel
        charactersPanel.SetActive(false);
        //Show the menu panel
        menuPanel.SetActive(true);

        //Set the first highlighted object to the first menu button
        eventSystem.SetSelectedGameObject(firstMenuButton);
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
        eventSystem.SetSelectedGameObject(thirdMenuButton);
    }

    public void OpenScoreBoard()
    {
        //Hide the menu panel
        menuPanel.SetActive(false);
        //Display the options panel
        scoreBoardPanel.SetActive(true);

        //List the scores in the scoreboard
        scoreboard.ListScores();

        //Set the first highlighted object to the first options button
        eventSystem.SetSelectedGameObject(firstScoreBoardButton);
    }

    public void CloseScoreBoard()
    {
        //Hide the options panel
        scoreBoardPanel.SetActive(false);
        //Show the menu panel
        menuPanel.SetActive(true);

        //Set the first highlighted object to the first menu button
        eventSystem.SetSelectedGameObject(secondMenuButton);
    }
}
