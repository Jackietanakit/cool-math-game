using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI informationText;
    public Transform parent;
    public static RestManager Instance;
    private OperationCard card;
    public List<Transform> operationOptionTransform;
    public OperationOption OperationOptionPrefab;
    public Transform newOperationTransform;
    public GameObject inventoryPanel;
    public GameObject inventoryZone;
    public GameObject idleScreen;
    public GameObject informationScreen;
    public GameObject restScreen;
    public GameObject canvas;
    private Artifact artifact;
    public TextMeshProUGUI restOptionText;
    public Button restOptionButton;

    void Start()
    {
        CreateRandomOperatorCard();
        GenerateInventory();
    }

    private void CreateRandomOperatorCard()
    {
        // Create Random Operation Card
        card = cardManager.Instance.CreateRandomCard();
        artifact = cardManager.Instance.CreateRandomArtifact();
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
            idleScreen.SetActive(false);
            informationScreen.SetActive(false);
            restScreen.SetActive(false);
            canvas.SetActive(false);
        }
        else
        {
            RewardManager.Instance.AddOperationCard();
            ScenesManager.Instance.LoadMapScene();
        }
    }

    public void ArtifactOption()
    {
        Debug.Log("ActionButton");
        RewardManager.Instance.AddArtifact(artifact);
        ScenesManager.Instance.LoadMapScene();
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        inventoryZone.SetActive(false);
        idleScreen.SetActive(true);
        informationScreen.SetActive(true);
        restScreen.SetActive(true);
        canvas.SetActive(true);
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

    public void GetInformation(string option)
    {
        switch (option)
        {
            case "Rest":
                informationText.text =
                    "Increase Current HP by 1 (The HP will not exceed MAX Health)";
                break;
            case "Operation":
                informationText.text = "Get a Random Operation (Maximum 5 Operation Cards)";
                break;
            case "Artifact":
                informationText.text = "Get a Random Artifact. Help you defeat the Game!";
                break;
            default:
                informationText.text = "Select 1 of the 3 Options.";
                break;
        }
    }
}
