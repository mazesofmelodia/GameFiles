using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float interactDistance = 1f;   //How close the player needs to be to interact with an object
    [SerializeField] private Transform interactPoint;       //Point that the interact raycast comes from

    // Update is called once per frame
    void Update()
    {
        //When the player presses the interact button
        if(Input.GetButtonDown("Interact")){
            //Check for an interaction
            CheckForInteraction();
        }
    }

    private void CheckForInteraction()
    {
        //Define raycast variable
        RaycastHit hit;

        Debug.DrawRay(interactPoint.position, interactPoint.forward * interactDistance, Color.green);
        //Check if the raycast hit an object
        if(Physics.Raycast(interactPoint.position, interactPoint.forward, out hit, interactDistance)){

            Interactable interactObject = hit.collider.gameObject.GetComponent<Interactable>();

            interactObject.Interact(this.gameObject);
        }
    }
}
