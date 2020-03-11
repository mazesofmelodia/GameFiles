using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSlot itemTreasure;     //Weapon treasure on this object
    [SerializeField] private AudioClip pickupSound;     //Item pickup sound
    [SerializeField] private AudioClip pickupFailSound;   //Sound that plays when the player can't pick up the item

    [Header("Event data")]
    [SerializeField] private AudioClipEvent playSFXEvent;
    [SerializeField] private VoidEvent onDestroyEvent;
    [SerializeField] private ItemSlotEvent onApproachEvent;
    [SerializeField] private VoidEvent onDistanceEvent;

    public void Interact(GameObject playerObject)
    {
        if(itemTreasure.inventoryItem == null)
        {
            Debug.Log("Trying to purchase but nothing's there");

            return;
        }

        //Get references to both the player stats and attack scripts
        Player player = playerObject.GetComponent<Player>();

        if (player == null)
        {
            return;
        }

        //Check if the player has enough space for the item in their inventory
        if (player.inventory.CheckIfInventoryFull())
        {
            //Play a sound informing the player that their inventory is full
            playSFXEvent.Raise(pickupFailSound);

            return;
        }

        //Check if the player has the amount of points needed to buy the item
        if (player.GetScore() >= itemTreasure.inventoryItem.SellPrice * itemTreasure.quantity)
        {
            //Reduce the score from the player
            player.UpdateScore(-itemTreasure.inventoryItem.SellPrice * itemTreasure.quantity);
            //Add the item to the player's inventory
            CollectItem(player);
        }
        else
        {
            //Play a sound informing the player that they don't have enough money
            playSFXEvent.Raise(pickupFailSound);
        }
    }

    private void CollectItem(Player player)
    {
        //Play the pickup sound
        playSFXEvent.Raise(pickupSound);
        //Change the weapon on the character for the new weapon
        player.inventory.AddItem(itemTreasure);
        //Destroy the game object
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (itemTreasure.inventoryItem == null)
        {
            return;
        }

        //If the player approaches the item
        if (other.CompareTag("Player"))
        {
            //Display item information to the player
            onApproachEvent.Raise(itemTreasure);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player moves away from the shop item
        if (other.CompareTag("Player"))
        {
            //Hide item information
            onDistanceEvent.Raise();
        }
    }

    private void OnDestroy()
    {
        onDestroyEvent.Raise();
        onDistanceEvent.Raise();
    }
}
