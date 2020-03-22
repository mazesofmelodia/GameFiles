using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected int health;
    //[SerializeField] protected int maxHealth = 100;         //Max health of enemy
    //[SerializeField] protected int damage;                  //Enemy Damage
    //[SerializeField] protected float speed;                 //Enemy Movement Speed
    [SerializeField] protected float timeBetweenAttacks;    //Time between enemy attacks
    [SerializeField] protected AudioClip damageSound;         //Damage sound on player
    [SerializeField] protected AudioClip deathSound;          //Death Sound on player
    [SerializeField] protected NavMeshAgent agent;          //Enemy Navigation agent
    [SerializeField] private Transform[] attackPoints;      //Enemy Attack point
    [SerializeField] private float range;                   //Enemy attack range
    [SerializeField] private CombatAction combatAction;

    private float attackTime;                       //Enemy attack time

    [Header("Enemy Stats")]
    public CharacterStat maxHealth;
    public CharacterStat strength;
    public CharacterStat speed;

    [Header("Item Drops")]
    //List of Loot from the enemy
    [SerializeField] protected List<Loot> lootItems = new List<Loot>();
    [SerializeField] protected float itemDropRange = 2;     //Range of item drops

    [Header("Event Data")]
    [SerializeField] protected AudioClipEvent playSFXEvent;
    [SerializeField] private IntEvent enemyCountEvent;

    protected Player playerTarget;                       //Player to target
    protected Animator anim;                                //Reference to animator
    private Collider col;
    protected bool isDead;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //Set health to maxHealth
        health = (int)maxHealth.Value;
        //Find the player in scene
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Set the nav agent target to the player
        agent.destination = playerTarget.transform.position;
        //Set the speed of the agent
        agent.speed = speed.Value;
        //Get reference to the animator component
        anim = GetComponent<Animator>();
        //Get reference to collider
        col = GetComponent<Collider>();
        //Increase the number of enemies in the scene by one
        enemyCountEvent.Raise(1);
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        //Check if there was a player in the scene
        if (playerTarget.playerState != PlayerState.Dead)
        {
            //Turn the enemy towards the player
            agent.destination = playerTarget.transform.position;
            //Check if the distance between the enemy and the player is greater than the stop distance
            if (Vector3.Distance(transform.position, playerTarget.transform.position) > agent.stoppingDistance)
            {
                //Toggle enemy animation
                anim.SetBool("IsMoving", true);
            }
            else
            {
                //If time passed is greater than attack time
                if (Time.time >= attackTime)
                {
                    //Toggle enemy animation
                    anim.SetBool("IsMoving", false);
                    //Start the attack function
                    Attack();
                    //Change the attack time to current time + time between attacks
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
        else
        {
            //Stop the enemy from moving
            agent.isStopped = true;

            //Play enemy idle animation
            anim.SetBool("IsMoving", false);
        }
    }

    public virtual void TakeDamage(int damageAmount){
        //Enemy takes damage based on damage amount
        health -= damageAmount;
        //Play damage sound
        playSFXEvent.Raise(damageSound);

        //Check if the enemy has lost all of their health
        if(health <= 0){
            //Enemy is dead
            Die();

            //Set the isdead bool to true
            isDead = true;
        }
    }

    //Enemy has lost all of their health
    protected virtual void Die(){
        agent.isStopped = true;

        //Play the death animation
        anim.SetTrigger("Dying");

        //Check if the enemy has any potential items to drop
        if(lootItems.Count > 0)
        {
            //Drop a random loot
            DropItems();
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
    private void DropItems()
    {
        foreach (Loot lootItem in lootItems)
        {
            //Generate a random number from 0 to 100
            int randomChance = Random.Range(0, 101);

            //Check if the random chance is lower than the drop items drop chance
            if (randomChance < lootItem.dropChance)
            {
                //Spawn the item when the enemy died
                Vector3 randomPos = Random.insideUnitSphere * itemDropRange;

                //Make sure the y position is 0
                randomPos.y = 0;

                Instantiate(lootItem.dropItem, transform.position + randomPos, Quaternion.identity);
            }
        }
    }

    void Attack()
    {
        //Play the attack animation
        anim.SetTrigger("Attacking");
    }

    public void HitPlayer()
    {
        combatAction?.Invoke(attackPoints, (int)strength.Value, range);
    }
}
