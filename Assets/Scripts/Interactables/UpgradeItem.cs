using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem : MonoBehaviour, IInteractable
{
    [SerializeField] private StatBuff statBuff;         //Stat buff of this upgrade item
    [SerializeField] private AudioClip pickupSound;     //Interact sound

    [Header("Event data")]
    [SerializeField] private AudioClipEvent playSFXEvent;
    [SerializeField] private VoidEvent onDestroyEvent;
    [SerializeField] private StatBuffEvent onApproachEvent;
    [SerializeField] private VoidEvent onDistanceEvent;
    [SerializeField] private VoidEvent onUpgradeTakenEvent;

    public void Interact(GameObject other)
    {
        if (statBuff == null)
        {
            Debug.Log("Trying to upgrade but nothing's there");

            return;
        }

        //Get reference to the player
        Player player = other.GetComponent<Player>();

        if (player == null)
        {
            return;
        }

        //Add the upgrade to the player
        player.UpgradeStat(statBuff);

        //Play the pickup sound
        playSFXEvent.Raise(pickupSound);

        //Call the upgrade taken event
        onUpgradeTakenEvent.Raise();

        //Destroy this object
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        onDestroyEvent.Raise();
        onDistanceEvent.Raise();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If there is no stat buff
        if(statBuff == null)
        {
            return;
        }

        //If the player approaches the upgrade
        if (other.CompareTag("Player"))
        {
            //Display upgrade information to the player
            onApproachEvent.Raise(statBuff);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player moves away from the shop item
        if (other.CompareTag("Player"))
        {
            //Hide item information
            onDistanceEvent.Raise();
        }
    }


}
