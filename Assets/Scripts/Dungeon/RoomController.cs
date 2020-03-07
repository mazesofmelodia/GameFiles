using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// Information on the room
/// </summary>
public class RoomInfo
{
    public GameObject roomObject;     //Room object
    public int X;           //Room x value
    public int Y;           //Room y value
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;      //Singleton Instance of the Room Controller

    private GameObject endRoomObject;
    private GameObject shopRoomObject;

    RoomInfo currentLoadRoomData;               //Info of room being loaded

    //Queue of rooms to be loaded, ensure they load in order
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    //List of rooms loaded
    public List<Room> loadedRooms = new List<Room>();

    public VoidEvent navMeshEvent;

    bool isLoadingRoom = false;                 //Check if a room is being loaded
    bool spawnedBossRoom = false;               //Check if the boss room has spawned
    bool spawnedShopRoom = false;               //Check if the shop room has been spawned
    bool updatedRooms = false;                  //Check to see if we updated the rooms

    private void Awake()
    {
        //Check if there is no instance in the scene
        if(instance == null)
        {
            //Make this Room Controller the instance
            instance = this;
        }
        //If there's already an instance
        else if(instance != null)
        {
            //Destroy this game object
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        //Update the room queue
        UpdateRoomQueue();
    }

    private void UpdateRoomQueue()
    {
        //Check if a room is being loaded
        if (isLoadingRoom)
        {
            //Exit the function
            return;
        }

        //If there is no rooms in the room queue
        if(loadRoomQueue.Count == 0)
        {
            //Check to see if a boss room has been spawned
            if (!spawnedBossRoom)
            {
                SpawnBossRoom(endRoomObject);
            }
            //Check if the shop room has been spawned
            else if (!spawnedShopRoom)
            {
                SpawnShopRoom(shopRoomObject);
            }
            //Check if the boss room has been added but the rooms have not been updated
            else if(spawnedBossRoom && spawnedShopRoom && !updatedRooms)
            {
                //Loop through all rooms
                foreach (Room room in loadedRooms)
                {
                    //Remove unneeded doors from the room
                    room.RemoveUnconnectedDoors();
                }
                //Rooms have now been updated
                updatedRooms = true;

                navMeshEvent.Raise();
            }
            //Exit the function
            return;
        }

        //Set the current load room data to be the next item out of the queue
        currentLoadRoomData = loadRoomQueue.Dequeue();

        //Currently loading room
        isLoadingRoom = true;

        //Load the room based on the data
        //StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
        SpawnRoom(currentLoadRoomData.roomObject);
    }

    //Sets the boss room object to spawn at the end
    public void SetBossRoom(GameObject bossRoomPrefab)
    {
        endRoomObject = bossRoomPrefab;
    }

    //Sets the shop room object to spawn within the dungeon
    public void SetShopRoom(GameObject shopRoomPrefab)
    {
        shopRoomObject = shopRoomPrefab;
    }

    //Loads in new room info
    public void LoadRoom(GameObject roomObject, int x, int y)
    {
        //Check if the room exists
        if (DoesRoomExist(x, y))
        {
            //Exit the function as a room is at that position
            return;
        }

        //Creates new RoomInfo
        RoomInfo newRoomData = new RoomInfo()
        {
            //Sets the room object, x and y values based on input
            roomObject = roomObject,
            X = x,
            Y = y
        };

        //Adds the newRoomData to the loadRoomQueue
        loadRoomQueue.Enqueue(newRoomData);
    }

    private void SpawnRoom(GameObject newRoom)
    {
        //Spawn the room prefab in the scene
        Instantiate(newRoom);
    }

    public void SpawnBossRoom(GameObject bossRoomObject)
    {
        //The boss room has now been spawned
        spawnedBossRoom = true;

        //If the load room queue empty
        if(loadRoomQueue.Count == 0)
        {
            //Set the boss room to be the last room spawned
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];

            //Define a temporary room to record x and y values
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);

            //Destroy the boss room
            Destroy(bossRoom.gameObject);

            //Find the room to replace by comparing x and y values of the temp room
            var roomToReplace = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);

            //Remove the room from the list
            loadedRooms.Remove(roomToReplace);

            //Load the end room
            LoadRoom(bossRoomObject, tempRoom.X, tempRoom.Y);
        }
    }

    public void SpawnShopRoom(GameObject bossRoomObject)
    {
        //The shop room has now been spawned
        spawnedShopRoom = true;

        //If the load room queue empty
        if (loadRoomQueue.Count == 0)
        {
            //Set the shop room to be a room of the selected range
            //Range is above halfway but before the boss room
            Room shopRoom = loadedRooms[Random.Range(Mathf.RoundToInt(loadedRooms.Count / 2), loadedRooms.Count - 2)];

            //Define a temporary room to record x and y values
            Room tempRoom = new Room(shopRoom.X, shopRoom.Y);

            //Destroy the boss room
            Destroy(shopRoom.gameObject);

            //Find the room to replace by comparing x and y values of the temp room
            var roomToReplace = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);

            //Remove the room from the list
            loadedRooms.Remove(roomToReplace);

            //Load the end room
            LoadRoom(bossRoomObject, tempRoom.X, tempRoom.Y);
        }
    }



    public void RegisterRoom(Room room)
    {
        //Check if a room is not at the location
        if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            //Sets the position of the room based on coordinates, width and height of the room
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.width,
                0, currentLoadRoomData.Y * room.height);

            //Set the x and y coordinates of the room based on the load room data
            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;

            //Set the name of the room
            room.name = currentLoadRoomData.roomObject.name + "( " + room.X + " , " + room.Y + " )";

            //Sets the parent of the room to this controller
            room.transform.parent = this.transform;

            //Not currently loading a room
            isLoadingRoom = false;

            //Add the loaded room to the list of loaded room
            loadedRooms.Add(room);
        }
        else
        {
            //Destroy the game object
            Destroy(room.gameObject);

            //Not currently loading a room
            isLoadingRoom = false;
        }
    }
    
    /// <summary>
    /// Check if a room exists at a coordinate point
    /// </summary>
    /// <param name="x">X point</param>
    /// <param name="y">Y point</param>
    /// <returns>A bool check if there is a room?</returns>
    public bool DoesRoomExist(int x, int y)
    {
        //Checks if there is a room at a given coordinate
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        //Checks if there is a room at a given coordinate
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }
}
