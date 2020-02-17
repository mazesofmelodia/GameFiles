using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] protected Image itemIconImage = null;  //Image for the slot

    //Reference to the index number
    public int SlotIndex { get; private set; }

    //Hotbar item in the slot
    public abstract HotbarItem SlotItem { get; set; }

    //Function for when an item is dropped
    public abstract void OnDrop(PointerEventData eventData);

    //Function for updating the slot ui, can vary for children
    public abstract void UpdateSlotUI();

    protected virtual void EnableSlotUI(bool enable)
    {
        //Enable the item icon image
        itemIconImage.enabled = enable;
    }

    private void OnEnable()
    {
        //Update the slot ui
        UpdateSlotUI();
    }

    protected virtual void Start()
    {
        //set the index based on the sibling index of this slot
        SlotIndex = transform.GetSiblingIndex();

        //Update the slot ui
        UpdateSlotUI();
    }
}
