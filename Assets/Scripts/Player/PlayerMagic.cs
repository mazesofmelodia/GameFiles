using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [SerializeField] private Transform[] magicCastPoints;    //Point that magic will be cast from

    private Player player;                                  //Reference to the player script

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    //Function for casting magic
    public void CastMagic(MagicSpell spell)
    {
        //Check if the player has enough mana to cast the spell
        if (player.GetMana() >= spell.manaCost)
        {
            //Spend the mana as the player now cast the spell
            player.SpendMana(spell.manaCost);

            //Add the player magic stat to the spell damage
            int damage = spell.baseDamage + (int)player.magic.Value;

            //Call the combat action of the spell
            spell.combatAction.Invoke(magicCastPoints, damage, spell.range);
        }
    }
}
