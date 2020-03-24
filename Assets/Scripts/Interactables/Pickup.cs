using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int pickupValue = 0;           //Amount of health the pickup restores
    [SerializeField] private AudioClip pickupSound;         //Sound that plays when the player picks up the item
    [SerializeField] private AudioClipEvent playSFXEvent;   //Audio clip event
    [SerializeField] private IntEvent pickupEvent;          //Event to be called when the player picks up the object

    private void OnTriggerEnter(Collider other)
    {
        //Check if a player collided with the pickup object
        if (other.CompareTag("Player"))
        {
            //Pickup the object
            PickupObject();
        }
    }

    private void PickupObject()
    {
        //Player the pickup event
        pickupEvent.Raise(pickupValue);

        //Play the pickup sound
        playSFXEvent.Raise(pickupSound);

        //Destroy the object
        Destroy(this.gameObject);
    }
}
