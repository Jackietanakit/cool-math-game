using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Artifact
{
    public int id;
    public string name;
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
