using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Buff Item", menuName = "Inventory/StatBuff")]
public class StatBuffItem : InventoryItem
{
    [Header("Buff Item Details")]
    [SerializeField] private string useText = "Use item";
    [SerializeField] private StatBuff statBuff;             //Stat buff
    [SerializeField] ItemSlotEvent useItemEvent;            //Event to use the item
    [SerializeField] StatBuffEvent buffEvent;               //Stat buff event
    [SerializeField] private AudioClip useItemSound;                //Item use sfx
    [SerializeField] private AudioClipEvent playUseSoundEvent;  //Event to play the sfx

    public override string GetInfoDisplayText()
    {
        //Create a string builder
        StringBuilder textBuilder = new StringBuilder();

        //Append the rarity name to the string builder
        textBuilder.Append(Rarity.name).AppendLine();

        //Append the use text to the string builder, color of text is dependent on use/rarity
        textBuilder.Append("<color=blue>Use: ").Append(useText).Append("</color>").AppendLine();

        //Append the max stack and sell price to the text
        textBuilder.Append("Max Stack: ").Append(MaxStack).AppendLine();
        textBuilder.Append("Sell Price ").Append(SellPrice).AppendLine();

        return textBuilder.ToString();
    }

    public override void UseItem()
    {
        //Call stat buff event
        buffEvent.Raise(statBuff);

        //Call event to use item
        useItemEvent.Raise(new ItemSlot(this, 1));

        //Play the item use sound
        playUseSoundEvent.Raise(useItemSound);
    }

}
