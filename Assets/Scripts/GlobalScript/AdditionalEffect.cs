using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalEffect
{
    public enum EffectType
    {
        Heal,
        Damage,
        Money,
        Card,
        Artifact
    }

    public EffectType effectType;
    public int value;
    public string description;
}
