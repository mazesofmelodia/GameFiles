using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        //Check if the object that collided with it was a player
        if(other.CompareTag("Player")){
            //Get a reference to the player stats
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();

            //Player wins the game
            player.WinGame();
        }
    }
}
