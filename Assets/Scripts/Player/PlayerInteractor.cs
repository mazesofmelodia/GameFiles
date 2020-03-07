using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private IInteractable currentInteractable = null;
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for an interaction
        CheckForInteraction();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if the other object is interactable
        var interactable = other.GetComponent<IInteractable>();

        //if the object is not interactable
        if(interactable == null)
        {
            return;
        }

        //Otherwise set the interactable as the current interactable
        currentInteractable = interactable;
    }

    private void OnTriggerExit(Collider other)
    {
        //Check if the other object is interactable
        var interactable = other.GetComponent<IInteractable>();

        //if the object is not interactable
        if (interactable == null)
        {
            return;
        }

        //Check if the interactable is the current interactable
        if(interactable != currentInteractable)
        {
            return;
        }

        //Set the current interactable to null
        currentInteractable = null;
    }

    private void CheckForInteraction()
    {
        //Check if the current interactable is null
        if(currentInteractable == null)
        {
            return;
        }

        //When the player presses the interact button
        if (Input.GetButtonDown("Interact") && player.playerState == PlayerState.Active)
        {
            //Interact with the Interactable
            currentInteractable.Interact(transform.root.gameObject);
        }
    }

    public void SetInteractableToNull()
    {
        currentInteractable = null;
    }
}
