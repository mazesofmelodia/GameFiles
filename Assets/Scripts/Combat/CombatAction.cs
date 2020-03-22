using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackTarget
{
    Player,
    Enemy
}

public abstract class CombatAction : ScriptableObject
{
    public AttackTarget target;

    public virtual void Invoke(Transform[] attackPoints, int damage, float range = 1.0f)
    {
        Debug.Log($"Attacking target: {target.ToString()}");
    }
}
