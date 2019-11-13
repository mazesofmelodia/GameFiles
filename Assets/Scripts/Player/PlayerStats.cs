using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public bool isDead;
    private int health;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int score = 0;
    [SerializeField] private PlayerUI gameUI;       //Reference to the player UI in the scene
    [SerializeField] private AudioClip damageSound; //Damage sound on player
    [SerializeField] private AudioClip deathSound;  //Death Sound on player

    private Animator anim;

    private void Awake() {
        //Incase the player UI wasn't set on the player
        if(gameUI == null){
            gameUI = FindObjectOfType<PlayerUI>().GetComponent<PlayerUI>();
        }    
    }

    private void Start() {
        //Set health to maxHealth
        health = maxHealth;
        //Get reference to animator component
        anim = GetComponent<Animator>();
        //Set up the player UI
        gameUI.InitalizeHealthbar(maxHealth, health);
        gameUI.UpdateScore(score);
    }

    public void TakeDamage(int damageAmount){
        //Player takes damage based on damage amount
        health -= damageAmount;
        //Play damage sound
        AudioManager.Instance.PlaySFX(damageSound);

        //Update the health UI of the player
        gameUI.UpdateHealthBar(health);

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
        //Play death sound
        AudioManager.Instance.PlaySFX(deathSound);
        //Player is dead
        isDead = true;
        //Disable movement to ensure that the player can't move while they're dead
        PlayerController playerMovement = GetComponent<PlayerController>();
        //Also disable the attack script
        PlayerAttack playerAttack = GetComponent<PlayerAttack>();

        playerAttack.enabled = false;
        playerMovement.enabled = false;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            //Increase score to test score manager
            score += 10;
            //Update ui
            gameUI.UpdateScore(score);
        }
    }
}
