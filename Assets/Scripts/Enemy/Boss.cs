using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss Details")]
    [SerializeField] VoidEvent enableHealthBarEvent;
    [SerializeField] FloatEvent setMaxHealthEvent;
    [SerializeField] IntEvent updateHealthBarEvent;
    [SerializeField] VoidEvent onDeathEvent;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //Enable the healthbar
        enableHealthBarEvent.Raise();

        //Set the max value on the healthbar
        setMaxHealthEvent.Raise(maxHealth.Value);

        //Update the healthbar immediately
        updateHealthBarEvent.Raise(health);
    }

    public override void TakeDamage(int damageAmount)
    {
        //Enemy takes damage based on damage amount
        health -= damageAmount;
        //Update health bar
        updateHealthBarEvent.Raise(health);
        //Play damage sound
        playSFXEvent.Raise(damageSound);

        //Check if the enemy has lost all of their health
        if (health <= 0)
        {
            //Enemy is dead
            Die();

            //Set the isdead bool to true
            isDead = true;
        }
    }

    private void OnDestroy()
    {
        onDeathEvent.Raise();
    }
}
