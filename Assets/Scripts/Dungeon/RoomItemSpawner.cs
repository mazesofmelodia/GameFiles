using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemSpawner : MonoBehaviour
{
    //List of item spawn points
    [SerializeField] private List<GameObject> itemSpawnPoints = new List<GameObject>();

    //List of shop items
    [SerializeField] private List<GameObject> items = new List<GameObject>();

    private void Start()
    {
        if(itemSpawnPoints.Count > 0)
        {
            //Loop through all of the spawnpoints
            foreach (GameObject spawnPoint in itemSpawnPoints)
            {
                //Select a random item
                GameObject randomObject = items[Random.Range(0, items.Count)];

                //Spawn the item at the shop point
                Instantiate(randomObject, spawnPoint.transform.position, Quaternion.identity);
            }
        }
    }
}
