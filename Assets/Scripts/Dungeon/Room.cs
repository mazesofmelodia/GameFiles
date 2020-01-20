using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width;       //Room width
    public int height;      //Room height
    public int X;           //X coordinate
    public int Y;           //Y coordinate

    [SerializeField] private EnemyStats enemyPrefab;    //Enemy to spawn
    //List of spawnpoints in the room
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    //List of enemies in the room
    private List<EnemyStats> enemies = new List<EnemyStats>();

    private bool enemiesSpawned = false;    //Have the enemies already been spawned

    //Door objects
    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    //List of doors
    public List<Door> doors = new List<Door>();

    //Custom constructor
    public Room(int x, int y)
    {
        //Set the X and Y coordinates based on the input values
        X = x;
        Y = y;
    }

    // Start is called before the first frame update
    void Start()
    {
        //If there is no room controller
        if (RoomController.instance == null)
        {
            //Inform the user that they are in the wrong scene and return
            Debug.Log("Pressed Play in the wrong scene");
            return;
        }

        //Get all of the doors in the room
        Door[] doorArray = GetComponentsInChildren<Door>();

        //Loop through all of the doors in the array
        foreach (Door d in doorArray)
        {
            //Add the door to the doors list
            doors.Add(d);

            //Check the door type on each door
            switch (d.doorType)
            {
                case Door.DoorType.Up:
                    //Set the top door to this door
                    topDoor = d;
                    break;
                case Door.DoorType.Down:
                    //Set the bottom door to this door
                    bottomDoor = d;
                    break;
                case Door.DoorType.Left:
                    //Set the left door to this door
                    leftDoor = d;
                    break;
                case Door.DoorType.Right:
                    //Set the right door to this door
                    rightDoor = d;
                    break;
                default:
                    break;
            }
        }

        //Register this room with the room controller
        RoomController.instance.RegisterRoom(this);

        if (name.Contains("End"))
        {
            RemoveUnconnectedDoors();
        }
    }

    public Room GetRightRoom()
    {
        //Check if there is a room to the right of this room
        if (RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            //Return that room
            return RoomController.instance.FindRoom(X + 1, Y);
        }

        //Otherwise there is no room
        return null;
    }

    public Room GetLeftRoom()
    {
        //Check if there is a room to the left of this room
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            //Return that room
            return RoomController.instance.FindRoom(X - 1, Y);
        }

        //Otherwise there is no room
        return null;
    }

    public Room GetTopRoom()
    {
        //Check if there is a room to the top of this room
        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            //Return that room
            return RoomController.instance.FindRoom(X, Y + 1);
        }

        //Otherwise there is no room
        return null;
    }

    public Room GetBottomRoom()
    {
        //Check if there is a room to the bottom of this room
        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            //Return that room
            return RoomController.instance.FindRoom(X, Y - 1);
        }

        //Otherwise there is no room
        return null;
    }

    //Removes doors that are not connected to any room
    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.Right:
                    //If there is no right room
                    if (GetRightRoom() != null)
                    {
                        //Deactivate the door
                        door.gameObject.SetActive(false);
                    }
                    break;

                case Door.DoorType.Left:
                    //If there is no left room
                    if (GetLeftRoom() != null)
                    {
                        //Deactivate the door
                        door.gameObject.SetActive(false);
                    }
                    break;

                case Door.DoorType.Up:
                    //If there is no up room
                    if (GetTopRoom() != null)
                    {
                        //Deactivate the door
                        door.gameObject.SetActive(false);
                    }
                    break;

                case Door.DoorType.Down:
                    //If there is no down room
                    if (GetBottomRoom() != null)
                    {
                        //Deactivate the door
                        door.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Draw a wire cube around the room
        Gizmos.DrawWireCube(transform.position, new Vector3(width, 0, height));
    }

    public Vector3 GetRoomCentre()
    {
        //Return a vector 3 based on the width, height, x and y values of the room
        return new Vector3(X * width, 0, Y * height);
    }

    private void OnTriggerEnter(Collider other)
    {
        //If a player enters the room and the enemies have not been spawned
        if (other.CompareTag("Player") && !enemiesSpawned)
        {
            //Spawn an enemy at each spawnpoint
            foreach (Transform spawnPoint in spawnPoints)
            {
                EnemyStats newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            //Enemies have been spawned
            enemiesSpawned = true;
        }
    }
}
