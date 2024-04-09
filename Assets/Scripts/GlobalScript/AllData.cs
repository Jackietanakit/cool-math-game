using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class AllStaticData
{
    public List<OperationCard> operationCards;

    public List<Artifact> artifacts;

    public AllStaticData(List<OperationCard> operationCards, List<Artifact> artifacts)
    {
        this.operationCards = operationCards;
        this.artifacts = artifacts;
    }
}
