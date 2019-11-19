using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Function to be overwritten with chilren of this class
    //Defines functionality for interaction.
    public virtual void Interact(GameObject playerObject){
        //Show that the interaction is working
        Debug.Log("Interacting....");
    }
}
