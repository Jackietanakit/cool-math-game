using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class PlayerInventoryData
{
    public int maxHealth;
    public int currentHealth;
    public int money;
    public int maxOperation;
    public float difficulty;
    public int EnemyAdditionalCount;
    public int difficultyModifier;
    public List<OperationName> operationNames;
    public List<string> artifactNames;

    public PlayerInventoryData loadData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");

        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerInventoryData playerInventoryData =
                JsonConvert.DeserializeObject<PlayerInventoryData>(json);
            Debug.Log(playerInventoryData);
            return playerInventoryData;
        }
        return this;
    }

    public void SaveData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");

        maxHealth = PlayerInventory.Instance.maxHealth;
        currentHealth = PlayerInventory.Instance.currentHealth;
        money = PlayerInventory.Instance.money;
        maxOperation = PlayerInventory.Instance.maxOperation;
        difficulty = PlayerInventory.Instance.difficulty;
        EnemyAdditionalCount = PlayerInventory.Instance.EnemyAdditionalCount;
        difficultyModifier = PlayerInventory.Instance.difficultyModifier;
        operationNames = new List<OperationName>();
        foreach (OperationCard operationCard in PlayerInventory.Instance.operationCards)
        {
            operationNames.Add(operationCard.operationName);
        }

        artifactNames = new List<string>();
        foreach (Artifact artifact in PlayerInventory.Instance.artifacts)
        {
            artifactNames.Add(artifact.ArtifactName);
        }

        string json = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(saveFilePath, json);
    }
}
