using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllData
{
    public List<OperationCard> operationCards;

    public List<Artifact> artifacts;

    public AllData(List<OperationCard> operationCards, List<Artifact> artifacts)
    {
        this.operationCards = operationCards;
        this.artifacts = artifacts;
    }
}
