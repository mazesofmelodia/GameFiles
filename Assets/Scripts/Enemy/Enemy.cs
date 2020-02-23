using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private int health;
    [SerializeField] protected int maxHealth = 100;         //Max health of enemy
    [SerializeField] protected int damage;                  //Enemy Damage
    [SerializeField] protected float speed;                 //Enemy Movement Speed
    [SerializeField] protected float timeBetweenAttacks;    //Time between enemy attacks
    [SerializeField] private AudioClip damageSound; //Damage sound on player
    [SerializeField] private AudioClip deathSound;  //Death Sound on player
    [SerializeField] protected NavMeshAgent agent;    //Enemy Navigation agent

    [Header("Item Drops")]
    [SerializeField] protected List<Loot> lootItems = new List<Loot>();

    [Header("Event Data")]
    [SerializeField] private AudioClipEvent playSFXEvent;
    [SerializeField] private IntEvent enemyCountEvent;

    protected Player playerTarget;                       //Player to target
    protected Animator anim;                                //Reference to animator
    private Collider col;
    protected bool isDead;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //Set health to maxHealth
        health = maxHealth;
        //Find the player in scene
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Set the nav agent target to the player
        agent.destination = playerTarget.transform.position;
        //Get reference to the animator component
        anim = GetComponent<Animator>();
        //Get reference to collider
        col = GetComponent<Collider>();
        //Increase the number of enemies in the scene by one
        enemyCountEvent.Raise(1);
    }

    public void TakeDamage(int damageAmount){
        //Player takes damage based on damage amount
        health -= damageAmount;
        //Play damage sound
        playSFXEvent.Raise(damageSound);

        //Check if the player has lost all of their health
        if(health <= 0){
            //Player is dead
            Die();

            //Set the isdead bool to true
            isDead = true;
        }
    }

    //Enemy has lost all of their health
    private void Die(){
        agent.isStopped = true;

        //Play the death animation
        anim.SetTrigger("Dying");

        //Check if the enemy has any potential items to drop
        if(lootItems.Count > 0)
        {
            //Drop a random loot
            DropItem();
        }
        //Decrease the number of enemies in the scene by one
        enemyCountEvent.Raise(-1);
        //Play death sound
        playSFXEvent.Raise(deathSound);
        //Disable collider
        col.enabled = false;
    }

    public void RemoveEnemy(){
        //Destroy the gameobject
        Destroy(gameObject);
    }

    //Functionality for dropping an item
    private void DropItem()
    {
        //Select a random loot from the list
        Loot randomLoot = lootItems[Random.Range(0, lootItems.Count)];

        //Generate a random number from 0 to 100
        int randomChance = Random.Range(0, 101);

        //Check if the random chance is lower than the drop items drop chance
        if(randomChance < randomLoot.dropChance)
        {
            //Spawn the item when the enemy died
            Instantiate(randomLoot.dropItem, transform.position, transform.rotation);
        }
    }
}
