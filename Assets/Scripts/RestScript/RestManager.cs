using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

public class RestManager : MonoBehaviour
{
    public static RestManager Instance;
    public Button button;
    private OperationCard card;
    public List<Transform> operationOptionTransform;
    public OperationOption OperationOptionPrefab;

    void Start()
    {
        CreateRandomOperatorCard();
        Test();
    }

    private void CreateRandomOperatorCard()
    {
        // Create Random Operation Card
        CardManager cardManager = new CardManager();
        card = cardManager.createRandomCard();

        // Create GameObject in the scene
        button.image.sprite = Resources.Load<Sprite>("Operators/" + card.operationName.ToString());
    }

    public void OperationOption()
    {
        Debug.Log("OperationButton");
        RewardManager.Instance.AddOperationCard(card);
        ScenesManager.Instance.LoadMapScene();
    }

    public void RestOption()
    {
        Debug.Log("RestButton");
        RewardManager.Instance.HealHealth(1);
        ScenesManager.Instance.LoadMapScene();
    }

    public void Test()
    {
        OperationOption operationOption = Instantiate(
            OperationOptionPrefab,
            operationOptionTransform[0]
        );
        operationOption.Initialize(card, operationOptionTransform[0]);
    }
}
