using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Artifact", menuName = "Artifact")]
public class Artifact : ScriptableObject
{
    public int id;

    public Sprite sprite;
    public string ArtifactName;
    public string description;
    public string bonus;
    public int effectId;

    public Artifact(int id, string name, string bonus, int effectId)
    {
        this.id = id;
        this.name = name;
        this.bonus = bonus;
        this.effectId = effectId;
    }
}

public enum ArtifactEffectType
{
    OnceStartCombat, //Proc effect only on start of the combat
    BeforeTurn, //Proc effect at the start of the turn

    AfterTurn, //Proc effect at the end of the turn
}
