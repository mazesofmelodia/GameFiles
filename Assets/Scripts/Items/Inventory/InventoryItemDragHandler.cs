using UnityEngine.EventSystems;
using UnityEngine;

public class InventoryItemDragHandler : ItemDragHandler
{
    [SerializeField] ItemDestroyer itemDestroyer = null;    //Reference to the item destroyer

    public override void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //Call the base class
            base.OnPointerUp(eventData);

            //If there is no item we're hovering over
            if(eventData.hovered.Count == 0)
            {
                //Cast the ItemSlotUI as an Inventory slot
                InventorySlot thisSlot = ItemSlotUI as InventorySlot;

                //Activate the item destroyer taking in the item slot and the slot index
                itemDestroyer.Activate(thisSlot.ItemSlot, thisSlot.SlotIndex);
            }
        }
    }
}
