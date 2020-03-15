using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class CharacterStat
{
    public float BaseValue;         //The base value of the stat

    //Public reference to the current calculated value
    public virtual float Value
    {
        get
        {
            //check if the value needs to be calculated again
            //Or if the base value has been changed
            if (isDirty || currentBaseValue != BaseValue)
            {
                //Set the currentBaseValue to the base value
                currentBaseValue = BaseValue;
                //Calculate the current value
                currentFinalValue = CalculateFinalValue();

                //Stat is no longer dirty
                isDirty = false;
            }

            //Return the current value
            return currentFinalValue;
        }
    }
    //List of stat modifiers on the stat
    protected readonly List<StatModifier> statModifiers;

    //Public reference to the stat modifiers list
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    protected bool isDirty;                               //Has the value changed since last calculation
    protected float currentBaseValue = float.MinValue;    //Current calculated based value
    protected float currentFinalValue;                    //Get the last calculated value of the stat

    public CharacterStat()
    {
        //Initalise the stat modifiers list
        statModifiers = new List<StatModifier>();

        //References the stat modifiers list as a readonly list
        StatModifiers = statModifiers.AsReadOnly();
    }
    public CharacterStat(float baseValue) : this()
    {
        //Set the base value to be the input value
        BaseValue = baseValue;
    }

    public virtual void AddModifier(StatModifier modifier)
    {
        //Stat is now dirty
        isDirty = true;

        //Add the modifier to the list
        statModifiers.Add(modifier);

        //Sort the modifiers by the order
        statModifiers.Sort(CompareModifierOrder);
    }

    public virtual bool RemoveModifier(StatModifier modifier)
    {
        //Check if the stat modifier was removed
        if (statModifiers.Remove(modifier))
        {
            //value will need to be changed
            isDirty = true;

            return true;
        }

        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        //Bool to check if we removed the stat modifiers
        bool didRemove = false;

        //Loop through all the stat modifiers in reverse
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if(statModifiers[i].Source == source)
            {
                //Stat value will need to be changed
                isDirty = true;

                //Modifiers have been removed
                didRemove = true;

                //Remove the stat modifier
                statModifiers.RemoveAt(i);
            }
        }

        //Return the bool value
        return didRemove;
    }

    //Function to compare which order value is higher between 2 stat modifiers
    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        //If a order is less than b order
        if(a.Order < b.Order)
        {
            return -1;
        }
        //If a order is higher than b order
        else if(a.Order > b.Order)
        {
            return 1;
        }

        //If a order and b order are the same
        return 0;
    }

    protected virtual float CalculateFinalValue()
    {
        //Set the final value as the base value initially
        float finalValue = BaseValue;
        //Total all percentage add modifiers
        float sumPercentAdd = 0;

        //Loop through all the stat modifiers
        for (int i = 0; i < statModifiers.Count; i++)
        {
            //Cache a reference to the current stat modifier
            StatModifier mod = statModifiers[i];


            //Check the type of stat modifier it is
            if(mod.Type == StatModType.Flat)
            {
                //If the modifier is flat, increase the final value by that number
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd)
            {
                //If the modifier is percent, add it to the sumPercentAdd
                sumPercentAdd += mod.Value;

                //If there is no other modifier or if the next stat modifier is not a percent add
                if(i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    //Apply the percent modifiers to the percent add
                    finalValue *= 1 + sumPercentAdd;

                    //Reset the sumPercent add value
                    sumPercentAdd = 0;
                }
            }
            else if(mod.Type == StatModType.PercentMult)
            {
                //If the modifier is percent, multiply the final value by 1 + the value
                finalValue *= 1 + mod.Value;
            }
        }

        //Round the value and return it
        return Mathf.Round(finalValue);
    }
}
