using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private Character character;   //The character of this selection

    //Set the selected character on the player spawner to this character
    public void SelectCharacter()
    {
        PlayerSpawner.SelectedCharacter = character;
    }
}
