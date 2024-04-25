using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Operation", menuName = "Operation/OperationCard")]
public class OperationCard : ScriptableObject
{
    public Sprite sprite;
    public OperationName operationName;
    public string description;

    public string limit; //The max number that can be used in the operation
    public AdditionalEffect additionalEffect;

    public OperationCard(
        OperationName operationName,
        string description,
        AdditionalEffect additionalEffect
    )
    {
        this.operationName = operationName;
        this.description = description;
        this.additionalEffect = additionalEffect;
    }
}

public enum OperationName
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Modulo,
    Power,
    Sqrt,
    Factorial,
}

// private Dictionary<OperationName, string> OperationDescription { }
