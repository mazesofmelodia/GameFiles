public interface IItemContainer
{
    //Add Item to Inventory
    ItemSlot AddItem(ItemSlot newItem);

    //Removes item from inventory
    void RemoveItem(ItemSlot itemToRemove);

    //Remove at a certain point in the inventory
    void RemoveAt(int slotIndex);

    //Swap the position of 2 item
    void Swap(int index1, int index2);

    //Check if the inventory contains a certain item
    bool HasItem(InventoryItem item);

    //Get the total quantity of an item
    int GetTotalQuantity(InventoryItem item);
}
