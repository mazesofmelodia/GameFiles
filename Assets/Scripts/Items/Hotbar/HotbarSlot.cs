﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HotbarSlot : ItemSlotUI, IDropHandler
{
    //Inventory that this hotbar item is connected to
    [SerializeField] private Inventory inventory = null;

    //Quantity Text for the hotbar, shows how many items total are in the inventory
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;
    [SerializeField] private bool highlighted = false;     //Check to see if the slot is highlighted

    [Header("Hotbar highlighting")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private AudioClip highlightSFX;
    [SerializeField] private AudioClipEvent playSFXEvent;
    [SerializeField] private StringEvent showItemNameEvent;

    private HotbarItem slotItem = null;       //Hotbar item this slot is referring to

    //Public getter and setter for slot item
    public override HotbarItem SlotItem {
        get
        {
            //Return the hotbar item in this slot
            return slotItem;
        }
        set
        {
            //Set the hotbar item to this value
            slotItem = value;

            //Update the SlotUI
            UpdateSlotUI();
        }
    }

    public void SetInventoryReference(Inventory newInventory)
    {
        //Set the inventory to be the new inventory
        inventory = newInventory;
    }

    public bool AddItem(HotbarItem itemToAdd)
    {
        //If there is an item in the slot
        if(SlotItem != null)
        {
            //Don't addd the item and exit the function
            return false;
        }

        //Set the slot item to be the item to add
        SlotItem = itemToAdd;

        //Item was added
        return true;
    }

    public void UseSlot(int index)
    {
        //If the index is not this slot index
        if(index != SlotIndex)
        {
            //Ignore the function
            return;
        }

        //Use item
        if (slotItem != null)
        {
            slotItem.UseItem();

            UpdateSlotUI();
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        //Get the item drag handler
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        //If the item drag handler is null
        if(itemDragHandler == null)
        {
            //Ignore the rest of the function
            return;
        }

        //Check if the item you dropped is an inventory item
        InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
        //If the item is an inventory slot
        if (inventorySlot != null)
        {
            //Set the slot item to be the inventory item
            SlotItem = inventorySlot.ItemSlot.inventoryItem;

            //Exit the function
            return;
        }

        //Check if the item you dropped is a Hotbar item
        HotbarSlot hotbarSlot = itemDragHandler.ItemSlotUI as HotbarSlot;
        //If the item is a hotbar slot
        if(hotbarSlot != null)
        {
            //Store the old item
            HotbarItem oldItem = SlotItem;

            //Set the slot as the new item
            SlotItem = hotbarSlot.SlotItem;

            //Set the hotbar slot item to the old item
            hotbarSlot.SlotItem = oldItem;

            //Exit the function
            return;
        }
    }

    public override void UpdateSlotUI()
    {
        //If this item slot is highlighted
        if (highlighted)
        {
            ToggleHotbarItemText();
        }

        //If there is no slot item
        if(SlotItem == null)
        {
            //Disable the slot ui
            EnableSlotUI(false);

            return;
        }

        //Set the sprite of the hotbar slot to the slot item icon
        itemIconImage.sprite = SlotItem.ItemIcon;

        //Activate the slot UI
        EnableSlotUI(true);

        //Set the quantity of the item
        SetItemQuantityUI();
    }

    private void SetItemQuantityUI()
    {
        //Check if the Slot item is an inventory item
        if(SlotItem is InventoryItem inventoryItem)
        {
            //If the inventory has the item
            if (inventory.HasItem(inventoryItem))
            {
                //Get the total quantity of the inventory item in the inventory
                int quantity = inventory.GetTotalQuantity(inventoryItem);

                //Set the quantity text to the quantity, leave empty if quantity is 1
                itemQuantityText.text = quantity > 1 ? quantity.ToString() : "";
            }
            else
            {
                //Set the SlotItem to null
                SlotItem = null;
            }
        }
        else
        {
            //Disable the quantity text
            itemQuantityText.enabled = false;
        }
    }

    protected override void EnableSlotUI(bool enable)
    {
        base.EnableSlotUI(enable);
        //Enable or disable the quantity text
        itemQuantityText.enabled = enable;
    }

    public void HighlightSlot()
    {
        //Change the background image color to the highlight color
        backgroundImage.color = highlightColor;

        //Play the highlight sfx event
        playSFXEvent.Raise(highlightSFX);

        //Set the highlighted bool to true
        highlighted = true;

        //Toggle the hotbar item text
        ToggleHotbarItemText();
    }

    public void UnHighlightSlot()
    {
        //Change the background image color to the default color
        backgroundImage.color = defaultColor;

        //Set the highlighted bool to true
        highlighted = false;
    }

    //Manages setting the item hover text on the hotbar
    private void ToggleHotbarItemText()
    {
        if(SlotItem != null)
        {
            showItemNameEvent.Raise(SlotItem.ItemName);
        }
        else
        {
            showItemNameEvent.Raise("");
        }
    }
}
