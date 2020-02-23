using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapon")]
public class Weapon : InventoryItem
{
    [Header("Weapon Details")]
    public int damage;                  //Weapon Damage
    public float attackSpeed;           //Time between attacks
    public float range;                 //Attack Range of weapon
    public GameObject weaponModel;      //Weapon model

    public override string GetInfoDisplayText()
    {
        //Create a string builder
        StringBuilder textBuilder = new StringBuilder();

        //Append the rarity name to the string builder
        textBuilder.Append(Rarity.name).AppendLine();

        //Append the use text to the string builder, color of text is dependent on use/rarity
        textBuilder.Append("<color=red>Damage: ").Append(damage.ToString()).Append("</color>").AppendLine();
        textBuilder.Append("<color=red>Attack Speed: ").Append(attackSpeed.ToString()).Append("</color>").AppendLine();
        textBuilder.Append("<color=red>Range: ").Append(range.ToString()).Append("</color>").AppendLine();

        //Append the max stack and sell price to the text
        textBuilder.Append("Sell Price ").Append(SellPrice).AppendLine();

        return textBuilder.ToString();
    }
}
