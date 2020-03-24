using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Inventory/Magic Spell")]
public class MagicSpell : InventoryItem
{
    [Header("Magic Spell Details")]
    public string description;
    public int baseDamage = 0;
    public int manaCost = 0;
    public float range = 1f;
    public CombatAction combatAction;

    [SerializeField] private MagicSpellEvent spellEvent;

    public override string GetInfoDisplayText()
    {
        //Create a string builder
        StringBuilder textBuilder = new StringBuilder();

        //Append the rarity name to the string builder
        textBuilder.Append(Rarity.name).AppendLine();

        //Append the use text to the string builder, color of text is dependent on use/rarity
        textBuilder.Append("<color=purple>").Append(description).Append("</color>").AppendLine();
        textBuilder.Append("<color=purple>Base Damage: ").Append(baseDamage.ToString()).AppendLine();
        textBuilder.Append("<color=purple>Mana Cost: ").Append(manaCost.ToString()).AppendLine();
        textBuilder.Append("<color=purple>Range: ").Append(range.ToString()).AppendLine();

        //Append the max stack and sell price to the text
        textBuilder.Append("Sell Price ").Append(SellPrice).AppendLine();

        return textBuilder.ToString();
    }

    public override void UseItem()
    {
        //Raise the spell event
        spellEvent.Raise(this);
    }
}
