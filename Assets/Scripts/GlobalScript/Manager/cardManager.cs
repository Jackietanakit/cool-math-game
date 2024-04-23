using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    void Awake()
    {
        Instance = this;
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
        List<OperationCard> initialDeck = new List<OperationCard>
        {
            // Add initial cards
            CreateNewCard(OperationName.Add, null),
            CreateNewCard(OperationName.Subtract, null),
            CreateNewCard(OperationName.Multiply, null),
            CreateNewCard(OperationName.Divide, null)
        };

        return initialDeck;
    }

    public OperationCard CreateRandomCard()
    {
        List<OperationName> operationInDeck =
            GameManager.instance._playerInventory.GetOperationCardNames();

        // Get randome cards that not contain in the deck
        foreach (OperationName name in System.Enum.GetValues(typeof(OperationName)))
        {
            if (!operationInDeck.Contains(name))
            {
                return CreateNewCard(name, null);
            }
        }

        return null;
    }
}
