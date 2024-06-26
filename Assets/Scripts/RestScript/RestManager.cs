using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI MoneyText;

    [SerializeField]
    Slider HealthSlider;

    [SerializeField]
    TextMeshProUGUI informationText;
    public Transform GridParent;
    public static RestManager Instance;
    private OperationCard card;

    [SerializeField]
    GameObject OperationOptionPrefab;
    public GameObject inventoryPopup;
    public GameObject idleScreen;
    public GameObject informationScreen;
    public GameObject restScreen;
    public GameObject canvas;
    private Artifact artifact;
    public TextMeshProUGUI restOptionText;
    public Button restOptionButton;
    public Image newOperationCardImage;

    void Start()
    {
        UpdateHealthAndMoney();
        CreateRandomOperatorCard();
        GenerateInventory();
        CheckFullHealth();
    }

    private void UpdateHealthAndMoney()
    {
        MoneyText.text = GameManager.instance._playerInventory.money.ToString();
        HealthSlider.value = GameManager.instance._playerInventory.currentHealth;
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
            inventoryPopup.SetActive(true);
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
        inventoryPopup.SetActive(false);
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
        List<OperationCard> operationCards = GameManager.instance._playerInventory.operationCards;

        foreach (OperationCard card in operationCards)
        {
            CreateOperationsInGrid(card);
        }
        newOperationCardImage.sprite = Resources.Load<Sprite>(
            "Operators/" + card.operationName.ToString()
        );
    }

    public void CreateOperationsInGrid(OperationCard operationCard)
    {
        GameObject operationOption = Instantiate(OperationOptionPrefab, GridParent);
        operationOption.GetComponent<OperationOption>().Initialize(operationCard);
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

    public void CheckFullHealth()
    {
        if (
            GameManager.instance._playerInventory.currentHealth
            == GameManager.instance._playerInventory.maxHealth
        )
        {
            restOptionButton.interactable = false;
            restOptionText.color = Color.black;
        }
    }
}
