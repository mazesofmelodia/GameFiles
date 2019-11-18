using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /*[SerializeField] private int damage;        //Player damage
    [SerializeField] private float attackSpeed; //Attack speed
    [SerializeField] private float range;       //Attack range*/
    [SerializeField] private Weapon currentWeapon;  //Current Weapon the player is holding
    [SerializeField] private AudioClip attackSound; //Sound that plays when the player attacks

    [Space]
    [SerializeField] private Transform attackPoint; //For melee attacks
    [SerializeField] private Transform weaponPoint; //Point to move the weapon Towards

    private GameObject currentWeaponModel;      //Current weapon model in scene
    private Animator anim;                      //Reference to animator component
    private float timeBetweenAttack;            //Time passed since attack

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to animator component
        anim = GetComponent<Animator>();
        //Position the starting weapon of the character
        PositionWeapon(currentWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenAttack <= 0){
            //Check if the player has clicked the attack button
            if(Input.GetButtonDown("Attack")){
                //Play the attacking animation
                anim.SetTrigger("Attacking");
                //Play attack sound
                AudioManager.Instance.PlaySFX(attackSound);

                //Set time between attack to be the attack speed value
                timeBetweenAttack = currentWeapon.attackSpeed;
            }

        }else{
            //Decrease time between attack over time
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    public void ChangeWeapon(){

    }

    //Function activated as part of an animation event
    public void Attack(){

        //Emit a sphere to hurt objects in the scene
        Collider[] attackedObjects = Physics.OverlapSphere(attackPoint.position, currentWeapon.range);

        //Loop through all hit objects
        for (int i = 0; i < attackedObjects.Length; i++)
        {
            //Check if the hit object has an Enemy stats component
            EnemyStats hitEnemy = attackedObjects[i].GetComponent<EnemyStats>();

            if(hitEnemy != null){
                //Damage the enemy
                hitEnemy.TakeDamage(currentWeapon.damage);
            }
        }
    }

    private void PositionWeapon(Weapon weaponToPosition, float scaleFactor = 0.005f){
        //Spawn the model at the weaponPoint
        //Will also make the spawned model a child of the weapon point
        GameObject weaponModel = Instantiate(weaponToPosition.weaponModel, weaponPoint.position, weaponPoint.rotation, weaponPoint);
        //Change the scale of the object in scene
        weaponModel.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
