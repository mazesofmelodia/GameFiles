using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Shoot Action", menuName ="Combat Actions/Projectile Shoot")]
public class ProjectileShootAction : CombatAction
{
    public Projectile projectile;       //Object to shoot

    public override void Invoke(Transform[] attackPoints, int damage, float range = 1)
    {
        //Set the target, damage and lifetime on the projectile
        projectile.target = target;

        projectile.damage = damage;

        projectile.lifeTime = range;

        //Loop through all the attack points
        foreach (Transform attackPoint in attackPoints)
        {
            //Spawn a projectile at the attack point
            Instantiate(projectile, attackPoint.position, attackPoint.rotation);
        }
    }
}
