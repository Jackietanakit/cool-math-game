using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Info", menuName = "Enemy/Enemy Info")]
public class EnemyInfoSO : ScriptableObject
{
    public int minHealth;
    public int maxHealth;
    public SPUM_Prefabs enemyPrefab;
    public List<DifficultyRequirement> difficultyRequirements;
}

[Serializable]
public struct DifficultyRequirement
{
    public float DifficultyThreshold;
    public List<Requirement> Requirements;

    public PenaltyType Penalty;
    public float PenaltyAmount;
}

public enum Requirement
{
    Exact,
    WithIn10Percent,
    WithIn25Percent,
    Prime,
    Odd,
    Even,
    MultipleOf3, // may change to Multiple Of N
}

public enum PenaltyType //Penalty if the requirement(s) are not met
{
    DividedBy,
    ModBy,
    SetTo,
}
