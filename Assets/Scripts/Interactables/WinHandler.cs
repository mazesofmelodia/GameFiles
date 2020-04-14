using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private VoidEvent winGameEvent;

    public void Interact(GameObject other)
    {
        //Check if the object that interacted with it was a player
        if (other.CompareTag("Player"))
        {
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
