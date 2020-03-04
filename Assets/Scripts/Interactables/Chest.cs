using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{ 
    [Header("Item Drops")]
    //List of Loot from the chest
    [SerializeField] private List<Loot> lootItems = new List<Loot>();
    [SerializeField] private float itemDropRange = 2;     //Range of item drops

    private Animator anim;
    private bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact(GameObject other)
    {
        //If the chest hasn't been opened yet
        if (!isOpened)
        {
            //Play the chest open animation
            anim.SetTrigger("OpenChest");

            //Drop the items
            DropItems();
        }
    }

    //Functionality for dropping an item
    private void DropItems()
    {
        foreach (Loot lootItem in lootItems)
        {
            //Generate a random number from 0 to 100
            int randomChance = Random.Range(0, 101);

            //Check if the random chance is lower than the drop items drop chance
            if (randomChance < lootItem.dropChance)
            {
                //Spawn the item when the enemy died
                Vector3 randomPos = Random.insideUnitSphere * itemDropRange;

                //Make sure the y position is 0
                randomPos.y = 0;

                Instantiate(lootItem.dropItem, transform.position + randomPos, Quaternion.identity);
            }
        }
    }

}
