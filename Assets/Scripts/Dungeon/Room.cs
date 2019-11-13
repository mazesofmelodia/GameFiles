using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyPrefab;    //Enemy to spawn
    //List of spawnpoints in the room
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    //List of doors
    [SerializeField] private List<GameObject> doors = new List<GameObject>();

    //List of enemies in the room
    private List<EnemyStats> enemies = new List<EnemyStats>();

    private bool enemiesSpawned = false;    //Have the enemies already been spawned

    private void OnTriggerEnter(Collider other) {
        //If a player enters the room and the enemies have not been spawned
        if(other.CompareTag("Player") && !enemiesSpawned){
            //Spawn an enemy at each spawnpoint
            foreach (Transform spawnPoint in spawnPoints)
            {
                EnemyStats newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

                //Add the enemy to the enemies list
                enemies.Add(newEnemy);
            }
            //Enemies have been spawned
            enemiesSpawned = true;
        }
    }

    public void CloseDoors(){
        //Loop through all of the doors in the room
        foreach (GameObject door in doors)
        {
            //Activate the doors
            door.SetActive(true);
        }
    }

    public void OpenDoors(){
        //Loop through all of the doors in the room
        foreach (GameObject door in doors)
        {
            //Deactivate the doors
            door.SetActive(false);
        }
    }

    public void CheckEnemies(){
        if(enemies.Count == 0){
            OpenDoors();
        }
    }
}
