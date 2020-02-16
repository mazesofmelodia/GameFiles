[System.Serializable]
public struct ItemSlot
{
    public InventoryItem inventoryItem;     //Item in slot
    public int quantity;                    //Quantity of item

    //Constructor for item slot
    public ItemSlot(InventoryItem inventoryItem, int quantity)
    {
        //Set the input values to the ItemSlot
        this.inventoryItem = inventoryItem;
        this.quantity = quantity;
    }

    //Function to define what happens using the comparator operator between 2 item slots
    public static bool operator ==(ItemSlot a, ItemSlot b)
    {
        return a.Equals(b);
    }

    //Function to define what happens using the not comparator operator between 2 item slots
    public static bool operator !=(ItemSlot a, ItemSlot b)
    {
        return !a.Equals(b);
    }

    //Function to check how much space is left in an item slot
    public int CheckRemainingItemSpace()
    {
        //Take away the quantity from the max stack
        return this.inventoryItem.MaxStack - this.quantity;
    }
}
