using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int money;

    public List<OperationCard> opeartionCards = new List<OperationCard>();
    public List<Artifact> artifacts = new List<Artifact>();

    private string saveFilePath;

    private void Start()
    {
        currentHealth = maxHealth;
        saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
        LoadInventory();
    }

    public void AddCard(OperationCard card)
    {
        opeartionCards.Add(card);
        SaveInventory();
    }

    public void RemoveCard(OperationCard card)
    {
        opeartionCards.Remove(card);
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
