using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyStats
{
    [Header("Melee Enemy Stats")]
    [SerializeField] private float stopDistance;    //Stopping Distance
    [SerializeField] private Transform attackPoint; //Enemy Attack point
    [SerializeField] private float range;           //Enemy attack range

    private float attackTime;                       //Enemy attack time
    

    // Update is called once per frame
    void Update()
    {
        //Check if there was a player in the scene
        if(!playerTarget.isDead){
            //Turn the enemy towards the player
            transform.LookAt(playerTarget.transform);
            //Check if the distance between the enemy and the player is greater than the stop distance
            if(Vector3.Distance(transform.position, playerTarget.transform.position) > stopDistance){
                //Toggle enemy animation
                anim.SetBool("IsMoving",true);
                //Move the enemy towards the player
                transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, speed * Time.deltaTime);
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
            PlayerStats hitPlayer = attackedObjects[i].GetComponent<PlayerStats>();

            if(hitPlayer != null){
                //Damage the player
                hitPlayer.TakeDamage(damage);
                Debug.Log("Player damaged");
            }
        }
    }

    

}
