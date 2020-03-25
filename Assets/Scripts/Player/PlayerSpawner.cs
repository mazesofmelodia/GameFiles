using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    //The character that the player will play as
    public static Character SelectedCharacter = null;


    [SerializeField] private Character defaultCharacter;       //Fallback character selection
    [SerializeField] private TransformEvent setPlayerPosEvent;
    [SerializeField] private VoidEvent startPlayerEvent;

    public static bool PlayerSpawned = false;              //Player has spawned

    private void Start()
    {
        //If the player has been spawned
        if (PlayerSpawned)
        {
            Debug.Log("Setting up player in new scene");
            //Call the event to set the character's position
            setPlayerPosEvent.Raise(transform);

            //Call the event to start up the player
            startPlayerEvent.Raise();
        }
        else
        {
            if(SelectedCharacter == null)
            {
                //Set the default character to be the selected character
                SelectedCharacter = defaultCharacter;
            }

            //Spawn the player prefab
            GameObject playerObject = Instantiate(SelectedCharacter.characterObject, transform.position, transform.rotation);

            //Set the player object not to be destroyed on load
            DontDestroyOnLoad(playerObject);

            PlayerSpawned = true;
        }
    }
}
