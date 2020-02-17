using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    [Header("Weapon Details")]
    public int damage;                  //Weapon Damage
    public float attackSpeed;           //Time between attacks
    public float range;                 //Attack Range of weapon
    public GameObject weaponModel;      //Weapon model
}
