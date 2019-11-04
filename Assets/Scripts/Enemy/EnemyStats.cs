using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private int health;
    [SerializeField] private int maxHealth = 100;   //Max health of enemy
    [SerializeField] private int damage;            //Enemy Damage

    // Start is called before the first frame update
    void Start()
    {
        //Set health to maxHealth
        health = maxHealth;
    }

    public void TakeDamage(int damageAmount){
        //Player takes damage based on damage amount
        health -= damageAmount;

        //Check if the player has lost all of their health
        if(health <= 0){
            //Player is dead
            Die();
        }
    }

    //Enemy has lost all of their health
    private void Die(){
        //Destroy the gameobject
        Destroy(gameObject);
    }
}
