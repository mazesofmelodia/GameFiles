using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : ItemSlotUI, IDropHandler
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;

    //Inventory slot item
    public override HotbarItem SlotItem
    {
        get
        {
            //Get the inventory item in this slot
            return ItemSlot.inventoryItem;
        }

        set
        {

        }
    }

    //Get the item slot based on its position within the index
    public ItemSlot ItemSlot => inventory.GetItemByIndex(SlotIndex);

    public void SetInventoryReference(Inventory newInventory)
    {
        //Link the inventory to this inventory slot
        inventory = newInventory;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        //Get the item drag handler from the event data
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        //If the drag handler is null
        if(itemDragHandler == null)
        {
            return;
        }
        
        //We dropped the inventory slot onto another inventory slot
        if((itemDragHandler.ItemSlotUI as InventorySlot) != null)
        {
            //Sawp the items
            inventory.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
        }
    }

    //Function to update the slot ui
    public override void UpdateSlotUI()
    {
        //If there is no item in the item slot
        if(ItemSlot.inventoryItem == false)
        {
            //Disable the Slot UI
            EnableSlotUI(false);
            return;
        }

        //Otherwise enable the Slot UI
        EnableSlotUI(true);

        //Set the sprite to be the item icon
        itemIconImage.sprite = ItemSlot.inventoryItem.ItemIcon;

        //Set the quantity text base on if the quantity is higher than one
        itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
    }

    protected override void EnableSlotUI(bool enable)
    {
        //Enable the UI based on the value of enable
        base.EnableSlotUI(enable);
        itemQuantityText.enabled = enable;
    }
}
