using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;              //Player Healthbar
    [SerializeField] private TextMeshProUGUI healthText;    //Player health Text
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

    public void UpdateScore(int score){
        //Set the score to 0
        scoreText.text = $"{score}";
    }

    private void SetHealthText()
    {
        healthText.text = $"{healthBar.value}/{healthBar.maxValue}";
    }
}
