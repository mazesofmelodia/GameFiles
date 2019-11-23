using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Interactable
{
    [SerializeField] private int restoreHealthValue = 0;    //Amount of health the pickup restores
    [SerializeField] private AudioClip pickupSound;         //Sound that plays when the player picks up the item

    public override void Interact(GameObject playerObject){
        //Get the player stats component on the player
        PlayerStats playerHealth = playerObject.GetComponent<PlayerStats>();
        //Increase the player's health by the value of this pickup
        playerHealth.RecoverHealth(restoreHealthValue);
        //Play the pickup sound
        AudioManager.Instance.PlaySFX(pickupSound);
        //Destroy this object
        Destroy(this.gameObject);
    }
}
