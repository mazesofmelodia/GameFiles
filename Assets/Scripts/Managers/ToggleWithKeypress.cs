using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWithKeypress : MonoBehaviour
{
    [SerializeField] private GameObject objectToToggle = null;

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
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
}
