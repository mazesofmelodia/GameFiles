using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName ="New Consumable Item", menuName ="Inventory/Consumable")]
public class ConsumableItem : InventoryItem
{
    [Header("Consumable Data")]
    [SerializeField] private string useText = "Use item";

    public override string GetInfoDisplayText()
    {
        //Create a string builder
        StringBuilder textBuilder = new StringBuilder();

        //Append the rarity name to the string builder
        textBuilder.Append(Rarity.name).AppendLine();

        //Append the use text to the string builder, color of text is dependent on use/rarity
        textBuilder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();

        //Append the max stack and sell price to the text
        textBuilder.Append("Max Stack: ").Append(MaxStack).AppendLine();
        textBuilder.Append("Sell Price ").Append(SellPrice).AppendLine();

        return textBuilder.ToString();
    }

    public override void UseItem()
    {
        Debug.Log($"Using {name}");
    }
}
