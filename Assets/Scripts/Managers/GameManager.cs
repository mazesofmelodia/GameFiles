using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Static instance */
    private static GameManager instance;      //GameManager in scene
    //Public referance to the GameManager for other scripts to access
    public static GameManager Instance{
        get{
            //Check if there is no instance of the game manager in the scene
            if(instance == null){
                //Look for an audio manager in scene
                instance = FindObjectOfType<GameManager>();
                //If we still didn't find a game manager
                if(instance == null){
                    //Create a new game object with an audio manager and set that as the instance
                    instance = new GameObject("NewGameManager", typeof(GameManager)).GetComponent<GameManager>();
                }
            }

            return instance;
        }
        //Ensures that set functionality can't be set be any other script
        private set{
            //Set the instance to a value
            instance = value;
        }
    }

    /* Other variables */
    [SerializeField] private AudioClip bgm;         //Reference to background music
    [SerializeField] private AudioClip battleTheme; //Battle theme when there are enemies in the scene
    [SerializeField] private GameObject winTreasure;    //Treasure to appear when all enemies are defeated

    private int enemiesInScene = 0;                     //Number of enemies currently in the scene
    private bool battleEngaged = false;                 //Has the player encountered enemies?

    private void Awake() {
        //Ensure the instance of the GameManager isn't destroyed
        DontDestroyOnLoad(this.gameObject);
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
            if(!battleEngaged){
                //Play the battle theme
                AudioManager.Instance.PlayMusicWithFade(battleTheme);
                //Set the battle engaged to true
                battleEngaged = true;
            }
        }else{
            //Play the level theme
            AudioManager.Instance.PlayMusicWithFade(bgm);
            //Player is no longer fighting an enemy
            battleEngaged = false;
        }
    }

}
