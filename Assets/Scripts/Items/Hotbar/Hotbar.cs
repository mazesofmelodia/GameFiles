using UnityEngine;

public class Hotbar : MonoBehaviour
{
    //Array of hotbar items
    [SerializeField] private HotbarSlot[] hotbarSlots = new HotbarSlot[10];

    public void Add(HotbarItem itemToAdd)
    {
        //Loop through all hotbar items
        foreach (HotbarSlot hotbarSlot in hotbarSlots)
        {
            //If successsfully add the item to the slot
            if (hotbarSlot.AddItem(itemToAdd))
            {
                //Exit the loop
                return;
            }
        }
    }
}
