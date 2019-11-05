using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;    //Enemy to spawn
    //List of spawnpoints in the room
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private bool enemiesSpawned = false;    //Have the enemies already been spawned

    private void OnTriggerEnter(Collider other) {
        //If a player enters the room and the enemies have not been spawned
        if(other.CompareTag("Player") && !enemiesSpawned){
            //Spawn an enemy at each spawnpoint
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            //Enemies have been spawned
            enemiesSpawned = true;
        }
    }
}
