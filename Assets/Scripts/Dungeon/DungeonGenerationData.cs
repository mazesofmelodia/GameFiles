using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationData/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    public string levelName;

    public int numberOfCrawlers = 0;   //How many crawlers are in the level
    public int iterationMin = 0;        //Minimum number of iterations
    public int iterationMax = 0;        //Maximum number of iterations

    [Header("Room Prefabs for Level")]
    public GameObject startRoomPrefab;          //Start Room
    public GameObject endRoomPrefab;            //End room
    public GameObject shopRoomPrefab;           //Shop room
    public List<GameObject> randomRoomPrefabs;  //Random rooms to select from

    public GameObject GetRandomRoom()
    {
        //Select a room object from the randomRoomPrefabs list
        GameObject randomRoom = randomRoomPrefabs[Random.Range(0, randomRoomPrefabs.Count)];

        //Return the room
        return randomRoom;
    }
}
