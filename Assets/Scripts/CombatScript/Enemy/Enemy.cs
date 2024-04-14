using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public TextMeshProUGUI healthText;

    public int health;

    public List<Requirement> requirements = new List<Requirement>();

    public void Initialize(EnemyInfoSO enemyInfo)
    {
        health = Random.Range(enemyInfo.minHealth, enemyInfo.maxHealth + 1);
        this.requirements = enemyInfo.requirements;
        spriteRenderer.sprite = enemyInfo.sprite;
        UpdateHealth();
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
        if (requirements.Contains(Requirement.Exact) && damage != health)
        {
            damage /= 2;
        }
        // else if (requirements.Contains(Requirement.Prime) && !IsPrime(damage))
        // {
        //     damage /= 2;
        // }
        else if (requirements.Contains(Requirement.Odd) && damage % 2 == 0)
        {
            damage /= 2;
        }
        else if (requirements.Contains(Requirement.Even) && damage % 2 != 0)
        {
            damage /= 2;
        }
        else if (requirements.Contains(Requirement.MultipleOf3) && damage % 3 != 0)
        {
            damage /= 2;
        }
        return damage;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void UpdateHealth()
    {
        healthText.text = health.ToString();
    }
}
