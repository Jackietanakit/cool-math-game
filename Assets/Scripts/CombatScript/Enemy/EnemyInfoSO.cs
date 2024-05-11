using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Info", menuName = "Enemy/Enemy Info")]
public class EnemyInfoSO : ScriptableObject
{
    public int minHealth;
    public int maxHealth;

    public string EnemyName;

    public string EnemyDescription;
    public SPUM_Prefabs enemyPrefab;
    public List<DifficultyRequirement> difficultyRequirements;
}

[Serializable]
public struct DifficultyRequirement
{
    public float difficultyThreshold;
    public List<Requirement> requirements;

    public PenaltyType penalty;
    public float penaltyAmount;

    public DifficultyRequirement(bool placeholder)
    {
        this.difficultyThreshold = 0;
        this.requirements = new List<Requirement>();
        this.penalty = PenaltyType.DividedBy;
        this.penaltyAmount = 0;
    }

    public DifficultyRequirement(
        float difficultyThreshold,
        List<Requirement> requirements,
        PenaltyType penalty,
        float penaltyAmount
    )
    {
        this.difficultyThreshold = difficultyThreshold;
        this.requirements = requirements;
        this.penalty = penalty;
        this.penaltyAmount = penaltyAmount;
    }

    // Check the requirement, if not met then take penalty, return true damage to the enemy

    public int ApplyRequirement(int Damage)
    {
        if (CheckRequirement(Damage))
        {
            return ApplyPenalty(Damage);
        }
        return Damage;
    }

    //return true if the requirement is not met
    public bool CheckRequirement(int Damage)
    {
        if (requirements.Contains(Requirement.Exact))
        {
            if (Damage != difficultyThreshold)
            {
                return true;
            }
        }
        else if (requirements.Contains(Requirement.WithIn10Percent))
        {
            if (Damage < difficultyThreshold * 0.9 || Damage > difficultyThreshold * 1.1)
            {
                return true;
            }
        }
        else if (requirements.Contains(Requirement.WithIn25Percent))
        {
            if (Damage < difficultyThreshold * 0.75 || Damage > difficultyThreshold * 1.25)
            {
                return true;
            }
        }
        else if (requirements.Contains(Requirement.Prime))
        {
            if (!IsPrime(Damage))
            {
                return true;
            }
        }
        else if (requirements.Contains(Requirement.Odd))
        {
            if (Damage % 2 == 0)
            {
                return true;
            }
        }
        else if (requirements.Contains(Requirement.Even))
        {
            if (Damage % 2 != 0)
            {
                return true;
            }
        }
        else if (requirements.Contains(Requirement.MultipleOf3))
        {
            if (Damage % 3 != 0)
            {
                return true;
            }
        }
        else if (requirements.Contains(Requirement.MultipleOf5))
        {
            if (Damage % 5 != 0)
            {
                return true;
            }
        }
        else if (requirements.Contains(Requirement.Contain3))
        {
            if (!Damage.ToString().Contains("3"))
            {
                return true;
            }
        }
        else if (requirements.Contains(Requirement.Contain5))
        {
            if (!Damage.ToString().Contains("5"))
            {
                return true;
            }
        }
        else if (requirements.Contains(Requirement.Contain7))
        {
            if (!Damage.ToString().Contains("7"))
            {
                return true;
            }
        }
        return false;
    }

    public string GetPenaltyDescription()
    {
        switch (penalty)
        {
            case PenaltyType.DividedBy:
                return "Damage is divided by " + penaltyAmount;
            case PenaltyType.ModBy:
                return "Damage is mod by " + penaltyAmount;
            case PenaltyType.SetTo:
                return "Damage is set to " + penaltyAmount;
            default:
                return "No penalty";
        }
    }

    public string GetRequirementDescription()
    {
        //while loop and switch case
        string description = "";
        foreach (Requirement requirement in requirements)
        {
            switch (requirement)
            {
                case Requirement.Exact:
                    description += "Damage must be exactly equal to the current health. \n";
                    break;
                case Requirement.WithIn10Percent:
                    description += "Damage must be within 10% range of the current health. \n";
                    break;
                case Requirement.WithIn25Percent:
                    description += "Damage must be within 25% of the current health. \n";
                    break;
                case Requirement.Prime:
                    description += "Damage must be a prime number\n";
                    break;
                case Requirement.Odd:
                    description += "Damage must be an odd number\n";
                    break;
                case Requirement.Even:
                    description += "Damage must be an even number\n";
                    break;
                case Requirement.MultipleOf3:
                    description += "Damage must be a multiple of 3\n";
                    break;
                case Requirement.MultipleOf5:
                    description += "Damage must be a multiple of 5\n";
                    break;
                case Requirement.Contain3:
                    description += "Damage must contain the number 3\n";
                    break;
                case Requirement.Contain5:
                    description += "Damage must contain the number 5\n";
                    break;
                case Requirement.Contain7:
                    description += "Damage must contain the number 7\n";
                    break;
            }
        }
        if (description == "")
        {
            description = "None";
        }
        return description;
    }

    int ApplyPenalty(int damage)
    {
        switch (penalty)
        {
            case PenaltyType.DividedBy:
                return damage / (int)penaltyAmount;
            case PenaltyType.ModBy:
                return damage % (int)penaltyAmount;
            case PenaltyType.SetTo:
                return (int)penaltyAmount;
            default:
                return damage;
        }
    }

    bool IsPrime(int number)
    {
        if (number < 2)
            return false;
        if (number == 2)
            return true;
        if (number % 2 == 0)
            return false;

        for (int i = 3; i <= Math.Sqrt(number); i += 2)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }
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
    MultipleOf5, // may change to Multiple Of N
    Contain3,
    Contain5,
    Contain7,
}

public enum PenaltyType //Penalty if the requirement(s) are not met
{
    DividedBy,
    ModBy,
    SetTo,
}
