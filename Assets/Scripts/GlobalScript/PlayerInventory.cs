using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public int maxHealth = 3;
    public int currentHealth;
    public int money;
    public int maxOperation = 5;

    public List<OperationCard> operationCards = new List<OperationCard>();
    public List<Artifact> artifacts = new List<Artifact>();

    private string saveFilePath;
    private cardManager cardInstance;

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

    private void Start()
    {
        cardInstance = new cardManager(); // Create an instance of cardManager
        currentHealth = maxHealth;
        saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
        LoadInventory();
    }

    public void CreateNewPlayerInventory()
    {
        // Clear all data
        operationCards.Clear();
        artifacts.Clear();

        // Add initial cards
        operationCards = cardInstance.CreateInitialDeck();
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

    public List<OperationName> GetOperationCardNames()
    {
        List<OperationName> cardNames = new List<OperationName>();
        foreach (OperationCard card in operationCards)
        {
            cardNames.Add(card.operationName);
        }

        return cardNames;
    }
}
