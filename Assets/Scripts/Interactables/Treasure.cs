using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Interactable
{
    [SerializeField] private ItemSlot itemTreasure;     //Weapon treasure on this object
    [SerializeField] private bool isShopItem = false;   //Is this an Item in a shop
    [SerializeField] private AudioClip pickupSound;     //Item pickup sound
    [SerializeField] private AudioClip purchaseFailSound;   //Sound that plays when the player doesn't have enough money

    [Header("Event data")]
    [SerializeField] private AudioClipEvent playSFXEvent;

    public override void Interact(GameObject playerObject){
        //Get references to both the player stats and attack scripts
        Player player = playerObject.GetComponent<Player>();

        //Check if this is a shop item
        if(isShopItem){
            //Check if the player has the amount of points needed to buy the item
            if(player.GetScore() >= itemTreasure.inventoryItem.SellPrice){
                //Reduce the score from the player
                player.UpdateScore(-itemTreasure.inventoryItem.SellPrice);
                //Add the item to the player's inventory
                CollectItem(player);
            }else{
                //Play a sound informing the player that they don't have enough money
                playSFXEvent.Raise(purchaseFailSound);
            }
        }else{
            //Change the weapon on the character
            CollectItem(player);
        }
    }

    private void CollectItem(Player player){
        //Play the pickup sound
        playSFXEvent.Raise(pickupSound);
        //Change the weapon on the character for the new weapon
        player.inventory.AddItem(itemTreasure);
        //Destroy the game object
        Destroy(this.gameObject);
    }
}
