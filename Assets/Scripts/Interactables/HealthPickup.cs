using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private int restoreHealthValue = 0;    //Amount of health the pickup restores
    [SerializeField] private AudioClip pickupSound;         //Sound that plays when the player picks up the item

    protected override void PickupObject(PlayerStats player)
    {
        //Increase the players health
        player.RecoverHealth(restoreHealthValue);

        //Play the pickup sound
        AudioManager.Instance.PlaySFX(pickupSound);

        //Destroy the object
        Destroy(this.gameObject);
    }
}
