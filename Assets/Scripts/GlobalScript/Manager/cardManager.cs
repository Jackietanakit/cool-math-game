using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager
{
    public OperationCard createNewCard(OperationName name, AdditionalEffect effect)
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

    public List<OperationCard> createInitialDeck()
    {
        List<OperationCard> initialDeck = new List<OperationCard>
        {
            // Add initial cards
            createNewCard(OperationName.Add, null),
            createNewCard(OperationName.Subtract, null),
            createNewCard(OperationName.Multiply, null),
            createNewCard(OperationName.Divide, null)
        };

        return initialDeck;
    }

    public OperationCard createRandomCard()
    {
        OperationName operationName = (OperationName)Random.Range(0, 3);
        Debug.Log("Random card not found for name: " + operationName);
        // Create a random operation card
        return createNewCard(operationName, null);
    }
}
