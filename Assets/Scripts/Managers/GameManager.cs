using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Default,
    Battle,
    Win,
    Lose
}

public class GameManager : MonoBehaviour
{
    /* Static instance */
    public static GameManager Instance;      //GameManager in scene
    //Public referance to the GameManager for other scripts to access
    /*public static GameManager Instance{
        get{
            //Check if there is no instance of the game manager in the scene
            if(instance == null){
                //Look for an audio manager in scene
                instance = FindObjectOfType<GameManager>();
                //If we still didn't find a game manager
                if(instance == null){
                    //Create a new game object with a game manager and set that as the instance
                    instance = new GameObject("NewGameManager", typeof(GameManager)).GetComponent<GameManager>();
                    //Add a scene transition to the gameobject
                    instance.gameObject.AddComponent(typeof (SceneTransition));
                }
            }

            return instance;
        }
        //Ensures that set functionality can't be set be any other script
        private set{
            //Set the instance to a value
            instance = value;
        }
    }*/

    /* Other variables */
    [SerializeField] private AudioClip bgm;         //Reference to background music
    [SerializeField] private AudioClip battleTheme; //Battle theme when there are enemies in the scene
    [SerializeField] private GameObject winTreasure;    //Treasure to appear when all enemies are defeated

    [HideInInspector] public GameState gameState = GameState.Default;     //Current state of the game

    private int enemiesInScene = 0;                     //Number of enemies currently in the scene

    private void Awake() {
        //Check if there is no instance in the scene
        if (Instance == null)
        {
            //Make this Manager the instance
            Instance = this;
        }
        //If there's already an instance
        else if (Instance != null)
        {
            //Destroy this game object
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Play the background music
        AudioManager.Instance.PlayMusic(bgm);
    }

    public void AdjustEnemyCountInScene(int number){
        //Adjust the number of enemies in the scene
        enemiesInScene += number;
        //Adjust the game depending on how many enemies are present in the scene
        SetBattleState();
    }

    void SetBattleState(){
        //If there are any enemies in scene
        if(enemiesInScene > 0){
            //Check if the player has already encountered an enemy
            if(gameState != GameState.Battle){
                //Play the battle theme
                AudioManager.Instance.PlayMusicWithCrossFade(battleTheme);
                //Set the battle engaged to true
                gameState = GameState.Battle;
                //Hide the win treasure
                //winTreasure.SetActive(false);
            }
        }else{
            //Play the level theme
            AudioManager.Instance.PlayMusicWithCrossFade(bgm);
            //Player is no longer fighting an enemy
            gameState = GameState.Default;
            //Show the win treasure
            //winTreasure.SetActive(true);
        }
    }

}
