using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Possible doortypes
    public enum DoorType
    {
        Up, Down, Left, Right
    }

    public DoorType doorType;    //Point where the door is at
}
