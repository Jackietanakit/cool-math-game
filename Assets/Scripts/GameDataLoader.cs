using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameDataLoader : MonoBehaviour
{
    public static GameDataLoader instance;
    List<OperationCard> operationCards = new List<OperationCard>();
    List<Artifact> artifacts = new List<Artifact>();

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

    private void InitializeData()
    {
        // // Define static data for 20 cards
        // operationCards.Add(new OperationCard(OperationName.Add, "Additional Operation", null));
        // operationCards.Add(
        //     new OperationCard(OperationName.Subtract, "Subtraction Operation", null)
        // );
        // operationCards.Add(
        //     new OperationCard(OperationName.Multiply, "Multiplication Operation", null)
        // );
        // operationCards.Add(new OperationCard(OperationName.Divide, "Division Operation", null));
        // operationCards.Add(new OperationCard(OperationName.Modulo, "Modulo Operation", null));
        // operationCards.Add(new OperationCard(OperationName.Power, "Power Operation", null));
        // operationCards.Add(new OperationCard(OperationName.Sqrt, "Square Root Operation", null));
        // operationCards.Add(new OperationCard(OperationName.Factorial, "Factorial Operation", null));

        // artifacts.Add(new Artifact(1, "Artifact 1", "Change all number 1 to number 3", 1));

        // allStaticData = new AllStaticData(operationCards, artifacts);

        // Debug.Log("Data initialized.");
    }
}
