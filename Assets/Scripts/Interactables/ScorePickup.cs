using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : Pickup
{
    [SerializeField] private int scoreIncreaseValue = 0;    //How much to increase the score by
    [SerializeField] private AudioClip pickupSound;         //Sound that plays when the player picks up the item

    protected override void PickupObject(PlayerStats player)
    {
        //Increase the players score
        player.UpdateScore(scoreIncreaseValue);

        //Play the pickup sound
        AudioManager.Instance.PlaySFX(pickupSound);

        //Destroy the object
        Destroy(this.gameObject);
    }
}
