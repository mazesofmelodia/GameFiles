using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArImageLoader : MonoBehaviour
{
    [SerializeField] private Image scannerImage;

    public void DisplayScannerIcon(HotbarItem hotbarItem)
    {
        //Set the scanner image sprite to be the scanner icon
        scannerImage.sprite = hotbarItem.ScannerIcon;
    }

    public void HideScannerIcon()
    {
        //Hide the scanner icon
        scannerImage.sprite = null;
    }
}
