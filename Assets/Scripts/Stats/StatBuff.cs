using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public enum StatType
{
    MaxHealth,
    MaxMana,
    ManaRegen,
    Strength,
    Speed,
    Magic
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

    public string GetUpgradeInfo()
    {
        //Create a string builder
        StringBuilder textBuilder = new StringBuilder();

        switch (ModType)
        {
            case StatModType.Flat:
                textBuilder.Append("Increases ").Append(StatType).Append(" by ").Append(BuffValue).AppendLine();
                break;
            case StatModType.PercentAdd:
                textBuilder.Append("Increases ").Append(StatType).Append(" by ").Append(BuffValue * 100).Append("%").AppendLine();
                break;
            case StatModType.PercentMult:
                textBuilder.Append("Increases ").Append(StatType).Append(" by ").Append(BuffValue * 100).Append("%").AppendLine();
                break;
        }

        return textBuilder.ToString();
    }
}
