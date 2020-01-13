using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Direction enum
public enum Direction
{
    Up = 0,
    Left = 1,
    Down = 2,
    Right = 3
};

public class DungeonCrawlerController : MonoBehaviour
{
    //List of Vector2 points represented as integers
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();

    //Dictionary to map vector2int directions to Direction enum values
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.Up, Vector2Int.up},
        {Direction.Left, Vector2Int.left},
        {Direction.Down, Vector2Int.down},
        {Direction.Right, Vector2Int.right}
    };

    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        //Define list of dungeon crawlers
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();

        for (int i = 0; i < dungeonData.numberOfCrawlers; i++)
        {
            //Add a dungeon crawler to the list starting at zero
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }

        //Define the number of iterations
        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);

        //Loop through based on the number of iterations
        for (int i = 0; i < iterations; i++)
        {
            //Loop through the dungeon crawlers in the scene
            foreach(DungeonCrawler dungeonCrawler in dungeonCrawlers)
            {
                //Define a new position for the map
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);

                //Add the new position to the positions visited to avoid overlap
                positionsVisited.Add(newPos);
            }
        }

        return positionsVisited;

    }
}
