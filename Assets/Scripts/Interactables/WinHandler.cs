using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private VoidEvent winGameEvent;

    private void OnTriggerEnter(Collider other) {
        //Check if the object that collided with it was a player
        if(other.CompareTag("Player")){
            //Raise the win event
            winGameEvent.Raise();
        }
    }
}
