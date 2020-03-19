using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreasureItemInteract : MonoBehaviour
{
    [SerializeField] private GameObject popupCanvasObject = null;       //Canvas of popup object
    [SerializeField] private RectTransform popupObject = null;          //Object to display on popup
    [SerializeField] private TextMeshProUGUI infoText = null;           //Text to display

    //Deactivate the canvas object
    public void HideInfo() => popupCanvasObject.SetActive(false);

    //Display the treasure item near the player
    public void DisplayTreasureInfo(ItemSlot item)
    {
        //Create a new string builder
        StringBuilder builder = new StringBuilder();

        //Get the colored text of the item
        builder.Append("<size=35>").Append(item.inventoryItem.ColouredName).Append("</size>\n");

        //Get the display text from the item
        builder.Append(item.inventoryItem.Rarity.Name).AppendLine();

        //Display the quantity of the item
        builder.Append($"Quantity: {item.quantity}").AppendLine();

        //Set the text based on the built text
        infoText.text = builder.ToString();

        //Activate the popup object
        popupCanvasObject.SetActive(true);

        //Rebuild the layout of the window
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }

    //Display the treasure item near the player
    public void DisplayShopItemInfo(ItemSlot item)
    {
        //Create a new string builder
        StringBuilder builder = new StringBuilder();

        //Get the colored text of the item
        builder.Append("<size=35>").Append(item.inventoryItem.ColouredName).Append("</size>\n");

        //Get the display text from the item
        builder.Append(item.inventoryItem.Rarity.Name).AppendLine();

        //Display the quantity of the item
        builder.Append($"Quantity: {item.quantity}").AppendLine();

        //Display the price of the item
        builder.Append($"Price: {item.inventoryItem.SellPrice * item.quantity}").AppendLine();

        //Set the text based on the built text
        infoText.text = builder.ToString();

        //Activate the popup object
        popupCanvasObject.SetActive(true);

        //Rebuild the layout of the window
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }

    public void DisplayUpgradeInfo(StatBuff statBuff)
    {
        //Create a new string builder
        StringBuilder builder = new StringBuilder();

        //Get the colored text of the item
        builder.Append("<size=35>").Append("Blessing of ").Append(statBuff.StatType.ToString()).Append("</size>\n");

        builder.Append(statBuff.GetUpgradeInfo()).AppendLine();

        //Set the text based on the built text
        infoText.text = builder.ToString();

        //Activate the popup object
        popupCanvasObject.SetActive(true);

        //Rebuild the layout of the window
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }
}
