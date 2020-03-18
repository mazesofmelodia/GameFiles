using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatType
{
    Strength,
    Speed
}

[Serializable]
public class StatBuff
{
    public StatType StatType;       //Stat type, used for applying different buffs to the player
    public StatModType ModType;     //Stat modifier type
    public float BuffValue;         //Amount to increase the stat by
    public float Duration;       //How long the buff should last

    public StatBuff(StatType statType, StatModType modType, float buffValue, float duration)
    {
        StatType = statType;
        ModType = modType;
        BuffValue = buffValue;
        Duration = duration;
    }

    public StatModifier GetBuffModifier()
    {
        //Create a new stat modifier and return it
        return new StatModifier(BuffValue, ModType, this);
    }
}
