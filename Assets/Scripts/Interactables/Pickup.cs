using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Check if a player collided with the pickup object
        if (other.CompareTag("Player"))
        {
            //Get the player script from the other object
            Player player = other.GetComponent<Player>();

            //Pickup the object
            PickupObject(player);
        }
    }

    protected virtual void PickupObject(Player player)
    {
        Debug.Log("Player is picking up this object");
    }
}
