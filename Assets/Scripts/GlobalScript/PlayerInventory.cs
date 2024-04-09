using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public int money;

    public List<OperationCard> operationCards = new List<OperationCard>();
    public List<Artifact> artifacts = new List<Artifact>();

    private string saveFilePath;
    private cardManager cardInstance;

    private void Start()
    {
        cardInstance = new cardManager(); // Create an instance of cardManager
        currentHealth = maxHealth;
        saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
        LoadInventory();
    }

    public void createNewPlayerInventory()
    {
        // Clear all data
        operationCards.Clear();
        artifacts.Clear();

        // Add initial cards
        operationCards = cardInstance.createInitialDeck();
    }

    public void AddOperationCard(OperationCard card)
    {
        operationCards.Add(card);
        SaveInventory();
    }

    public void RemoveOperationCard(OperationCard card)
    {
        operationCards.Remove(card);
        SaveInventory();
    }

    public void AddArtifact(Artifact artifact)
    {
        artifacts.Add(artifact);
        SaveInventory();
    }

    public void RemoveArtifact(Artifact artifact)
    {
        artifacts.Remove(artifact);
        SaveInventory();
    }

    public void SaveInventory()
    {
        string json = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadInventory()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            JsonConvert.PopulateObject(json, this);
        }
    }
}
