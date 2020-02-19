using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData generationData;    //Dungeon generation data

    private List<Vector2Int> dungeonRooms;          //List of dungeon rooms

    private void Start()
    {
        //Generate the list of room positions
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(generationData);

        //Set the boss room for this dungeon
        RoomController.instance.SetBossRoom(generationData.endRoomPrefab);

        //Spawn the rooms
        SpawnRooms(dungeonRooms);
    }

    //Spawn the rooms
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        //Load the start room
        RoomController.instance.LoadRoom(generationData.startRoomPrefab, 0, 0);

        //Loop through all location points in dungeon rooms
        foreach (Vector2Int roomLocation in rooms)
        {
            //Load a room in the roomLocation
            RoomController.instance.LoadRoom(generationData.GetRandomRoom(), roomLocation.x, roomLocation.y);
        }
    }
}
