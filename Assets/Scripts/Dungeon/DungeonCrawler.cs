using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawler : MonoBehaviour
{
    public Vector2Int Position { get; set; }     //Position of the dungeon crawler

    public DungeonCrawler(Vector2Int startPos)
    {
        //Set the position of the crawler to the input position
        Position = startPos;
    }

    //Move the crawler based on movement map
    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        //Define a direction to move
        Direction toMove = (Direction) Random.Range(0, directionMovementMap.Count);

        //Move the position in that direction
        Position += directionMovementMap[toMove];

        //Return the new position
        return Position;
    }
}
