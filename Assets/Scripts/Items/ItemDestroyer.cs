using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDestroyer : MonoBehaviour
{
    [SerializeField] private Inventory inventory = null;        //Inventory that the destroyer is interacting with
    [SerializeField] private TextMeshProUGUI areYouSureText;    //Confirm text for destroying an object

    private int slotIndex = 0;

    private void OnDisable()
    {
        //Set the slot index to -1 if disabled
        slotIndex = -1;
    }

    public void Activate(ItemSlot itemSlot, int slotIndex)
    {
        //set the slot index to the inventory slot index
        this.slotIndex = slotIndex;

        //Set the text to confirm what item you're deleting
        areYouSureText.text = $"Are you sure you want to destroy {itemSlot.quantity}x {itemSlot.inventoryItem.ColouredName}?";

        //Activate this textbox
        gameObject.SetActive(true);
    }

    public void Destroy()
    {
        //Remove the item from the item container
        inventory.ItemContainer.RemoveAt(slotIndex);

        //Deactivate the item destroyer
        gameObject.SetActive(false);
    }
}
