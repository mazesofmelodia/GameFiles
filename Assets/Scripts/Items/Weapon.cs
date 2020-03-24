using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapon")]
public class Weapon : InventoryItem
{
    [Header("Weapon Details")]
    public int damageAddition;                      //Weapon Damage
    public float attackSpeed;               //Time between attacks
    public float range;                     //Attack Range of weapon
    public GameObject weaponModel;          //Weapon model
    public CombatAction combatAction;       //Combat action of the weapon
    public AudioClip weaponSound;           //Weapon sound effect
    [SerializeField] private WeaponEvent weaponChangeEvent;   //Event which is called to switch the weapon
    [SerializeField] private ItemSlotEvent useItemEvent;      //Event to use the item

    public override string GetInfoDisplayText()
    {
        //Create a string builder
        StringBuilder textBuilder = new StringBuilder();

        //Append the rarity name to the string builder
        textBuilder.Append(Rarity.name).AppendLine();

        //Append the use text to the string builder, color of text is dependent on use/rarity
        textBuilder.Append("<color=red>Damage: +").Append(damageAddition.ToString()).Append("</color>").AppendLine();
        textBuilder.Append("<color=red>Attack Speed: ").Append(attackSpeed.ToString()).Append("</color>").AppendLine();
        textBuilder.Append("<color=red>Range: ").Append(range.ToString()).Append("</color>").AppendLine();

        //Append the max stack and sell price to the text
        textBuilder.Append("Sell Price ").Append(SellPrice).AppendLine();

        return textBuilder.ToString();
    }

    public override void UseItem()
    {
        //Call event to change weapons
        weaponChangeEvent.Raise(this);
    }

    public void RemoveFromInventory()
    {
        //Call event to use item
        useItemEvent.Raise(new ItemSlot(this, 1));
    }
}
