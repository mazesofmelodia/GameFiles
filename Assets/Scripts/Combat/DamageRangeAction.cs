using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Range Action", menuName = "Combat Actions/Damage Range")]
public class DamageRangeAction : CombatAction
{
    public override void Invoke(Transform[] attackPoints, int damage, float range = 1)
    {
        foreach(Transform attackPoint in attackPoints)
        {
            //Emit a sphere to hurt objects in the scene
            Collider[] attackedObjects = Physics.OverlapSphere(attackPoint.position, range);

            switch (target)
            {
                case AttackTarget.Player:
                    //Loop through all hit objects
                    for (int i = 0; i < attackedObjects.Length; i++)
                    {
                        //Check if the hit object has a Player stats component
                        Player hitPlayer = attackedObjects[i].GetComponent<Player>();

                        if (hitPlayer != null)
                        {
                            //Damage the player
                            hitPlayer.TakeDamage(damage);
                        }
                    }
                    break;
                case AttackTarget.Enemy:
                    //Loop through all hit objects
                    for (int i = 0; i < attackedObjects.Length; i++)
                    {
                        //Check if the hit object has an Enemy stats component
                        Enemy hitEnemy = attackedObjects[i].GetComponent<Enemy>();

                        if (hitEnemy != null)
                        {
                            //Damage the enemy
                            hitEnemy.TakeDamage(damage);
                        }
                    }
                    break;
            }
            
        }
    }
}
