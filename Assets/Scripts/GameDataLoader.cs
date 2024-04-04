using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameDataLoader : MonoBehaviour
{
    public static GameDataLoader instance;
    public List<OperationCard> operationCards = new List<OperationCard>();
    public List<Artifact> artifacts = new List<Artifact>();
    AllData allData = new AllData(null, null);

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
    }

    public void Start()
    {
        InitializeData(); // Initialize operation data when the game starts
        SaveGameData(); // Save the data to a JSON file
    }

    private void InitializeData()
    {
        // Define static data for 20 cards
        operationCards.Add(new OperationCard(OperationName.Add, "Additional Operation", null));
        operationCards.Add(
            new OperationCard(OperationName.Subtract, "Subtraction Operation", null)
        );
        operationCards.Add(
            new OperationCard(OperationName.Multiply, "Multiplication Operation", null)
        );
        operationCards.Add(new OperationCard(OperationName.Divide, "Division Operation", null));
        operationCards.Add(new OperationCard(OperationName.Modulo, "Modulo Operation", null));
        operationCards.Add(new OperationCard(OperationName.Power, "Power Operation", null));
        operationCards.Add(new OperationCard(OperationName.Sqrt, "Square Root Operation", null));
        operationCards.Add(new OperationCard(OperationName.Factorial, "Factorial Operation", null));

        artifacts.Add(new Artifact(1, "Artifact 1", "Change all number 1 to number 3", 1));

        allData = new AllData(operationCards, artifacts);

        Debug.Log("Data initialized.");
    }

    private void SaveGameData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "operationInfo.json");
        string jsonData = JsonConvert.SerializeObject(allData, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Data saved to: " + filePath);
    }
}
