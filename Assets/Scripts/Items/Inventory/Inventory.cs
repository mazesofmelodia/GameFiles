using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour, IItemContainer
{
    [SerializeField] private int size = 20;
    //Event for updating the inventory
    [SerializeField] private UnityEvent onInventoryItemsUpdated = null;

    //Item slot array
    private ItemSlot[] itemSlots = new ItemSlot[0];

    public void Start()
    {
        //Set the size of the array based on size value
        itemSlots = new ItemSlot[size];
    }

    public ItemSlot GetItemByIndex(int slotIndex)
    {
        //Return the item in the slot index
        return itemSlots[slotIndex];
    }

    public bool CheckIfInventoryFull()
    {
        //Loop through all of the inventory slots
        for (int i = 0; i < itemSlots.Length; i++)
        {
            //Check if the item slot doesn't have an item
            if(itemSlots[i].inventoryItem == null)
            {
                //Return false, the inventory still has space left
                return false;
            }
        }

        //Return true, inventory doesn't have any space left
        return true;
    }

    public ItemSlot AddItem(ItemSlot itemSlot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            //If there is an item in this slot
            if (itemSlots[i].inventoryItem != null)
            {
                //if the item is the same as the newItem
                if (itemSlots[i].inventoryItem == itemSlot.inventoryItem)
                {
                    //Get a reference to the remaining item space
                    int slotRemainingSpace = itemSlots[i].CheckRemainingItemSpace();

                    //If the new item's quantity is less than the space remaining
                    if (itemSlot.quantity < slotRemainingSpace)
                    {
                        //Add the new item quantity to the item slot
                        itemSlots[i].quantity += itemSlot.quantity;

                        //Set the new item quantity to 0
                        itemSlot.quantity = 0;

                        onInventoryItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else if (slotRemainingSpace > 0)
                    {
                        //Increase the quantity items slot by the remaining space
                        itemSlots[i].quantity += slotRemainingSpace;

                        //Reduce the quantity of the new item by the remaining slot space
                        itemSlot.quantity -= slotRemainingSpace;
                    }
                }
            }
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            //If there's no item in the inventory slot
            if (itemSlots[i].inventoryItem == null)
            {
                //If new item's quantity is less than the item's max quantity
                if (itemSlot.quantity <= itemSlot.inventoryItem.MaxStack)
                {
                    //Add the new item to the slot
                    itemSlots[i] = itemSlot;

                    //Clear the new item quantity, the items have been added to the inventory
                    itemSlot.quantity = 0;

                    onInventoryItemsUpdated.Invoke();

                    return itemSlot;
                }
                else
                {
                    //Add a max stack of the new item to this item slot
                    itemSlots[i] = new ItemSlot(itemSlot.inventoryItem, itemSlot.inventoryItem.MaxStack);

                    //Reduce the quantity by the max quantity
                    itemSlot.quantity -= itemSlot.inventoryItem.MaxStack;
                }
            }
        }

        onInventoryItemsUpdated.Invoke();

        //Return the item
        return itemSlot;
    }

    public int GetTotalQuantity(InventoryItem item)
    {
        //Initalize a number for counting
        int totalCount = 0;

        //Loop through all items in the inventory
        foreach (ItemSlot itemSlot in itemSlots)
        {
            //Check if the item in the slot is null
            //Move to the next item if it is
            if (itemSlot.inventoryItem == null) { continue; }

            //Check if the item is what we are looking for
            //Move on to the next iteration if not
            if (itemSlot.inventoryItem != item) { continue; }

            //Otherwise add to the total count
            totalCount += itemSlot.quantity;
        }

        //return the number of items
        return totalCount;
    }

    public bool HasItem(InventoryItem item)
    {
        foreach (ItemSlot itemSlot in itemSlots)
        {
            //Check if the item in the slot is null
            //Move to the next item if it is
            if (itemSlot.inventoryItem == null) { continue; }

            //Check if the item is what we are looking for
            //Move on to the next iteration if not
            if (itemSlot.inventoryItem != item) { continue; }

            //Otherwise return true
            return true;
        }

        //If the item was not found
        return false;
    }

    public void RemoveAt(int slotIndex)
    {
        //Check if the item index is outside the range of the array
        if (slotIndex < 0 || slotIndex > itemSlots.Length - 1) { return; }

        //Clear the item slot
        itemSlots[slotIndex] = new ItemSlot();

        onInventoryItemsUpdated.Invoke();
    }

    public void RemoveItem(ItemSlot itemSlot)
    {
        //Loop through the inventory
        for (int i = 0; i < itemSlots.Length; i++)
        {
            //if the item isn't null
            if (itemSlots[i].inventoryItem != null)
            {
                //if the item is the same as the item to remove
                if (itemSlots[i].inventoryItem == itemSlot.inventoryItem)
                {
                    //if this item's quantity is lower than the item to remove quantory
                    if (itemSlots[i].quantity < itemSlot.quantity)
                    {
                        //Lower the quantity of the item to remove
                        itemSlot.quantity -= itemSlots[i].quantity;

                        //Clear the item slot
                        itemSlots[i] = new ItemSlot();


                    }
                    else
                    {
                        //Lower the quantity of the item
                        itemSlots[i].quantity -= itemSlot.quantity;

                        if (itemSlots[i].quantity == 0)
                        {
                            //Clear the item slot
                            itemSlots[i] = new ItemSlot();

                            onInventoryItemsUpdated.Invoke();

                            return;
                        }
                    }
                }
            }
        }
    }

    public void Swap(int index1, int index2)
    {
        //Get the items from both indexes
        ItemSlot firstSlot = itemSlots[index1];
        ItemSlot secondSlot = itemSlots[index2];

        //If both items are the same, return as nothing should happen
        if (firstSlot.Equals(secondSlot)) { return; }

        if (secondSlot.inventoryItem != null)
        {
            if (firstSlot.inventoryItem == secondSlot.inventoryItem)
            {
                //Check how much remaining space is in the second slot
                int secondSlotRemainingSpace = secondSlot.CheckRemainingItemSpace();

                //Check if the first slots quantity is lower than the remaining space
                if (firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    //Add the items to the second slot
                    secondSlot.quantity += firstSlot.quantity;

                    //Resets the first item slot
                    itemSlots[index1] = new ItemSlot();

                    onInventoryItemsUpdated.Invoke();

                    return;
                }
            }
        }

        //Put the second slot item in the index1 slot and the first slot item in the index2 slot
        itemSlots[index1] = secondSlot;
        itemSlots[index2] = firstSlot;

        onInventoryItemsUpdated.Invoke();
    }
}
