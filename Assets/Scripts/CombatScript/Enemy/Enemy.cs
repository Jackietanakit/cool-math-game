using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyInfoSO enemyInfo;
    public SPUM_Prefabs spumprefab;

    public TextMeshPro healthText;

    public GameObject RequirementContainer;

    public GameObject DamageInfo;

    public EnemyPanelObjects EnemyPanelObjects;

    public BoxCollider2D Collider;
    public int health;

    public CanvasGroup CanvasGroup;

    public DifficultyRequirement requirement = new DifficultyRequirement(true);

    void Update()
    {
        //If the enemy is hovered for 0.5 seconds, show the panel. if not stop showing. The panel fade out and in
        if (Collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            StartCoroutine(ShowPanel());
        }
        else
        {
            StartCoroutine(FadeOutPanel());
        }
    }

    public void Initialize(EnemyInfoSO enemyInfo)
    {
        this.enemyInfo = enemyInfo;
        spumprefab = enemyInfo.enemyPrefab;
        //instantiates the enemy prefab
        Instantiate(spumprefab, this.transform);
        generateRandomHealth(enemyInfo.minHealth, enemyInfo.maxHealth);
        this.requirement = GetDifficultyRequirement(enemyInfo.difficultyRequirements);
        UpdateHealth();
    }

    private IEnumerator ShowPanel()
    {
        //if the enemy is hovered for 0.5 seconds, show the panel
        yield return new WaitForSeconds(0.5f);
        if (Collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            StartCoroutine(FadeInPanel());
        }
    }

    private IEnumerator FadeInPanel()
    {
        while (CanvasGroup.alpha < 1)
        {
            CanvasGroup.alpha += Time.deltaTime / 2;
            yield return null;
        }
    }

    private IEnumerator FadeOutPanel()
    {
        while (CanvasGroup.alpha > 0)
        {
            CanvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
    }

    void ShowDamageInfo(int Damage)
    {
        DamageInfo.SetActive(true);
        DamageInfo.GetComponent<TextMeshPro>().text = Damage.ToString();
    }

    void HideDamageInfo()
    {
        DamageInfo.SetActive(false);
    }

    public void UpdatePanel()
    {
        EnemyPanelObjects.enemyName.text = enemyInfo.EnemyName;
        EnemyPanelObjects.healthText.text = "Health : " + health.ToString();
        EnemyPanelObjects.requirementDescription.text = requirement.GetRequirementDescription();
        EnemyPanelObjects.penaltyDescription.text = requirement.GetPenaltyDescription();
    }

    public DifficultyRequirement GetDifficultyRequirement(List<DifficultyRequirement> requirements)
    {
        //check the difficulty threshold, use the requirements with the highest reached difficulty threshold, use difficulty from inventory
        DifficultyRequirement highestRequirement = new DifficultyRequirement(true);
        foreach (DifficultyRequirement requirement in requirements)
        {
            if (requirement.difficultyThreshold <= GameManager.instance._playerInventory.difficulty)
            {
                if (
                    highestRequirement.difficultyThreshold == 0
                    || requirement.difficultyThreshold > highestRequirement.difficultyThreshold
                )
                {
                    highestRequirement = requirement;
                }
            }
        }

        if (!highestRequirement.Equals(new DifficultyRequirement(true)))
        {
            return highestRequirement;
        }
        else
        {
            // If no requirement is met, return a default requirement
            Debug.LogError("No requirement found");
            return new DifficultyRequirement(true);
        }
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
        if (!requirement.Equals(new DifficultyRequirement(true)))
        {
            return requirement.ApplyRequirement(damage);
        }
        return damage;
    }

    void generateRandomHealth(int min, int max)
    {
        health = 0;
        // Max and Min is multiplied by the difficulty
        while (requirement.CheckRequirement(health) || health == 0)
            health =
                (int)
                    UnityEngine.Random.Range(
                        min * GameManager.instance._playerInventory.difficulty,
                        max * GameManager.instance._playerInventory.difficulty + 1
                    ) + UnityEngine.Random.Range(-5, 5);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void UpdateHealth()
    {
        healthText.text = health.ToString();
        UpdatePanel();
    }
}

[Serializable]
public struct EnemyPanelObjects
{
    public GameObject InfoPanel;
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI requirementDescription;
    public TextMeshProUGUI penaltyDescription;
}
