using UnityEngine;
public interface IInteractable
{
    //Function to be overwritten with chilren of this class
    //Defines functionality for interaction.
    void Interact(GameObject other);
}
