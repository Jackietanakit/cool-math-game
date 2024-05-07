using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SPUM_Prefabs spumprefab;

    public TextMeshPro healthText;

    public GameObject RequirementContainer;
    public int health;

    public DifficultyRequirement requiremnent;

    public void Initialize(EnemyInfoSO enemyInfo)
    {
        spumprefab = enemyInfo.enemyPrefab;
        //instantiates the enemy prefab
        Instantiate(spumprefab, this.transform);
        health = Random.Range(enemyInfo.minHealth, enemyInfo.maxHealth + 1);
        // this.requirements = enemyInfo.requirements;
        UpdateHealth();
    }

    public DifficultyRequirement GetDifficultyRequirement()
    {

        
        return new DifficultyRequirement();
    }

    //returns whether the enemy dies
    public bool TakeDamage(int damage)
    {
        int actualdamage = GetRealDamage(damage);
        health -= actualdamage;
        if (health <= 0)
        {
            Die();
            Debug.Log("Enemy died");
            return true;
        }
        UpdateHealth();
        return false;
    }

    public int GetRealDamage(int damage)
    {
        // If the number does not pass the requirement, the damage is halfed out
        // if (requirements.Contains(Requirement.Exact) && damage != health)
        // {
        //     damage /= 2;
        // }
        // else if (requirements.Contains(Requirement.Prime) && !IsPrime(damage))
        // {
        //     damage /= 2;
        // }
        // else if (requirements.Contains(Requirement.Odd) && damage % 2 == 0)
        // {
        //     damage /= 2;
        // }
        // else if (requirements.Contains(Requirement.Even) && damage % 2 != 0)
        // {
        //     damage /= 2;
        // }
        // else if (requirements.Contains(Requirement.MultipleOf3) && damage % 3 != 0)
        // {
        //     damage /= 2;
        // }
        return damage;
    }

    void generateRandomHealth(int min, int max) { }

    void Die()
    {
        Destroy(gameObject);
    }

    void UpdateHealth()
    {
        healthText.text = health.ToString();
    }
}
