using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : Interactable
{
    [SerializeField] private int scoreIncreaseValue = 0;    //How much to increase the score by
    [SerializeField] private AudioClip pickupSound;         //Sound that plays when the player picks up the item

    public override void Interact(GameObject playerObject){
        //Get the player stats component on the player
        PlayerStats playerScore = playerObject.GetComponent<PlayerStats>();
        //Increase the player's score by the value of this pickup
        playerScore.UpdateScore(scoreIncreaseValue);
        //Play the pickup Sound
        AudioManager.Instance.PlaySFX(pickupSound);
        //Destroy this object
        Destroy(this.gameObject);
    }
}
