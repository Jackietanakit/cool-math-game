using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int money;

    public List<Operation> opeartions = new List<Operation>();
    public List<Artifact> artifacts = new List<Artifact>();

    private string saveFilePath;

    private void Start()
    {
        currentHealth = maxHealth;
        saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
        LoadInventory();
    }

    public void AddCard(Operation card)
    {
        opeartions.Add(card);
        SaveInventory();
    }

    public void RemoveCard(Operation card)
    {
        opeartions.Remove(card);
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

[System.Serializable]
public class Card
{
    public string name;
    public CardType type;
    public int damage;
    public int blockAmount;
    public string effect;
}

public enum CardType
{
    Attack,
    Defense,
    Utility
}

[System.Serializable]
public class Artifact
{
    public string name;
    public ArtifactType type;
    public string bonus;
    public string effect;
}

public enum ArtifactType
{
    Passive,
    Active,
    Consumable
}
