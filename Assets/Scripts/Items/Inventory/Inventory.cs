using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory",menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    //Item container for the inventory
    public ItemContainer ItemContainer { get; } = new ItemContainer(12);

    //Event for updating the inventory
    [SerializeField] VoidEvent onInventoryItemsUpdated = null;
    [SerializeField] private ItemSlot itemSlotTest = new ItemSlot();    //Test item slot to add to inventory

    public void OnEnable()
    {
        //Subscribe to the Delegate event on enable
        ItemContainer.OnItemsUpdated += onInventoryItemsUpdated.Raise;
    }

    public void OnDisable()
    {
        //Unsubscribe from the Delegate event on disable
        ItemContainer.OnItemsUpdated -= onInventoryItemsUpdated.Raise;
    }

    public void AddItem(ItemSlot newItem)
    {
        //Define a new Inventory Slot and add it to the inventory
        ItemContainer.AddItem(newItem);
    }

    [ContextMenu("Test Add")]
    public void TestAdd()
    {
        //Add the item to the item container
        ItemContainer.AddItem(itemSlotTest);
    }
}
