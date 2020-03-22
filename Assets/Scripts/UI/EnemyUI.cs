using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;              //Player Healthbar
    
    public void InitalizeHealthBar(float value)
    {
        //Set the max value of the healthbar to the input value
        healthBar.maxValue = value;
    }

    public void UpdateHealthBar(int value)
    {
        //Update the current value of the healthbar based on the input
        healthBar.value = value;
    }
}
