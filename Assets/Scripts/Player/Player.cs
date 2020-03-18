using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Active,
    Dead,
    InventoryOpen,
    Win
}

public class Player : MonoBehaviour
{
    public PlayerState playerState;     //State of the player
    public Inventory inventory;   //Player inventory
    public Animator anim;         //Player animator

    //[SerializeField] private int maxHealth = 100;
    [SerializeField] private int score = 0;
    [SerializeField] private AudioClip damageSound; //Damage sound on player
    [SerializeField] private AudioClip deathSound;  //Death Sound on player

    [Header("Character Stats")]
    public CharacterStat maxHealth;     //Max Health of the player
    public CharacterStat strength;      //Base damage of player
    public CharacterStat speed;         //Player speed

    //List of stat buffs on the player
    private List<StatBuff> statBuffs = new List<StatBuff>();

    [Header("Event Data")]
    [SerializeField] private AudioClipEvent playSFXEvent;
    [SerializeField] private IntEvent setMaxHealthEvent;
    [SerializeField] private IntEvent setHealthEvent;
    [SerializeField] private IntEvent setScoreEvent;
    [SerializeField] private IntEvent submitScoreEvent;
    [SerializeField] private VoidEvent loseGameEvent;
    [SerializeField] private VoidEvent toggleInventoryEvent;
    [SerializeField] private InventoryEvent inventoryEvent;

    private int health;

    private void Start() {
        //Hide the mouse cursor and lock it in place
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Set health to maxHealth
        health = (int) maxHealth.Value;

        //Get reference to animator component
        //anim = GetComponent<Animator>();
        //Set up the player UI
        setMaxHealthEvent.Raise((int) maxHealth.Value);
        setHealthEvent.Raise(health);
        setScoreEvent.Raise(score);

        //Link the player inventory to any relevant components
        inventoryEvent.Raise(inventory);
    }

    private void Update()
    {
        InventoryToggle();

        //If there are stat buffs on the player
        if(statBuffs.Count != 0)
        {
            //Update the stat buffs
            StatBuffsTick();
        }
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
        health = Mathf.Clamp(health, 0, (int) maxHealth.Value);

        //Update the health bar
        setHealthEvent.Raise(health);
    }

    public void UpdateScore(int scorePoints){
        //Change the players score to reflect changes
        score += scorePoints;

        //Update score UI
        setScoreEvent.Raise(score);
    }

    public void SubmitScore()
    {
        //Submit the score
        submitScoreEvent.Raise(score);
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
        playerState = PlayerState.Dead;
        //Call the lose game event
        loseGameEvent.Raise();
        //Submit the players score
        SubmitScore();
    }

    public void InventoryToggle()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            //If the player is dead or the player won
            if(playerState == PlayerState.Dead || playerState == PlayerState.Win)
            {
                //Exit the function
                return;
            }
            if(playerState != PlayerState.InventoryOpen)
            {
                //Raise the inventory open event
                toggleInventoryEvent.Raise();

                //Change player state to inventory
                playerState = PlayerState.InventoryOpen;
            }
            else
            {
                //Raise the inventory open event
                toggleInventoryEvent.Raise();

                //Change player state to inventory
                playerState = PlayerState.Active;
            }
        }
    }

    public void AddBuff(StatBuff newBuff)
    {
        StatBuff buffToAdd = new StatBuff(newBuff.StatType, newBuff.ModType, newBuff.BuffValue, newBuff.Duration);
        //Define a new stat modifier
        StatModifier newBuffModifier = buffToAdd.GetBuffModifier();

        switch (buffToAdd.StatType)
        {
            case StatType.Strength:
                //Add the modifier value to the strength stat
                strength.AddModifier(newBuffModifier);

                //Add the stat buff to the list of stat buffs
                statBuffs.Add(buffToAdd);
                break;
            case StatType.Speed:
                //Add the modifier to the speed stat
                speed.AddModifier(newBuffModifier);

                //Add the modifier to the list of stat buffs
                statBuffs.Add(buffToAdd);
                break;
        }
    }

    private void StatBuffsTick()
    {
        //Loop through all of the buffs in reverse
        for (int i = statBuffs.Count - 1; i >= 0; i--)
        {
            //Reduce the duration of the stat buff overtime
            statBuffs[i].Duration -= Time.deltaTime;

            //If the duration is less than or equal to 0
            if(statBuffs[i].Duration <= 0)
            {
                //Check the stat type of the buff
                switch (statBuffs[i].StatType)
                {
                    case StatType.Strength:
                        //Remove all modifiers from the stat
                        strength.RemoveAllModifiersFromSource(statBuffs[i]);

                        //Remove the buff from the list
                        statBuffs.Remove(statBuffs[i]);
                        break;
                    case StatType.Speed:
                        //Remove all modifiers from the stat
                        speed.RemoveAllModifiersFromSource(statBuffs[i]);

                        //Remove the buff from the list
                        statBuffs.Remove(statBuffs[i]);
                        break;
                }
            }
        }
    }
}