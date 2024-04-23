using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestManager : MonoBehaviour
{
    public static RestManager Instance;
    public Button button;
    private OperationCard card;

    void Start()
    {
        CreateRandomOperatorCard();
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
}
