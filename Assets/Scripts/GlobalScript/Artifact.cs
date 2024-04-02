using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Artifact
{
    public string name;
    public ArtifactType type;
    public string bonus;
    public string effect;
}

public enum ArtifactType
{
    Passive,
    Active,
    Consumable
}
