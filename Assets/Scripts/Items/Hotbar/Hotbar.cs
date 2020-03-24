using UnityEngine;

public class Hotbar : MonoBehaviour
{
    //Array of hotbar items
    [SerializeField] private HotbarSlot[] hotbarSlots = new HotbarSlot[10];

    private int selectedHotbarSlot;         //Which Hotbar was selected

    private bool hotbarDisabled = false;    //is the hotbar disabled

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

    //Function to disable the hotbar
    public void HotbarToggle()
    {
        hotbarDisabled = !hotbarDisabled;
    }

    public void DisableHotbar()
    {
        hotbarDisabled = false;
    }

    public void SetInventoryReference(Inventory newInventory)
    {
        //Loop through all hotbar slots
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            //Link the inventory to the hotbar slot
            hotbarSlots[i].SetInventoryReference(newInventory);
        }
    }

    private void Update()
    {
        if (!hotbarDisabled)
        {
            ToggleCheck();
            UseItem();
        }
        
    }

    private void ToggleCheck()
    {
        //If the toggle buttons have been pressed
        if (Input.GetAxisRaw("Toggle") > 0)
        {
            ToggleHotbarItem(1);
        }
        else if (Input.GetAxisRaw("Toggle") < 0)
        {
            ToggleHotbarItem(-1);
        }
    }

    private void UseItem()
    {
        //Use the item
        if (Input.GetButtonDown("UseItem"))
        {
            //Use the item
            hotbarSlots[selectedHotbarSlot].UseSlot(selectedHotbarSlot);
        }
    }

    private void ToggleHotbarItem(int toggle)
    {
        //Store a reference to the previous hotbar slot
        int previousHotbarSlot = selectedHotbarSlot;

        //Toggle the selected hotbar slot
        selectedHotbarSlot += toggle;

        //Check if the hotbar slot index has gone below 0
        if(selectedHotbarSlot < 0)
        {
            //Set the selected hotbar slot to the max item
            selectedHotbarSlot = hotbarSlots.Length - 1;
        }
        //Check if the selected hotbar slot is beyond the array length
        else if(selectedHotbarSlot > hotbarSlots.Length - 1)
        {
            //Set the selected hotbar slot to the minimum item
            selectedHotbarSlot = 0;
        }

        //Unhighlight the previous slot
        hotbarSlots[previousHotbarSlot].UnHighlightSlot();

        //Highlight the selected slot
        hotbarSlots[selectedHotbarSlot].HighlightSlot();
    }
}
