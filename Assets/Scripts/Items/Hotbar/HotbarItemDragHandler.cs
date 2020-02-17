using UnityEngine.EventSystems;

public class HotbarItemDragHandler : ItemDragHandler
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        //If the left mouse button is over the item
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerUp(eventData);

            //If we are not hovering over anything
            if(eventData.hovered.Count == 0)
            {
                //Clear the hotbar slot
                (ItemSlotUI as HotbarSlot).SlotItem = null;
            }
        }
    }
}
