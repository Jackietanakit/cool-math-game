using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "AllData", menuName = "AllData")]
public class AllStaticData : ScriptableObject
{
    public List<OperationCard> operationCards;

    public List<Artifact> artifacts;

    public List<EnemyInfoSO> enemyInfoSOs;
}
