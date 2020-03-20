using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;       //Player Prefab Object
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
            //Spawn the player prefab
            GameObject playerObject = Instantiate(playerPrefab, transform.position, transform.rotation);

            //Set the player object not to be destroyed on load
            DontDestroyOnLoad(playerObject);

            PlayerSpawned = true;
        }
    }
}
