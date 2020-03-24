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
    [SerializeField] private Transform cameraTarget;    //Target for the game camera
    [SerializeField] private AudioClip damageSound; //Damage sound on player
    [SerializeField] private AudioClip deathSound;  //Death Sound on player

    [Header("Character Stats")]
    public CharacterStat maxHealth;     //Max Health of the player
    public CharacterStat maxMana;       //Max mana of the player
    public CharacterStat manaRegen;     //Rate of mana regeneration
    public CharacterStat strength;      //Base damage of player
    public CharacterStat speed;         //Player speed
    public CharacterStat magic;         //Magic Power of the character

    //List of stat buffs on the player
    private List<StatBuff> statBuffs = new List<StatBuff>();

    [Header("Event Data")]
    [SerializeField] private AudioClipEvent playSFXEvent;
    [SerializeField] private IntEvent setMaxHealthEvent;
    [SerializeField] private IntEvent setHealthEvent;
    [SerializeField] private IntEvent setMaxManaEvent;
    [SerializeField] private IntEvent setManaEvent;
    [SerializeField] private IntEvent setScoreEvent;
    [SerializeField] private IntEvent submitScoreEvent;
    [SerializeField] private VoidEvent loseGameEvent;
    [SerializeField] private VoidEvent toggleInventoryEvent;
    [SerializeField] private InventoryEvent inventoryEvent;
    [SerializeField] private TransformEvent setCameraRefEvent;

    private int health;                 //Current health
    private int mana;                   //Current mana
    private float manaRegenTimer = 0;   //Measure for regenerating mana

    private void Start() {
        //Hide the mouse cursor and lock it in place
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Set health to maxHealth
        health = (int) maxHealth.Value;

        //Set mana to maxMana
        mana = (int)maxMana.Value;

        //Get reference to animator component
        //anim = GetComponent<Animator>();

        StartLevel();
        
    }

    public void StartLevel()
    {
        //Set up the player UI
        setMaxHealthEvent.Raise((int)maxHealth.Value);
        setMaxManaEvent.Raise((int)maxMana.Value);
        setManaEvent.Raise(mana);
        setHealthEvent.Raise(health);
        setScoreEvent.Raise(score);

        //Link the player inventory to any relevant components
        inventoryEvent.Raise(inventory);

        //Set up the reference to the camera
        setCameraRefEvent.Raise(cameraTarget);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpendMana(10);
        }

        ManaRegen();
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
        //Clamp the value to make sure it doesn't go below 0
        health = Mathf.Clamp(health, 0, (int)maxHealth.Value);
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

    //Function to get the current health value
    public int GetHealth()
    {
        return health;
    }

    private void ManaRegen()
    {
        //If the mana is equal to the max mana
        if(mana == maxMana.Value)
        {
            //Ignore this function
            return;
        }

        //Check if the game was won or if the player is dead
        if(playerState == PlayerState.Dead || playerState == PlayerState.Win)
        {
            return;
        }

        //Increase the timer over time
        manaRegenTimer += Time.deltaTime;

        //If a second has passed
        if(manaRegenTimer >= 1)
        {
            //Increase mana by mana regen
            RecoverMana((int)manaRegen.Value);

            //Set the regen timer back to 0
            manaRegenTimer = 0;
        }
    }

    public void SpendMana(int manaCost)
    {
        //Reduce mana by the cost
        mana -= manaCost;

        //Clamp mana value so it doesn't go below 0
        mana = Mathf.Clamp(mana, 0, (int)maxMana.Value);

        //Update the mana UI
        setManaEvent.Raise(mana);
    }

    public void RecoverMana(int recoverAmount)
    {
        //Recover mana
        mana += recoverAmount;
        //Clamp mana value so it doesn't go above max mana
        mana = Mathf.Clamp(mana, 0, (int)maxMana.Value);

        //Update the mana bar
        setManaEvent.Raise(mana);
    }

    //Get the current mana value
    public int GetMana()
    {
        return mana;
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

    //Function to add a permaanent stat upgrade
    public void UpgradeStat(StatBuff newUpgrade)
    {
        //Get the stat modifier
        StatModifier newStatMod = newUpgrade.GetBuffModifier();

        switch (newUpgrade.StatType)
        {
            case StatType.MaxHealth:
                //Add the modifier to health
                maxHealth.AddModifier(newStatMod);
                //Update the health UI
                setMaxHealthEvent.Raise((int)maxHealth.Value);
                break;
            case StatType.Strength:
                //Add the modifier to strength
                strength.AddModifier(newStatMod);
                break;
            case StatType.Speed:
                //Add the modifier to speed
                speed.AddModifier(newStatMod);
                break;
        }
    }

    //Function to add a temporary stat upgrade
    public void AddBuff(StatBuff newBuff)
    {
        StatBuff buffToAdd = new StatBuff(newBuff.StatType, newBuff.ModType, newBuff.BuffValue, newBuff.Duration);
        //Define a new stat modifier
        StatModifier newBuffModifier = buffToAdd.GetBuffModifier();

        switch (buffToAdd.StatType)
        {
            case StatType.MaxHealth:
                //Add the modifier to the max health stat
                maxHealth.AddModifier(newBuffModifier);

                //Add the stat buff to the list of stat buffs
                statBuffs.Add(buffToAdd);
                break;
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
                    case StatType.MaxHealth:
                        //Remove all modifiers from this buff
                        maxHealth.RemoveAllModifiersFromSource(statBuffs[i]);

                        //Remove the buff from the list
                        statBuffs.Remove(statBuffs[i]);

                        //adjust the max health in the ui
                        setMaxHealthEvent.Raise((int)maxHealth.Value);

                        //If the player's current health is higher than the max health
                        if(health > maxHealth.Value)
                        {
                            //Clamp the current health to the max health value
                            health = Mathf.Clamp(health, 0, (int) maxHealth.Value);

                            //Update the health ui
                            setHealthEvent.Raise(health);
                        }
                        break;
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
