using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotHolder : MonoBehaviour
{
    //Array of inventory slots
    [SerializeField] private InventorySlot[] inventorySlots = new InventorySlot[12];

    //Link all of the inventory slots to the inventory
    public void SetInventoryReference(Inventory newInventory)
    {
        //Loop through all inventory slots
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            //Set the inventory reference on this inventory slot
            inventorySlots[i].SetInventoryReference(newInventory);
        }
    }
}
