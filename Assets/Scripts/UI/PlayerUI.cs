using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;      //Player Healthbar
    [SerializeField] TextMeshProUGUI scoreText;     //Player Score text

    public void InitalizeHealthbar(int maxHealth, int currentHealth){
        //Set the max value to be the maximum health
        healthBar.maxValue = maxHealth;
        //Make sure the current value is set to current health
        healthBar.value = currentHealth;
    }

    public void UpdateHealthBar(int currentHealth){
        //Set the value of the health bar to the current health
        healthBar.value = currentHealth;
    }

    public void UpdateScore(int score){
        //Set the score to 0
        scoreText.text = score.ToString();
    }
}
