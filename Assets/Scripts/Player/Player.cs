﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool isDead;
    public Inventory inventory;   //Player inventory
    private int health;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int score = 0;
    [SerializeField] private AudioClip damageSound; //Damage sound on player
    [SerializeField] private AudioClip deathSound;  //Death Sound on player

    [Header("Event Data")]
    [SerializeField] private AudioClipEvent playSFXEvent;
    [SerializeField] private IntEvent setMaxHealthEvent;
    [SerializeField] private IntEvent setHealthEvent;
    [SerializeField] private IntEvent setScoreEvent;
    [SerializeField] private VoidEvent loseGameEvent;

    private Animator anim;

    private void Start() {
        //Set health to maxHealth
        health = maxHealth;
        //Get reference to animator component
        anim = GetComponent<Animator>();
        //Set up the player UI
        setMaxHealthEvent.Raise(maxHealth);
        setHealthEvent.Raise(health);
        setScoreEvent.Raise(score);
    }

    public void TakeDamage(int damageAmount){
        //Player takes damage based on damage amount
        health -= damageAmount;
        //Play damage sound
        playSFXEvent.Raise(damageSound);

        //Update the health UI of the player
        setHealthEvent.Raise(health);

        //Check if the player has lost all of their health
        if (health <= 0){
            //Player is dead
            Die();
        }
    }

    public void RecoverHealth(int recoverAmount){
        //Recover health
        health += recoverAmount;
        //Clamp health value so it doesn't go above max health
        health = Mathf.Clamp(health, 0, maxHealth);

        //Update the health bar
        setHealthEvent.Raise(health);
    }

    public void UpdateScore(int scorePoints){
        //Change the players score to reflect changes
        score += scorePoints;

        //Update score UI
        setScoreEvent.Raise(score);
    }

    public int GetScore(){
        //Return the current score value
        return score;
    }

    //Player has lost all of their health
    private void Die(){
        //Player death animation
        anim.SetTrigger("Dying");
        //Play death sound
        playSFXEvent.Raise(deathSound);
        //Player is dead
        isDead = true;
        //Call the lose game event
        loseGameEvent.Raise();
        //Disable player
        DisablePlayer();
    }

    public void WinGame(){
        //Disable player actions
        DisablePlayer();
    }

    private void DisablePlayer(){
        //Disable movement to ensure that the player can't move while they're dead
        PlayerController playerMovement = GetComponent<PlayerController>();
        //Also disable the attack script
        PlayerAttack playerAttack = GetComponent<PlayerAttack>();
        //And the interact script
        PlayerInteractor playerInteractor = GetComponent<PlayerInteractor>();

        playerAttack.enabled = false;
        playerMovement.enabled = false;
        playerInteractor.enabled = false;
    }

}