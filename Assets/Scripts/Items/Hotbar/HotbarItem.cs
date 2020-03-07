using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HotbarItem : ScriptableObject
{
    [Header("Basic")]
    [SerializeField] private string itemName = "Hotbar Item Name";  //Name of the item
    [SerializeField] private Sprite itemIcon;                       //Item icon
    [SerializeField] private Sprite scannerIcon;                    //Icon for the ar scanner

    //Getters for values
    public string ItemName => itemName;             //Reference to item name
    public abstract string ColouredName { get; }    //Colour of text based on external factor
    public Sprite ItemIcon => itemIcon;             //Reference to item icon
    public Sprite ScannerIcon => scannerIcon;       //Reference to the scanner icon

    //Display info text, will vary between children
    public abstract string GetInfoDisplayText();

    public abstract void UseItem();

}
