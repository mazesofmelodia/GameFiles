using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;              //Player Healthbar
    [SerializeField] private Slider manaBar;                //Player Manabar
    [SerializeField] private TextMeshProUGUI healthText;    //Player health Text
    [SerializeField] private TextMeshProUGUI manaText;      //Player mana Text
    [SerializeField] TextMeshProUGUI scoreText;             //Player Score text

    public void InitalizeHealthbar(int maxHealth){
        //Set the max value to be the maximum health
        healthBar.maxValue = maxHealth;

        //Set the health text of the health bar
        SetHealthText();
    }

    public void UpdateHealthBar(int currentHealth){
        //Set the value of the health bar to the current health
        healthBar.value = currentHealth;

        //Set the health text of the health bar
        SetHealthText();
    }

    public void InitalizeManabar(int maxMana)
    {
        //Set the max value to be the maximum mana
        manaBar.maxValue = maxMana;

        //Set the mana text of the mana bar
        SetManaText();
    }

    public void UpdateManaBar(int currentMana)
    {
        //Set the value of the mana bar to the current mana
        manaBar.value = currentMana;

        //Set the mana text of the mana bar
        SetManaText();
    }

    public void UpdateScore(int score){
        //Set the score to 0
        scoreText.text = $"{score}";
    }

    private void SetHealthText()
    {
        //Set the health text based on the max health and the current health
        healthText.text = $"{healthBar.value}/{healthBar.maxValue}";
    }

    private void SetManaText()
    {
        //Set the mana text based on the max mana and the current mana
        manaText.text = $"{manaBar.value}/{manaBar.maxValue}";
    }
}
