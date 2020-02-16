using UnityEngine;

public abstract class InventoryItem : HotbarItem
{
    [Header("Item Data")]
    [SerializeField] private Rarity rarity;                     //Rarity of the item
    [SerializeField] [Min(0)] private int sellPrice = 1;        //Selling price of the item
    [SerializeField] [Min(1)] private int maxStack = 1;         //Max number of the items you can hold

    //Get the color of the text based on the rarity of the item
    public override string ColouredName
    {
        get{
            //Get the hex colour of the rarity textcolour
            string hexColour = ColorUtility.ToHtmlStringRGB(rarity.TextColour);

            //Return the text with that colour
            return $"<color=#{hexColour}>{ItemName}</color>";
        }
    }

    //Public access reference to the sell price
    public int SellPrice => sellPrice;
    //Public reference to the max stack of the item
    public int MaxStack => maxStack;
}
