using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Interactable
{
    [SerializeField] private Weapon weaponTreasure;     //Weapon treasure on this object
    [SerializeField] private bool isShopItem = false;   //Is this an Item in a shop

    public override void Interact(GameObject playerObject){
        //Get references to both the player stats and attack scripts
        PlayerStats playerScore = playerObject.GetComponent<PlayerStats>();
        PlayerAttack playerWeapon = playerObject.GetComponent<PlayerAttack>();

        //Check if this is a shop item
        if(isShopItem){
            //Check if the player has the amount of points needed to buy the item
            if(playerScore.GetScore() >= weaponTreasure.cost){
                //Reduce the score from the player
                playerScore.UpdateScore(-weaponTreasure.cost);
                //Change the weapon on the character
                ChangeWeapon(playerWeapon);
            }else{
                Debug.Log("Not enough score");
            }
        }else{
            //Change the weapon on the character
            ChangeWeapon(playerWeapon);
        }
    }

    private void ChangeWeapon(PlayerAttack playerWeapon){
        //Change the weapon on the character for the new weapon
        playerWeapon.ChangeWeapon(weaponTreasure);
        //Destroy the game object
        Destroy(this.gameObject);
    }
}
