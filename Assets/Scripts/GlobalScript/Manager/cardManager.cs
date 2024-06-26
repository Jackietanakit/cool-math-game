using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardManager : MonoBehaviour
{
    public static cardManager Instance;

    public List<OperationCard> StartingCards;

    [SerializeField]
    List<Artifact> ArtifactPools;

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

    public OperationCard CreateNewCard(OperationName name, AdditionalEffect effect)
    {
        Dictionary<OperationName, OperationCard> operationCardMap =
            new Dictionary<OperationName, OperationCard>();

        // Populate the map with data from GameDataLoader
        foreach (OperationCard card in GameDataLoader.instance.allStaticData.operationCards)
        {
            operationCardMap[card.operationName] = card;
        }

        // Get the desired card based on the name
        if (operationCardMap.TryGetValue(name, out OperationCard operationCard))
        {
            return operationCard;
        }
        else
        {
            Debug.LogWarning("Operation card not found for name: " + name);
            return null; // Or handle the default case as per your requirements
        }
    }

    public List<OperationCard> CreateInitialDeck()
    {
        return StartingCards;
    }

    public OperationCard CreateRandomCard()
    {
        List<OperationName> operationInDeck =
            GameManager.instance._playerInventory.GetOperationCardNames();
        List<OperationName> operationOutDeck = new List<OperationName>();

        // Get randome cards that not contain in the deck
        foreach (OperationName name in System.Enum.GetValues(typeof(OperationName)))
        {
            if (!operationInDeck.Contains(name))
            {
                operationOutDeck.Add(name);
            }
        }
        OperationName randomeOperation = operationOutDeck[Random.Range(0, operationOutDeck.Count)];

        return CreateNewCard(randomeOperation, null);
    }

    public Artifact CreateRandomArtifact()
    {
        // List<Artifact> artifacts = GameDataLoader.instance.allStaticData.artifacts;
        foreach (Artifact artifact in GameManager.instance._playerInventory.artifacts)
        {
            // artifacts.Remove(artifact);
            ArtifactPools.Remove(artifact);
        }
        // return artifacts[Random.Range(0, artifacts.Count)];
        return ArtifactPools[Random.Range(0, ArtifactPools.Count)];
    }

    public Artifact CreateArtifactFromName(string artifactName)
    {
        foreach (Artifact artifact in ArtifactPools)
        {
            if (artifact.ArtifactName == artifactName)
            {
                return artifact;
            }
        }
        return null;
    }
}
