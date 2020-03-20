using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private StringEvent nextLevelEvent;

    public void Interact(GameObject other)
    {
        //Check if it was the player who interacted with the
        if (other.CompareTag("Player"))
        {
            //Call an event to load the next level
            nextLevelEvent.Raise(nextLevelName);
        }
    }
}
