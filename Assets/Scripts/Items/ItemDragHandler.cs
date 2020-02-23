using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    [SerializeField] protected ItemSlotUI itemSlotUI = null;    //UI of the item slot

    //Events for when the user is hovering over the item and when they stop
    [SerializeField] protected HotbarItemEvent onMouseStartHoverItem = null;
    [SerializeField] protected VoidEvent onMouseEndHoverItem = null;

    private CanvasGroup canvasGroup = null;     //Canvas group of the item
    private Transform originalParent = null;    //Original parent of the item
    private bool isHovering = false;            //Is the item hovering

    public ItemSlotUI ItemSlotUI => itemSlotUI;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //When the Handler is disabled
    private void OnDisable()
    {
        if (isHovering)
        {
            //Call Event
            onMouseEndHoverItem.Raise();

            //Hovering is now false
            isHovering = false;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //Raise event
            onMouseEndHoverItem.Raise();

            //Record the original parent
            originalParent = transform.parent;

            //Set the current parent to be the parent of the parent
            transform.SetParent(transform.parent.parent);

            //Make sure the canvas group oesn't block raycasts
            canvasGroup.blocksRaycasts = false;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        //If the left mouse button is held down
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //Set the position of the item to where the mouse is
            transform.position = Input.mousePosition;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        //If the left mouse button was released
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //Set the parent back to the original parent
            transform.SetParent(originalParent);

            //Zero the position of the item
            transform.localPosition = Vector3.zero;

            //Canvas group now blocks raycasts again
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //raise event displaying item info
        onMouseStartHoverItem.Raise(itemSlotUI.SlotItem);

        //Item is now hovering
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //raise event
        onMouseEndHoverItem.Raise();

        //Item is not hovering
        isHovering = false;
    }
}
