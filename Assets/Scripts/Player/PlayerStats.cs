using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public bool isDead;
    private int health;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int score = 0;

    private Animator anim;

    private void Start() {
        //Set health to maxHealth
        health = maxHealth;
        //Get reference to animator component
        anim = GetComponent<Animator>();
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

    //Player has lost all of their health
    private void Die(){
        //Player death animation
        anim.SetTrigger("Dying");
        //Player is dead
        isDead = true;
        //Disable movement to ensure that the player can't move while they're dead
        PlayerController playerMovement = GetComponent<PlayerController>();
        playerMovement.enabled = false;
    }
}
