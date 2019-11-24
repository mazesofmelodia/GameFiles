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
    [Header("Win/Lose Management, Change for Final Game")]
    [SerializeField] private GameObject winUI;      //Win UI to display
    [SerializeField] private GameObject loseUI;     //Lose UI to display
    [SerializeField] private AudioClip winSound;    //Win Sound
    [SerializeField] private AudioClip loseSound;   //lose Sound

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

    public void RecoverHealth(int recoverAmount){
        //Recover health
        health += recoverAmount;
        //Clamp health value so it doesn't go above max health
        health = Mathf.Clamp(health, 0, maxHealth);

        //Update the health bar
        gameUI.UpdateHealthBar(health);
    }

    public void UpdateScore(int scorePoints){
        //Change the players score to reflect changes
        score += scorePoints;

        //Update score UI
        gameUI.UpdateScore(score);
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
        AudioManager.Instance.PlaySFX(deathSound);
        //Player is dead
        isDead = true;
        //Disable player
        DisablePlayer();

        //Display the lose screen and play the lose sound
        loseUI.SetActive(true);
        AudioManager.Instance.PlaySFX(loseSound);
    }

    public void WinGame(){
        //Display the win screen and play the win sound
        winUI.SetActive(true);
        AudioManager.Instance.PlaySFX(winSound);

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
