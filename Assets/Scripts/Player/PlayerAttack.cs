using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //Get reference to animator component
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the player has clicked the attack button
        if(Input.GetButtonDown("Attack")){
            //Play the attacking animation
            anim.SetTrigger("Attacking");
        }
    }
}
