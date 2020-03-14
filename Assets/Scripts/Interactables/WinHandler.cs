using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private VoidEvent winGameEvent;

    private void OnTriggerEnter(Collider other) {
        //Check if the object that collided with it was a player
        if(other.CompareTag("Player")){
            //Get the player component
            Player player = other.GetComponent<Player>();

            //Set the player state to win
            player.playerState = PlayerState.Win;

            //Submit the players score
            player.SubmitScore();

            //Raise the win event
            winGameEvent.Raise();
        }
    }
}
