using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ray Shoot Action", menuName = "Combat Actions/Ray Shoot")]
public class RayShootAction : CombatAction
{
    public override void Invoke(Transform[] attackPoints, int damage, float range = 1)
    {
        base.Invoke(attackPoints, damage, range);
    }
}
