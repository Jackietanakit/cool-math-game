using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OperationCard
{
    public OperationName operationName;
    public string description;
    public AdditionalEffect additionalEffect;
    public Sprite sprite;

    public OperationCard(
        OperationName operationName,
        string description,
        AdditionalEffect additionalEffect
    )
    {
        this.operationName = operationName;
        this.description = description;
        this.additionalEffect = additionalEffect;
        this.sprite = Resources.Load<Sprite>("Operators/" + operationName.ToString());
        ;
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
    Factorial
}

// private Dictionary<OperationName, string> OperationDescription { }
