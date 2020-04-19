using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /*[SerializeField] private int damage;        //Player damage
    [SerializeField] private float attackSpeed; //Attack speed
    [SerializeField] private float range;       //Attack range*/
    [SerializeField] private Weapon currentWeapon;  //Current Weapon the player is holding

    [Space]
    [SerializeField] private Transform[] attackPoints; //For melee attacks
    [SerializeField] private Transform weaponPoint; //Point to move the weapon Towards

    [Header("Event Data")]
    [SerializeField] AudioClipEvent playSFXEvent;

    private Player player;                      //Get a reference to the player
    private GameObject currentWeaponModel;      //Current weapon model in scene
    //private Animator anim;                      //Reference to animator component
    private float timeBetweenAttack;            //Time passed since attack

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the player component
        player = GetComponent<Player>();
        //Get reference to animator component
        //anim = GetComponent<Animator>();
        //Position the starting weapon of the character
        SetWeapon(currentWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenAttack <= 0){
            //Check if the player has clicked the attack button
            if(Input.GetButtonDown("Attack") && player.playerState == PlayerState.Active){
                //Play the attacking animation
                player.anim.SetTrigger("Attacking");
                //Play attack sound
                playSFXEvent.Raise(currentWeapon.weaponSound);

                //Set time between attack to be the attack speed value
                timeBetweenAttack = currentWeapon.attackSpeed;
            }

        }else{
            //Decrease time between attack over time
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    public void ChangeWeapon(Weapon newWeapon){
        //Check if the time between attack is greater than 0
        if (timeBetweenAttack > 0)
        {
            //Exit the function as we don't want to change weapon inbetween attacking
            return;
        }

        //Destroy the current weapon on the character
        Destroy(currentWeaponModel);

        //Record the old weapon
        Weapon oldWeapon = currentWeapon;

        //Remove all the stat modifiers from the old weapon
        player.strength.RemoveAllModifiersFromSource(currentWeapon);

        //Change the weapon on the character
        currentWeapon = newWeapon;

        //Update the model on the character
        SetWeapon(currentWeapon);

        //Define an inventory slot for the old weapon
        ItemSlot oldWeaponItem = new ItemSlot(oldWeapon, 1);

        //Add the old weapon to the inventory
        player.inventory.AddItem(oldWeaponItem);

        //Remove the current weapon from the inventory
        currentWeapon.RemoveFromInventory();
    }

    //Function activated as part of an animation event
    public void Attack(){

        //Call the weapon combat action
        currentWeapon.combatAction.Invoke(attackPoints, (int) player.strength.Value, currentWeapon.range);
    }

    private void SetWeapon(Weapon weaponToPosition, float scaleFactor = 0.005f){
        //Spawn the model at the weaponPoint
        //Will also make the spawned model a child of the weapon point
        currentWeaponModel = Instantiate(weaponToPosition.weaponModel, weaponPoint.position, weaponPoint.rotation, weaponPoint);
        //Change the scale of the object in scene
        currentWeaponModel.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

        //Add the modifiers of the new weapon to the character
        player.strength.AddModifier(new StatModifier(weaponToPosition.damageAddition, StatModType.Flat));
    }
}
