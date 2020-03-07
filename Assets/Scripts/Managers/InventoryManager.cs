using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject objectToToggle = null;

    public void ToggleActive()
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);

        if (objectToToggle.activeSelf)
        {
            //Enable the Cursor and unlock the mouse
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //Hide the mouse cursor and lock it in place
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
