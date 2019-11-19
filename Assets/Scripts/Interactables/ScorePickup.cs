using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : Interactable
{
    [SerializeField] private int scoreIncreaseValue = 0;    //How much to increase the score by

    public override void Interact(GameObject playerObject){
        //Get the player stats component on the player
        PlayerStats playerScore = playerObject.GetComponent<PlayerStats>();
        //Increase the player's score by the value of this pickup
        playerScore.UpdateScore(scoreIncreaseValue);
        //Destroy this object
        Destroy(this.gameObject);
    }
}
