using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Shoot Action", menuName ="Combat Actions/Projectile Shoot")]
public class ProjectileShootAction : CombatAction
{
    public override void Invoke(Transform[] attackPoints, int damage, float range = 1)
    {
        base.Invoke(attackPoints, damage, range);
    }
}
