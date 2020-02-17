using System;

[System.Serializable]
public class ItemContainer : IItemContainer
{
    //Item slot array
    private ItemSlot[] items = new ItemSlot[0];

    //Method for setting the size of the inventory
    public ItemContainer(int size) => items = new ItemSlot[size];

    public Action OnItemsUpdated = delegate { };

    public ItemSlot GetItemByIndex(int slotIndex)
    {
        //Return the item in the slot index
        return items[slotIndex];
    }

    public ItemSlot AddItem(ItemSlot itemSlot)
    {
        for (int i = 0; i < items.Length; i++)
        {
            //If there is an item in this slot
            if(items[i].inventoryItem != null)
            {
                //if the item is the same as the newItem
                if(items[i].inventoryItem == itemSlot.inventoryItem)
                {
                    //Get a reference to the remaining item space
                    int slotRemainingSpace = items[i].CheckRemainingItemSpace();

                    //If the new item's quantity is less than the space remaining
                    if(itemSlot.quantity < slotRemainingSpace)
                    {
                        //Add the new item quantity to the item slot
                        items[i].quantity += itemSlot.quantity;

                        //Set the new item quantity to 0
                        itemSlot.quantity = 0;

                        OnItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else if(slotRemainingSpace > 0)
                    {
                        //Increase the quantity items slot by the remaining space
                        items[i].quantity += slotRemainingSpace;

                        //Reduce the quantity of the new item by the remaining slot space
                        itemSlot.quantity -= slotRemainingSpace;
                    }
                }
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            //If there's no item in the inventory slot
            if(items[i].inventoryItem == null)
            {
                //If new item's quantity is less than the item's max quantity
                if(itemSlot.quantity <= itemSlot.inventoryItem.MaxStack)
                {
                    //Add the new item to the slot
                    items[i] = itemSlot;

                    //Clear the new item quantity, the items have been added to the inventory
                    itemSlot.quantity = 0;

                    OnItemsUpdated.Invoke();

                    return itemSlot;
                }
                else
                {
                    //Add a max stack of the new item to this item slot
                    items[i] = new ItemSlot(itemSlot.inventoryItem, itemSlot.inventoryItem.MaxStack);

                    //Reduce the quantity by the max quantity
                    itemSlot.quantity -= itemSlot.inventoryItem.MaxStack;
                }
            }
        }

        OnItemsUpdated.Invoke();

        //Return the item
        return itemSlot;
    }

    public int GetTotalQuantity(InventoryItem item)
    {
        //Initalize a number for counting
        int totalCount = 0;

        //Loop through all items in the inventory
        foreach (ItemSlot itemSlot in items)
        {
            //Check if the item in the slot is null
            //Move to the next item if it is
            if(itemSlot.inventoryItem == null) { continue; }

            //Check if the item is what we are looking for
            //Move on to the next iteration if not
            if(itemSlot.inventoryItem != item) { continue; }

            //Otherwise add to the total count
            totalCount += itemSlot.quantity;
        }

        //return the number of items
        return totalCount;
    }

    public bool HasItem(InventoryItem item)
    {
        foreach (ItemSlot itemSlot in items)
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
        if (slotIndex < 0 || slotIndex > items.Length - 1) { return; }

        //Clear the item slot
        items[slotIndex] = new ItemSlot();

        OnItemsUpdated.Invoke();
    }

    public void RemoveItem(ItemSlot itemSlot)
    {
        //Loop through the inventory
        for (int i = 0; i < items.Length; i++)
        {
            //if the item isn't null
            if(items[i] != null)
            {
                //if the item is the same as the item to remove
                if(items[i].inventoryItem == itemSlot.inventoryItem)
                {
                    //if this item's quantity is lower than the item to remove quantory
                    if(items[i].quantity < itemSlot.quantity)
                    {
                        //Lower the quantity of the item to remove
                        itemSlot.quantity -= items[i].quantity;

                        //Clear the item slot
                        items[i] = new ItemSlot();

                        
                    }
                    else
                    {
                        //Lower the quantity of the item
                        items[i].quantity -= itemSlot.quantity;

                        if(items[i].quantity == 0)
                        {
                            //Clear the item slot
                            items[i] = new ItemSlot();

                            OnItemsUpdated.Invoke();

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
        ItemSlot firstSlot = items[index1];
        ItemSlot secondSlot = items[index2];

        //If both items are the same, return as nothing should happen
        if(firstSlot == secondSlot) { return; }

        if(secondSlot.inventoryItem != null)
        {
            if(firstSlot.inventoryItem == secondSlot.inventoryItem)
            {
                //Check how much remaining space is in the second slot
                int secondSlotRemainingSpace = secondSlot.CheckRemainingItemSpace();

                //Check if the first slots quantity is lower than the remaining space
                if(firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    //Add the items to the second slot
                    secondSlot.quantity += firstSlot.quantity;

                    //Resets the first item slot
                    items[index1] = new ItemSlot();

                    OnItemsUpdated.Invoke();

                    return;
                }
            }
        }

        //Put the second slot item in the index1 slot and the first slot item in the index2 slot
        items[index1] = secondSlot;
        items[index2] = firstSlot;

        OnItemsUpdated.Invoke();
    }
}
