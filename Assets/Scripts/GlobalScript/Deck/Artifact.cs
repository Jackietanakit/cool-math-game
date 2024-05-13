using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Artifact", menuName = "Artifact")]
public class Artifact : ScriptableObject
{
    public int id;
    public string ArtifactName;
    public string description;
    public int price;
    public ArtifactEffectType effectType;
    public Sprite artifactSprite;

    public Artifact(
        int id,
        string name,
        string description,
        int price,
        ArtifactEffectType effectType
    )
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.price = price;
        this.effectType = effectType;
    }
}

public enum ArtifactEffectType
{
    OnceStartCombat, //Proc effect only on start of the combat
    BeforeTurn, //Proc effect at the start of the turn
    AfterTurn, //Proc effect at the end of the turn
}
