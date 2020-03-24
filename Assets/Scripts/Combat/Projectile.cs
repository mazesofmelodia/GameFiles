using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public AttackTarget target;   //Determining if this should hurt the player or the enemy
    [HideInInspector] public int damage = 0;        //Damage of the projectile
    [HideInInspector] public float lifeTime = 1f;        //How long the projectile lasts

    public float projectileSpeed;                   //Speed of the projectile

    private void Start()
    {
        Invoke("Despawn", lifeTime);
    }

    private void Update()
    {
        //Move the shot forward over time
        this.transform.position += this.transform.forward * projectileSpeed * Time.deltaTime;
    }

    //Check for collisions
    private void OnTriggerEnter(Collider other)
    {
        switch (target)
        {
            case AttackTarget.Player:
                //Check if the object is tagged player
                if (other.CompareTag("Player"))
                {
                    //Get the player component of the object
                    Player player = other.GetComponent<Player>();

                    //Apply damage to the player
                    player.TakeDamage(damage);
                }
                break;
            case AttackTarget.Enemy:
                //Check if the object is tagged enemy
                if (other.CompareTag("Enemy"))
                {
                    //Get the enemy component of the object
                    Enemy enemy = other.GetComponent<Enemy>();

                    //Apply damage to the enemy
                    enemy.TakeDamage(damage);
                }
                break;
        }

        //Destroy the projectile
        Despawn();
    }

    private void Despawn()
    {
        //Destroy the object
        Destroy(this.gameObject);
    }

}
