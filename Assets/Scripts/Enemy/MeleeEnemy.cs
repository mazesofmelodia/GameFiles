using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
    [Header("Melee Enemy Stats")]
    [SerializeField] private Transform attackPoint; //Enemy Attack point
    [SerializeField] private float range;           //Enemy attack range

    private float attackTime;                       //Enemy attack time
    

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        //Check if there was a player in the scene
        if(playerTarget.playerState != PlayerState.Dead){
            //Turn the enemy towards the player
            agent.destination = playerTarget.transform.position;
            //Check if the distance between the enemy and the player is greater than the stop distance
            if(Vector3.Distance(transform.position, playerTarget.transform.position) > agent.stoppingDistance){
                //Toggle enemy animation
                anim.SetBool("IsMoving",true);
            }else{
                //If time passed is greater than attack time
                if(Time.time >= attackTime){
                    //Toggle enemy animation
                    anim.SetBool("IsMoving",false);
                    //Start the attack function
                    Attack();
                    //Change the attack time to current time + time between attacks
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
        else
        {
            //Stop the enemy from moving
            agent.isStopped = true;

            //Play enemy idle animation
            anim.SetBool("IsMoving", false);
        }
    }

    void Attack(){
        //Play the attack animation
        anim.SetTrigger("Attacking");
    }

    public void HitPlayer(){
        //Emit a sphere to hurt objects in the scene
        Collider[] attackedObjects = Physics.OverlapSphere(attackPoint.position, range);

        //Loop through all hit objects
        for (int i = 0; i < attackedObjects.Length; i++)
        {
            //Check if the hit object has a Player stats component
            Player hitPlayer = attackedObjects[i].GetComponent<Player>();

            if(hitPlayer != null){
                //Damage the player
                hitPlayer.TakeDamage(damage);
            }
        }
    }

    

}
