using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSlot itemTreasure;     //Weapon treasure on this object
    [SerializeField] private AudioClip pickupSound;     //Item pickup sound
    [SerializeField] private AudioClip pickupFailSound;   //Sound that plays when the player can't pick up the item

    [Header("Event data")]
    [SerializeField] private AudioClipEvent playSFXEvent;
    [SerializeField] private VoidEvent onDestroyEvent;
    [SerializeField] private ItemSlotEvent onApproachEvent;
    [SerializeField] private VoidEvent onDistanceEvent;

    public void Interact(GameObject playerObject){
        if (itemTreasure.inventoryItem == null)
        {
            Debug.Log("Trying to pickup but nothing's there");

            return;
        }

        //Get references to both the player stats and attack scripts
        Player player = playerObject.GetComponent<Player>();

        if(player == null)
        {
            return;
        }

        //Change the weapon on the character
        CollectItem(player);
    }

    private void CollectItem(Player player){
        //Check if the inventory is full
        if (player.inventory.CheckIfInventoryFull())
        {
            //Play a sound informing the player that they don't have enough money
            playSFXEvent.Raise(pickupFailSound);
        }
        else
        {
            //Play the pickup sound
            playSFXEvent.Raise(pickupSound);
            //Change the weapon on the character for the new weapon
            player.inventory.AddItem(itemTreasure);
            //Destroy the game object
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(itemTreasure.inventoryItem == null)
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
