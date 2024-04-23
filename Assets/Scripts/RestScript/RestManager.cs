using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

public class RestManager : MonoBehaviour
{
    public Transform parent;
    public static RestManager Instance;
    public Button button;
    private OperationCard card;
    public List<Transform> operationOptionTransform;
    public OperationOption OperationOptionPrefab;
    public Transform newOperationTransform;
    public GameObject inventoryPanel;
    public GameObject inventoryZone;

    void Start()
    {
        CreateRandomOperatorCard();
        GenerateInventory();
    }

    private void CreateRandomOperatorCard()
    {
        // Create Random Operation Card
        card = CardManager.Instance.CreateRandomCard();

        // Create GameObject in the scene
        button.image.sprite = Resources.Load<Sprite>("Operators/" + card.operationName.ToString());
    }

    public void OperationOption()
    {
        Debug.Log("OperationButton");
        RewardManager.Instance.newCard = card;
        if (
            GameManager.instance._playerInventory.operationCards.Count
            >= GameManager.instance._playerInventory.maxOperation
        )
        {
            inventoryPanel.SetActive(true);
            inventoryZone.SetActive(true);
        }
        else
        {
            RewardManager.Instance.AddOperationCard();
            ScenesManager.Instance.LoadMapScene();
        }
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        inventoryZone.SetActive(false);
    }

    public void RestOption()
    {
        Debug.Log("RestButton");
        RewardManager.Instance.HealHealth(1);
        ScenesManager.Instance.LoadMapScene();
    }

    public void GenerateInventory()
    {
        int i = 0;
        List<OperationCard> operationCards = GameManager.instance._playerInventory.operationCards;
        foreach (OperationCard inventoryCard in operationCards)
        {
            OperationOption operationOption = Instantiate(OperationOptionPrefab, parent);
            operationOption.Initialize(inventoryCard);
            operationOption.transform.position = operationOptionTransform[i].position;
            i++;
        }
        OperationOption newOperationOption = Instantiate(OperationOptionPrefab, parent);
        newOperationOption.Initialize(card);
        newOperationOption.transform.position = newOperationTransform.position;
    }
}
