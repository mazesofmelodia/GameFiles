using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;    //Enemy to spawn

    public void SpawnEnemy()
    {
        //Spawn the enemy at the location of the spawner
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}
