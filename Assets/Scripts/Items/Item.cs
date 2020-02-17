using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject 
{
    [Header("Item Details")]
    public string itemName = "ItemName";	        //Name of the item
	public int cost = 50;							//Cost of the item
	public string description;						//Description of the item
}

