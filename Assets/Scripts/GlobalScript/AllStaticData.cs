using System;
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

    public List<EnemySpawningPatternInfo> EnemySpawnInfos;
}

[Serializable]
public struct EnemySpawningPatternInfo
{
    public int mapid;

    public int amountEnemySpawn; //Required for repeating and random type
    public bool isBoss;
    public EnemySpawnType type;
    public List<EnemyInfoSO> Enemies;
}

public enum EnemySpawnType
{
    Repeating,
    Random,
    Fixed
}
