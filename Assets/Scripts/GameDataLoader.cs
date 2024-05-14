using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameDataLoader : MonoBehaviour
{
    public static GameDataLoader instance;
    readonly List<OperationCard> operationCards = new List<OperationCard>();
    readonly List<Artifact> artifacts = new List<Artifact>();

    public AllStaticData allStaticData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadGameData();
    }

    private void LoadGameData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "GameData.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            allStaticData = JsonConvert.DeserializeObject<AllStaticData>(jsonData);
            Debug.Log("Game data loaded successfully.");
        }
        else
        {
            Debug.LogError("Game data file not found!");

            SaveGameData();
        }
    }

    private void SaveGameData()
    {
        InitializeData();
        string filePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        string jsonData = JsonConvert.SerializeObject(allStaticData, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Data saved to: " + filePath);
    }

    private void InitializeData() { }
}
