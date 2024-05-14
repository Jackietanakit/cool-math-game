using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public int maxHealth = 3;
    public int currentHealth;
    public int money = 0;
    public int maxOperation = 5;

    public float difficulty = 1;

    public int EnemyAdditionalCount = 0; //Can be edited by difficulty modifier

    public List<OperationCard> operationCards = new List<OperationCard>();
    public List<Artifact> artifacts = new List<Artifact>();
    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure GameManager persists between scenes if needed
        }
        else
        {
            Destroy(gameObject); // Ensure only one GameManager instance exists
        }
    }

    public void CreateNewPlayerInventory()
    {
        // Clear all data
        operationCards.Clear();
        artifacts.Clear();

        // Initial Health
        currentHealth = maxHealth;

        // Add initial cards
        operationCards = cardManager.Instance.CreateInitialDeck();
    }

    public void AddOperationCard(OperationCard card)
    {
        operationCards.Add(card);
    }

    public void RemoveOperationCard(OperationCard card)
    {
        operationCards.Remove(card);
    }

    public void AddArtifact(Artifact artifact)
    {
        artifacts.Add(artifact);
    }

    public void RemoveArtifact(Artifact artifact)
    {
        artifacts.Remove(artifact);
    }

    public void SaveInventory()
    {
        Debug.Log("Saving inventory to: " + saveFilePath);
        PlayerInventoryData playerInventoryData = new PlayerInventoryData();
        playerInventoryData.SaveData();
    }

    public void LoadInventory()
    {
        Debug.Log("Loading inventory from: " + saveFilePath);
        PlayerInventoryData playerInventoryData = new PlayerInventoryData();
        playerInventoryData = playerInventoryData.loadData();
        ApplyData(playerInventoryData);
    }

    private void ApplyData(PlayerInventoryData data)
    {
        Debug.Log("Change Data Formamt");
        maxHealth = data.maxHealth;
        currentHealth = data.currentHealth;
        money = data.money;
        maxOperation = data.maxOperation;
        difficulty = data.difficulty;
        EnemyAdditionalCount = data.EnemyAdditionalCount;
        foreach (OperationName operationName in data.operationNames)
        {
            operationCards.Add(cardManager.Instance.CreateNewCard(operationName, null));
        }

        foreach (string artifactName in data.artifactNames)
        {
            artifacts.Add(cardManager.Instance.CreateArtifactFromName(artifactName));
        }
    }

    public List<OperationName> GetOperationCardNames()
    {
        List<OperationName> cardNames = new List<OperationName>();
        foreach (OperationCard card in operationCards)
        {
            cardNames.Add(card.operationName);
        }

        return cardNames;
    }

<<<<<<< Updated upstream
    public void SetDifficulty(int difficulty)
    {
        this.difficulty = difficulty;
    }
=======
    public void SetDifficultyTo(float difficulty)
    {
        this.difficulty = difficulty;
    }

    public void SetMoneyTo(int amount)
    {
        money = amount;
    }
>>>>>>> Stashed changes
}
